# Bindings

## Assertions

### CosmosContainerShouldExist
Ensures the container exists for the given database.

Example:
```cs
recordService
    .AssertAgainst(new CosmosContainerShouldExist(CosmosClient, "myContainer", "databaseName"));
```

### CosmosDocumentSchemaShouldBeValid
Ensures all documents in the container conform to a schema.

Example :

```cs
recordService
   .AssertAgainst(new CosmosDocumentSchemaShouldBeValid(CosmosClient,$"{containerName}-schema.json", "myContainer", "databaseName"));
```
