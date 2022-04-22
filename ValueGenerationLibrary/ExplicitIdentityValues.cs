using Microsoft.EntityFrameworkCore;

namespace SqlServer.ValueGeneration
{
    public class ExplicitIdentityValues
    {
        public static void Run()
        {
            using (var context = new ExplicitIdentityValuesContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }


            using (var context = new ExplicitIdentityValuesContext())
            {
                context.Blogs.Add(new Blog { BlogId = 100, Url = "http://blog1.somesite.com" });
                context.Blogs.Add(new Blog { BlogId = 101, Url = "http://blog2.somesite.com" });
                context.Blogs.Add(new Blog { BlogId = 200, Url = "http://blog3.somesite.com" });

                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Blogs ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Blogs OFF");
                }
                finally
                {
                    context.Database.CloseConnection();
                }
            }

        }
    }
}