using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.Repositories.AuthorRepositories

{
    public interface IAuthorRepository : IAsyncRepository, IAsyncFindable<Author>, IAsyncInsertable<Author>, IAsyncQueryableRepository<Author>, IAsyncDeletableRepository<Author>, IAsyncUpdatableRepository<Author>
    {
    }
}
