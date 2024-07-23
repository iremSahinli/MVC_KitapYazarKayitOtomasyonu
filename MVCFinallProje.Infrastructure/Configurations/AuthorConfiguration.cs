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
    public class AuthorConfiguration : AuditableEntityConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.Name).IsRequired().HasMaxLength(128);
            builder.Property(a => a.DateOfBirth).IsRequired();
            base.Configure(builder);
        }
    }
}
