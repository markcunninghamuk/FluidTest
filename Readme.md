[![Build Status](https://dev.azure.com/markcunningham/FluidTest/_apis/build/status/FluidTest.Core?branchName=master)](https://dev.azure.com/markcunningham/FluidTest/_build/latest?definitionId=32&branchName=master) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=markcunninghamuk_FluidTest&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=markcunninghamuk_FluidTest)

<a href="https://www.buymeacoffee.com/markcunningham" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>

# Introduction
A flexible and resilient test engine allowing you to focus on reusable components and removing the need to have messy, unreadable tests. Scenarios where you will consider using it, you can also watch the intro video on [youtube](https://www.youtube.com/watch?v=9c8Hdn18AY8)

- Data-based testing
- Resilient retry policy handled internally to reduce flaky tests
- Enforcing maximum reuse of test scenarios ([Single Responsibility Principle](https://en.wikipedia.org/wiki/Single_responsibility_principle))
- Readable tests
- Complex domains where entity behavior is highly coupled
- You require test code to match production grade
- You require a standard approach to testing
- Fluent API, making it more focused
- Parallel test executions, most approaches load data at the start and wipe at the end, but for parallel runs this is not always a good approach

**Note**
One thing to mention here, is you will need to know C# to use the framework.

## Getting Started

To get started you will need to install the nuget package using the command

    Install-Package FluidTest

Once you start you need to know 1 thing: what is the data type of the primary key of your entities. In most cases for example databases, it is a big int. In systems like Dynamics 365 and Salesforce, it could be a Guid.


To get started you need to instantiate a `RecordService`. The `RecordService` is a generic class that allows you to pass in the datatype you are working with. 

**Example**

My system uses Guids on the record and I require to create a record. When you create a new `RecordService`, you must pass in an `AggregateId` and optionally a retry policy, The `AggregateId` is used to cleandown and the end, and is also the identifier of the parent record that you will create. Everything hangs off an aggregate in a relational database model. For NoSQL databases, you don't need to worry as much as you can create a complex object in one hit.

```cs
var service = new RecordService<Guid>(Guid.NewGuid());
```
Alternatively you can pass in a retry policy so you can handle the types of errors you require.

```cs
var retryPolicy = Policy
     .Handle<ArgumentNullException>()
      .Or<InvalidOperationException>()
      .Or<Exception>()
      .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)));

service = new RecordService<Guid>(Guid.NewGuid(), retryPolicy);
```

Once you have instantiated the service you can call the following Methods, below is an example of what a test should typically look like

```cs
var service = new RecordService<Guid>(Guid.NewGuid());

service
    .CreateRecord(new ActiveOrder(service.AggregateId))
    .AssignAggregateId()
    .If(DateTime.Now.Hour > 15, x => x.CreateRelatedRecord(new OrderLine(Guid.NewGuid())))
    .ExecuteAction(new SetShippingDate())
    .Delay(5000)
    .ExecuteAction(new CancelOrder())
    .Delay(1000)
    .AssertAgainst(new MustBeCancelled())
    .Cleanup(new Cleanup());
```

We will cover these methods in the coming chapters. Do you see how readable this test is? It's fair to reason about what this test is doing:

1. We are creating and Order, passing the `AggregateId`. 
2. Then we tell the `RecordService` that we require to Set the `AggregateId` to be the value of the created Record. 
3. We then add a `OrderLine`, if the time is past 3PM.
4. We then set the Shipping Date of the order.
5. We cancel the Order.
6. Then at the end we assert the record is in the state using what we call a Specification class. 
7. Finally we perform a cleanup to remove the record if we choose to.

## Components

**RecordCreators**

- Must implement Interface `IRecordCreator` or `IRelatedRecordCreator`

There are 2 types of record creators:
- `CreateRecord`
- `CreateRelatedRecord` (called after Create Record, the Id of `CreateRecord` is passed into the CreateRelatedRecord class). An example to follow.

Create Record is designed to create a parent record. Example:

```cs
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
```
Now If we create a record related to the above, we create a new class, let's say `OrderLine`, and the engine will pass the Id of the above into our class. The Guid passed in is the Id of the Order we created above. Hopefully this makes it clear how the engine passes information between the methods.

```cs
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
```

Notice an Id is passed in to the `CreateRecord` method meaning no more managing Id's in your tests!

You can also pass in all of the previously created Id's. This is sometimes known as the composite scenario. Here is what that looks like


```cs
public class CreateDummyExampleComposite : IRelatedRecordCreatorComposite<DummyModel, Guid>
{      

    public Record<DummyModel, Guid> CreateRecord(List<Guid> id)
    {
       var ob = new
       {
            key1 = id[0], // The Order Id
            key2 = id[1]  // The Order Line id
       };

       return new Record<DummyModel, Guid>(new DummyModel(), Guid.NewGuid());
     }
}
```

*There are plans to enhance the engine so you can use friendly names / aliases to access records instead of using the index*

**Specifications / Assertions**

- Must implement Interface `BaseValidator`

Specifications are a key part of the framework. Think of a specification as a shopping basket of Assertions. Assertions are a single check against an Entity.

Specifiation classes look like this:

```cs
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
```

The key takeaway here is that you must inherit `BaseValidator` and override the `GetRecord` and the `GetValidators`. The Validators are reusable single units of assertion against a record.    

**Validators**

- Must implement Interface `ISpecificationValidator`

Validators must check only **one** assertion. Why? Because once they do more than one check, they are not reusable or helpful. So I may have a scenario in which the cancelled status only gets checked by an operator manually, but the system sets the date of cancellation. If I am forced to use both I would not be able to use it. That's the idea of single responsibility, promote reuse!  So please ensure you follow this **golden** rule.

*Good Examples of Validators*

```cs
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
```

**Bad Example of Validators* (Unless you are certain it is such a specific example)*

```cs
internal class MustDoB : ISpecificationValidator<Order>
{
    public void Validate(Order item)
    {
        Assert.AreEqual(item.CancelledDate, DateTime.Today);
        Assert.AreEqual(item.Status, OrderStatus.Cancelled);
        Assert.AreEqual(item.OrderDate)                
    }
}
```

**Teardown**

- Must implement Interface `IRecordCleanup`

Teardown is designed to clear down any records created during a test. For unit tests it is unlikely you will need this. For integration tests however, it is advised.

*Example of cleanup / teardown*

```cs
public class Cleanup : IRecordCleanup<Guid>
{        
    void IRecordCleanup<Guid>.Cleanup(Guid AggregateId)
    {
        Console.WriteLine($"clean record using {AggregateId}");
    }
}
```

**Execute Actions**

- Must implement Interface `IExecutableAction` or `IExecutableAggregateAction`
  
For scenarios where you require to perform an action on a record, you can for example cancel an Order or set the status of a record.
  
`ExecuteAggregateAction` will pass in the `AggregateId` from the engine:
  
```cs
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
```
**WaitFor**

Used for situations where execution of the pipeline needs to wait for a specific action complete.
It accepts an implementation of `IWaitableAction` which contains the logic for the action to wait for.

The example below shows an implementation of the `IWaitableAction` that polls a fictitious background service manager to check if it has finished. 

```cs
public class WaitForBackgroundProcess : IWaitableAction
{
    private int millisecondInterval;
    private int millisecondTimeout;
    private Stopwatch stopwatch;
    private FictionalBackgroundServiceManager serviceManager; // Not a real service for example purpose

    public WaitForBackgroundProcess(int millisecondInterval, int millisecondTimeout) {
        this.millisecondInterval =  millisecondInterval;
        this.millisecondTimeout = millisecondTimeout;
        this.stopwatch = new Stopwatch();
        this.serviceManager = new FictionalBackgroundServiceManager();
    }

    public void Execute()
    {
        stopwatch.Start();

        bool conditionFulfilled = false;
        bool timeoutReached = false;
        while(!conditionFulfilled && !timeoutReached)
        {
            // Perform action to check if background process is finished
            conditionFulfilled = serviceManager.HasFinished();
            timeoutReached = (stopwatch.ElapsedMilliseconds >= millisecondTimeout);
            Thread.Sleep(millisecondInterval);
        }

        stopwatch.Stop();
    }
}
```

**If and Conditionals**

`If` is a really useful way to executing an action based on a scenario,

The following example creates a record if the time is past 3pm. Some tests are time based.

```cs
.If(DateTime.Now.Hour > 15, x => x.CreateRelatedRecord(new ActiveOrderConfiguration(Guid.NewGuid())))
```

**AssignAggregateId**

Used for situations where a record created becomes the new key entity. Remember all assertions and cleanup require the `AggregateId` and retrieve the record and perform cleanup based on it, so be careful not to leave a mess in your system.

```cs
.AssignAggregateId();
```

Alternatively you can supply the aggregate Id on the fly depending on your ID type

```cs
.AssignAggregateId(Guid.NewGuid());
.AssignAggregateId(100);
.AssignAggregateId("Customer1001");

```


**GetRecords**

Returns a list of all the objects and their identifiers created with a test session.

```cs
.GetRecords()
```


## Rules and Guidance

Before writing a test:
- Check to see if there are classes already reusable. There is a high chance a record creation may exist or assertions are there to be reused.
- You will probably need a new Specification class for your test. You can pick and mix the assertions when you override the `GetValidators` method.
- Where you need to control the Guids, you can pass them via constructors. (You will have to define the id's in your test).
- Ensure you understand the role of the `AggregateId`. Every record should always have a parent that links all the others together, for example an order has lines and has related entities, but the order is the aggregate. I can cleanup all of the created resources if I happen to know the order id for example.
- Midway through you can change the `AggregateId`, but you must set it yourself using the `AssignAggregateId`.
- As you use the framework more, you should see that the speed of writing tests increases due to the reusability of the classes and the enforcement of single responsible units / classes.

## Referenced Components
Thanks to the guys who have worked on Polly. It used to handle the retry mechanism internally. https://github.com/App-vNext/Polly

## Future Enhancements
Please raise issues using the gitHub Issues tab.

## Good luck

For any enhancements, suggestions or opinions you can contact me at <a href="mailto:mark_cunningham@outlook.com">Email Me</a>. Or raise issues in the issues tab.










