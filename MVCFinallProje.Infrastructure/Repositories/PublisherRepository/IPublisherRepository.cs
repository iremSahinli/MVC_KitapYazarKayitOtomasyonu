using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Infrastructure.DataAccess.Interfaces;
using MVCFinallProje.Infrastructure.Repositories.AuthorRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.Repositories.PublisherRepository
{
    public interface IPublisherRepository : IAsyncRepository, IAsyncFindable<Publisher>, IAsyncInsertable<Publisher>, IAsyncQueryableRepository<Publisher>, IAsyncDeletableRepository<Publisher>, IAsyncUpdatableRepository<Publisher>
    {
    }
}
