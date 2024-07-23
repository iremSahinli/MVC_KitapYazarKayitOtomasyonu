using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCFinallProje.Domain.Core.BaseEntities;
using MVCFinallProje.Domain.Core.BaseEntityConfiguration;
using MVCFinallProje.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.Configurations
{
    public class PublisherConfiguration : AuditableEntityConfiguration<Publisher>
    {
        public override void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(128);
            builder.Property(p => p.Adress).IsRequired();
            base.Configure(builder);
        }
    }
}
