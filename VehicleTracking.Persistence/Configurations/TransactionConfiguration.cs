﻿using VehicleTracking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTracking.Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<AccountTransaction>
    {
        public void Configure(EntityTypeBuilder<AccountTransaction> builder)
        {
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            var converter = new EnumToStringConverter<ActionCode>();

            builder
                .Property(e => e.Action)
                .HasConversion(converter);

            builder
                .Property(e => e.TransactionDatetime)
                .HasDefaultValueSql("getdate()");
        }
    }
}
