using GymManagement.Domain.Admins;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Admins.Persistence
{
    public class AdminConfigurations : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasData(new Admin(
                userId: Guid.NewGuid(),
                id: Guid.Parse("2150e333-8fdc-42a3-9474-1a3956d46de8")));
        }
    }
}
