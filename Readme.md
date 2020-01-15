
# Introduction (v1.0.0.15 onwards)
A flexible Engine allowing you to focus on reusable components and removing the need to have messy unreadable tests. Scenarios where you will consider using it.

- Data based testing
- Enforcing maximum reuse of test scenarios (Single Responsibility Principle)
- Readable tests
- Complex domains where entity behavior is highly coupled
- You require test code to match production grade.
- You require a standard approach to testing.
- Fluent API, making it more focused.
- Parallel test executions, most approaches load data at the start and wipe at the end, but for parallel runs this is not always a good approach

**Note**
One thing to mention here, is you will need to know C# to use the framework.

## Getting Started

To get started you will need to install the nuget package using the command

    Install-Package Marktek.Fluent.Testing.Engine -Version 1.0.0.15

One you start you need to know 1 thing. What is the data type of the primary key of your entities. In most cases for Example databases, it is a big int. In systems like Dynamics 365, Salesforce, it could be a Guid.


To get Started you need to instantiate a RecordService, the RecordService is a generic class that allows you to pass int the datatype you are working with. 

**Example**
My system uses Guids on the record and I require to create a record. When you create a new RecordService, you must pass in an AggregateId, The AggregateId is used to cleandown and the end, and is also the Identifier of the parent record that you will create, Everything hangs off an aggregate in a relation database model. For NoSQL databases, you dont need to worry as much as you can create a complex object in one hit.

    var service = new RecordService<Guid>(Guid.NewGuid());
Once you have instantiated the service you can call the following Methods, below is an example of what a test should typically look like

            var service = new RecordService<Guid>(Guid.NewGuid());
            
            service
                 .CreateRecord(new ActiveOrder(service.AggregateId))
                 .AssignAggregateId()
                 .If(DateTime.Now.Hour > 15, x => x.CreateRelatedRecord(new OrderLine(Guid.NewGuid())))
                 .ExecuteAction(new SetShippingDate())
                 .Delay(5000)
                 .ExecuteActionOnAggregate(new CancelOrder())
                 .Delay(1000)
                 .AssertAgainst(new MustBeCancelled())
                 .Cleanup(new Cleanup());

We will cover these methods in the coming chapters. Do you see how readable this test is. Its fair to reason about what this test is doing. We are creating and Order, passing the AggregateId, Then we tell the RecordService that we require to Set the AggregateId to be the value of the created Record, We then add a OrderLine if the time is past 3PM, We then set the Shipping Date of the order. Cancel the Order and Then at the End we assert the record is in the state using what we call a Specification class. Finally we perform a cleanup to remove the record if we choose to.

## Components

**RecordCreators**

- Must implement Interface **IRecordCreator** or **IRelatedRecordCreator**

There are 2 types of record Creators
- CreateRecord
- CreateRelatedRecord (Called After Create Record, The Id of CreateRecord is passed into the CreateRelatedRecord class). An Example to Follow.

Create Record is designed to create a parent Record. Example

       public ActiveOrderConfiguration(Guid aggregateId)
            {
                this.id = aggregateId;
            }
    
            public Record<Order, Guid> CreateRecord()
            {
                var c = new Order();
                Console.WriteLine($"Creating Order");
			    File.AppendAllLines("C:\\Test\\test.txt",new[] { "Creating Order" });
			    return new Record<Order, Guid>(c, id);
            }
        }

Now If we create a record related to the above, We create a new class, Lets Say OrderLine, and the engine will pass the Id of the above into our class. The Guid passed in is the Id of the Order we created above. Hopefully this makes it clear how the engine passes information between the methods.

    public class OrderLine : IRelatedRecordCreator<OrderLine, Guid>
    {
     public Record<OrderLine, Guid> CreateRecord(Guid id) **Id Passed in from Engine of CreateRecord
            {
                var c = new OrderLine();
                Console.WriteLine($"Creating related Order with parent id {id}");
        
                File.AppendAllLines("C:\\Test\\test.txt", new[] { $"Creating related OrderLine with parent id {id}" });    
                return new Record<OrderLine, Guid>(c, c.Id);
            }
    }
Notice an Id is passed in to the CreateRecord method meaning no more managing Id's in your tests!

**Specifications / Assertions**

- Must implement Interface **BaseValidator**

Specifications are a key part of the framework, think of a specification as a shopping basket of Assertions. Assertions are a Single check against an Entity.
Specifiation classes look like this. 

    public class MustBeCancelled : BaseValidator<Guid, Order>
        {
            public override Order GetRecord(Guid id)
            {
                return new Order { };
            }
    
            public override List<ISpecificationValidator<Order>> GetValidators()
            {
                return new List<ISpecificationValidator<Order>>
               {
                   new MustDoA(), *******These are reusable*****
                   new MustDoB()
               };
            }
        }  

 The key takeaway here is that you must inherit BaseValidator and override the GetRecord and the GetValidators. The Validators are reusable single units of assertion against a record.    

**Validators**

- Must implement Interface **ISpecificationValidator**

Validators must check only **one** assertion, Why? Because once they do more than one check, they are not reusable or helpful. So I may have a scenario in which the cancelled status only gets checked by an operator manually, but the system sets the date of cancellation, If i am forced to use both I would not be able to use it. That's the idea of single responsibility, promote reuse!  So please ensure you follow this **golden** rule.

*Good Examples of Validators*

      internal class MustDoA : ISpecificationValidator<Order>
        {
            public void Validate(Order item)
            {
                Assert.AreEqual(item.Status, OrderStatus.Cancelled);
            }
        }
    
        internal class MustDoB : ISpecificationValidator<Order>
        {
            public void Validate(Order item)
            {
                Assert.AreEqual(item.CancelledDate, DateTime.Today);
            }
        }

**Bad Example of Validators* (Unless you are certain it is such a specific example)*

        internal class MustDoB : ISpecificationValidator<Order>
        {
            public void Validate(Order item)
            {
                Assert.AreEqual(item.CancelledDate, DateTime.Today);
                Assert.AreEqual(item.Status, OrderStatus.Cancelled);
                Assert.AreEqual(item.OrderDate)                
            }
        }

**Teardown**

- Must implement Interface **ISpecificationValidator**

Teardown is designed to clear down any records created during a test, For unit tests it is unlikely you will need this, For integration tests however it is advised.

*Example of cleanup / teardown*
  
         public class Cleanup : IRecordCleanup<Guid>
            {        
                void IRecordCleanup<Guid>.Cleanup(Guid AggregateId)
                {
                    Console.WriteLine($"clean record using {AggregateId}");
                }
            }

**Execute Actions**

- Must implement Interface **IExecutableAction** or **IExecutableAggregateAction**
  
  For scenarios where you require to perform an action on a record, you can for example Cancel an Order or set the status of a record.
  
  *ExecuteAggregateAction* will pass in the AggregateId from the engine
  
            internal class CustomExecutor : IExecutableAction<CustomOrder, Guid>, IExecutableAggregateAction<CustomOrder, Guid>
            {
                public void Execute()
                {
                    throw new NotImplementedException();
                } 
                public void Execute(Guid id)
                {
                    throw new NotImplementedException();
                }
            }
**Delay**
On the record service specify the milliseconds you require to wait for. This is useful for scenarios where you hit an api and require to wait until it finishes. You as the developer are responsible for the waiting time.

**If and Conditionals**
If is a really useful way to executing an action based on a scenario,

The following example creates a record if the time is past 3pm, some tests are time based.

    .If(DateTime.Now.Hour > 15, x => x.CreateRelatedRecord(new ActiveOrderConfiguration(Guid.NewGuid())))

**AssignAggregateId**
Used for situations where a record created becomes the new key entity. Remember all assertions and cleanup require the AggregateId and retrieve the record and perform cleanup based on it, so be careful not to leave a mess in your system.

## Rules and Guidance

Before writing a test
- Check to see if there are classes already reusable, there is a high chance a record creation may exist or assertions are there to be reused
- You will probably need a new Specification class for your test, You can pick and mix the assertions when you override the GetValidators method
- Where you need to control the guids, you can pass them via constructors (You will have to define the id's in your test) 
- Ensure you understand the role of the AggregateId, Every record should always have a parent that links all the others together, for example and order has lines and has related entities, but the order is the aggregate, I can cleanup all of the created resources if i happen to know the order id for example.
- Mid way through you can change the Aggregate Id, but you must set it yourself using the AssignAggregateId 
- As you use the framework more, you should see that the speed of writing tests increases due to the reusability of the classes and the enforcement of single responsible units / classes.

## Good luck
For any enhancements, suggestions or opinions you can contact me at <a href="mailto:mark_cunningham@outlook.com">Email Me</a>  










