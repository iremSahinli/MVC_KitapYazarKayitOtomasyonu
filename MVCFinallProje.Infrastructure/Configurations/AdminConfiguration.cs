using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCFinallProje.Domain.Core.BaseEntityConfiguration;
using MVCFinallProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.Configurations
{
    public class AdminConfiguration : AuditableEntityConfiguration<Admin>
    {
        public override void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(128);
            builder.Property(a => a.Email).IsRequired().HasMaxLength(128);
            builder.Property(a => a.IdentityId).IsRequired();
            base.Configure(builder);
        }
    }
}
