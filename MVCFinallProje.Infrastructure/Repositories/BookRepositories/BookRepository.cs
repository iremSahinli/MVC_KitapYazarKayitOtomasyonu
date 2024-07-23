using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Infrastructure.AppContext;
using MVCFinallProje.Infrastructure.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.Repositories.BookRepositories
{
    public class BookRepository : EFBaseRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context) { }
       
    }
}
