using MVCFinallProje.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Domain.Entities
{
    public class Book : AuditableEntity
    {
        public string Name { get; set; }

        public DateTime PublishDate { get; set; }


        //Navigations:
        public virtual Author Author { get; set; }
        public Guid AuthorId { get; set; }

        public virtual Publisher Publisher { get; set; }
        public Guid PublisherId { get; set; }

    }
}
