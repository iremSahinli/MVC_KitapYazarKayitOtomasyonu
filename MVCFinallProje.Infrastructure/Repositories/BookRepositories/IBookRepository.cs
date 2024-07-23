using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.Repositories.BookRepositories
{
    public interface IBookRepository : IAsyncRepository, IAsyncFindable<Book>, IAsyncInsertable<Book>, IAsyncQueryableRepository<Book>, IAsyncDeletableRepository<Book>, IAsyncUpdatableRepository<Book>
    {

    }
}

