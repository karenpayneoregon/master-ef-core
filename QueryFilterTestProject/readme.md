# Global Query Filters

[Global query filters](https://docs.microsoft.com/en-us/ef/core/querying/filters) are LINQ query predicates applied to Entity Types in the metadata model (usually in OnModelCreating). A query predicate is a boolean expression typically passed to the LINQ Where query operator. EF Core applies such filters automatically to any LINQ queries involving those Entity Types. EF Core also applies them to Entity Types, referenced indirectly through use of Include or navigation property. Some common applications of this feature are:

- Soft delete - An Entity Type defines an IsDeleted property. See [Karen's article](https://social.technet.microsoft.com/wiki/contents/articles/53834.entity-framework-core-3-x-global-query-filters-c.aspx).
- [Multi-tenancy](https://weblogs.asp.net/ricardoperes/entity-framework-multitenancy) - An Entity Type defines a TenantId property.
- Also for focusing on specific types such in this case on customers living in specific countries.



