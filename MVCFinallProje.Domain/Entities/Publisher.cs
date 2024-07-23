using MVCFinallProje.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Domain.Entities
{
    public class Publisher : AuditableEntity
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }
        public string Name { get; set; }
        public string Adress { get; set; }


        public virtual IEnumerable<Book> Books { get; set; }
    }
}
