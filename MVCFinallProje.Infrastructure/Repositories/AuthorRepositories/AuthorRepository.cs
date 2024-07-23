using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Infrastructure.AppContext;
using MVCFinallProje.Infrastructure.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.Repositories.AuthorRepositories
{
    public class AuthorRepository : EFBaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)   { }





        // IAuthorRepository arayüzündeki metodların uygulanması burada yapılacak
    }
}
