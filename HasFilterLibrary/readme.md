# Global Query Filters

Global [query filters](https://docs.microsoft.com/en-us/ef/core/querying/filters) are LINQ query predicates applied to Entity Types in the metadata model (usually in OnModelCreating). A query predicate is a boolean expression typically passed to the LINQ Where query operator. EF Core applies such filters automatically to any LINQ queries involving those Entity Types. EF Core also applies them to Entity Types, referenced indirectly through use of Include or navigation property. 

```csharp
modelBuilder.Entity<Customers>()
    .HasQueryFilter(customer => customer.CountryIdentfier == 9);
```

