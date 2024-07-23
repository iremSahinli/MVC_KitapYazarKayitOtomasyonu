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
    public class BookConfiguration : AuditableEntityConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Name).IsRequired().HasMaxLength(128);
            builder.Property(b => b.PublishDate).IsRequired();
            base.Configure(builder);
        }
    }
}
