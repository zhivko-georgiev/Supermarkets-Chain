namespace SupermarketsChain.Data
{
    using System.Data.Entity;
    using Migrations;
    using Models;

    public class SupermarketsChainEntities : DbContext
    {
        public SupermarketsChainEntities()
            : base("SupermarketsChainConnection")
        {
        }

        public DbSet<Measure> Measures { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}
