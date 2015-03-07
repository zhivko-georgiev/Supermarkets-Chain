namespace SupermarketsChain.Data
{
    using System.Data.Entity;
    using SupermarketsChain.Models;
    using Migrations;

    public class SupermarketsChainEntities : DbContext
    {
        public SupermarketsChainEntities()
            : base("SupermarketsChainConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SupermarketsChainEntities, Configuration>());
        }

        public DbSet<Measure> Measures { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Expense> Expenses { get; set; }
    }
}
