﻿using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Subscriptions.Persistence
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(s=> s.Id);

            builder.Property(s=>s.Id).ValueGeneratedNever();

            builder.Property("_maxGyms")
                .HasColumnName("MaxGyms");

            builder.Property(s => s.AdminId);

            builder.Property(s=>s.SubscriptionType)
                .HasConversion(subscriptionType => subscriptionType.Value,
                value=> SubscriptionType.FromValue(value));

            builder.Property<List<Guid>>("_gymIds")
                .HasColumnName("GymIds")
                .HasListOfIdsConverter();
        }
    }
}
