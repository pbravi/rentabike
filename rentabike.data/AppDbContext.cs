using Microsoft.EntityFrameworkCore;
using rentabike.model;
using rentabike.model.strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace rentabike.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<LeafRental> LeafRentals { get; set; }
        public DbSet<CompositeRental> CompositeRentals { get; set; }
        public DbSet<RentalType> RentalTypes { get; set; }
        public DbSet<StrategyRentalType> FamilyPriceStrategyRentalTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region RentType data
            modelBuilder.Entity<RentalType>().HasData(new RentalType { Id = 1, Description = "Rent by hour", IsComposite = false });
            modelBuilder.Entity<RentalType>().HasData(new RentalType { Id = 2, Description = "Rent by day", IsComposite = false });
            modelBuilder.Entity<RentalType>().HasData(new RentalType { Id = 3, Description = "Rent by week", IsComposite = false });
            modelBuilder.Entity<RentalType>().HasData(new RentalType { Id = 4, Description = "Rent familiar group (30% discount)", IsComposite = true });
            modelBuilder.Entity<RentalType>().HasData(new RentalType { Id = 5, Description = "Rental group", IsComposite = true });
            #endregion

            #region Strategy data
            modelBuilder.Entity<Strategy>().HasData(new Strategy { Id = 1, Description = "Family Strategy", MinCompositeSize = 3, MaxCompositeSize = 5 });
            #endregion

            #region Family Strategy Type data
            modelBuilder.Entity<StrategyRentalType>().HasData(new StrategyRentalType { Id = 1, PriceStrategyId = 1, RentalTypeId = 1, Price = 5 });
            modelBuilder.Entity<StrategyRentalType>().HasData(new StrategyRentalType { Id = 2, PriceStrategyId = 1, RentalTypeId = 2, Price = 20 });
            modelBuilder.Entity<StrategyRentalType>().HasData(new StrategyRentalType { Id = 3, PriceStrategyId = 1, RentalTypeId = 3, Price = 60 });
            modelBuilder.Entity<StrategyRentalType>().HasData(new StrategyRentalType { Id = 4, PriceStrategyId = 1, RentalTypeId = 4, Discount = 0.30 });
            modelBuilder.Entity<StrategyRentalType>().HasData(new StrategyRentalType { Id = 5, PriceStrategyId = 1, RentalTypeId = 5 });
            #endregion
        }
    }
}
