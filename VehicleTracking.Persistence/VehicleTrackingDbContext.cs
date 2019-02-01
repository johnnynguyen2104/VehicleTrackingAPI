using VehicleTracking.Domain.Entities;
using VehicleTracking.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace VehicleTracking.Persistence
{
    public class VehicleTrackingDbContext : DbContext
    {
        public VehicleTrackingDbContext(DbContextOptions<VehicleTrackingDbContext> options)
            : base(options)
        {
        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed(modelBuilder);
            modelBuilder.ApplyConfiguration(new AuditLogConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        }


        //private void Seed(ModelBuilder modelBuilder)
        //{
        //    int a1 = 1,
        //        a2 = 2,
        //        a3 = 3;

        //    modelBuilder.Entity<BankAccount>().HasData(
        //        new BankAccount() { Id = a1, AccountNumber = "1", IsActive = true, Currency = "THB"  },
        //        new BankAccount() { Id = a2, AccountNumber = "2", IsActive = true, Currency = "THB" },
        //        new BankAccount() { Id = a3, AccountNumber = "3", IsActive = true, Currency = "THB" }
        //        );

        //    modelBuilder.Entity<AccountTransaction>().HasData(
        //        new AccountTransaction() { AccountId = a1, Id= 1, Action = ActionCode.Deposit, Amount = 100, Note = "This is the initial deposit."  },
        //        new AccountTransaction() { AccountId = a2, Id= 2, Action = ActionCode.Deposit, Amount = 200, Note = "This is the initial deposit."  },
        //        new AccountTransaction() { AccountId = a3, Id= 3, Action = ActionCode.Deposit, Amount = 300, Note = "This is the initial deposit."  }
        //       );

        //    modelBuilder.Entity<AccountStatement>().HasData(
        //        new AccountStatement() { AccountId = a1, ClosingBalance = 0, Id = 1, StatementDetails = "This statement on Dec, 2018" },
        //        new AccountStatement() { AccountId = a2, ClosingBalance = 0, Id = 2, StatementDetails = "This statement on Dec, 2018" },
        //        new AccountStatement() { AccountId = a3, ClosingBalance = 0, Id = 3, StatementDetails = "This statement on Dec, 2018" }
        //        );
        //}
    }
}
