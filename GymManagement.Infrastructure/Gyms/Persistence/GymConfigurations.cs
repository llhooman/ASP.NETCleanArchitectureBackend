using GymManagement.Domain.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GymManagement.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Gyms.Persistence
{
    public class GymConfigurations : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g=>g.Id)
                .ValueGeneratedNever();
            builder.Property("_maxRooms")
                .HasColumnName("MaxRooms");
            builder.Property<List<Guid>>("_roomIds")
                .HasColumnName("RoomIds")
                .HasListOfIdsConverter();
            builder.Property<List<Guid>>("_trainerIds")
            .HasColumnName("TrainerIds")
            .HasListOfIdsConverter();

            builder.Property(g => g.Name);

            builder.Property(g => g.SubscriptionId);
        }
    }
}
