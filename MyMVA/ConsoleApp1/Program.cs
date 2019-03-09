namespace ConsoleApp1
{
    using System.Linq;
    using System;

    using Microsoft.EntityFrameworkCore;

    class Program
    {
        static void Main(string[] args)
        {
            // "Scipt-Migration" command can be used in package manager console to generate SQL script of the latest migration.
            SetupDatabase();

            using (var db = new BloggingContext())
            {
                var blogs = db.Blogs.Include(b => b.Posts).ToList();

                foreach (var blog in blogs)
                {
                    Console.WriteLine($"{blog.BlogUrl.PadRight(33)} [Tenant: {db.Entry(blog).Property("TenantId").CurrentValue}");

                    foreach (var blogPost in blog.Posts)
                    {
                        Console.WriteLine($" - {blogPost.Title.PadRight(30)} [IsDeleted: {blogPost.IsDeleted}]");
                    }

                    Console.WriteLine();
                }
            }

        }

        private static void SetupDatabase()
        {
            using (var db = new BloggingContext())
            {
                if (db.Database.EnsureCreated())
                {
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                }
            }
        }
    }
}
