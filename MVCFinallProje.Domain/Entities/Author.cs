using MVCFinallProje.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Domain.Entities
{
    public class Author : AuditableEntity
    {
        public Author() //Başlangıta boş oluşturulacak bir constructor yapısı oluşturduk.
        {
            Books = new HashSet<Book>();   //Aynı nesneden birden fazla eklendiği zaman sadece birtanesini koleksiyonda hashset tutar.
        }


        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }


        //Navigation:
        public virtual IEnumerable<Book> Books { get; set; }
    }
}
