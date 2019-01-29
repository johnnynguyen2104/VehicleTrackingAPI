﻿using VehicleTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Persistence.Configurations
{
    class AccountStatementConfiguration : IEntityTypeConfiguration<AccountStatement>
    {
        public void Configure(EntityTypeBuilder<AccountStatement> builder)
        {

            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.HasOne(a => a.Account)
                .WithMany(a => a.Statements)
                .HasForeignKey(a => a.AccountId)
                .IsRequired();

            builder.Property(a => a.StatementDetails).HasMaxLength(50);

            builder.Property(a => a.StatementDateTime)
                .HasDefaultValueSql("getdate()");
        }
    }
}
