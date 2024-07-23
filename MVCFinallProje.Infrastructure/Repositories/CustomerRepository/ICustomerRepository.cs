using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Infrastructure.DataAccess.Interfaces;
using MVCFinallProje.Infrastructure.Repositories.BookRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.Repositories.CustomerRepository
{
    public interface ICustomerRepository : IAsyncRepository, IAsyncFindable<Customer>, IAsyncInsertable<Customer>, IAsyncQueryableRepository<Customer>, IAsyncDeletableRepository<Customer>, IAsyncUpdatableRepository<Customer>, IAsyncTransactionRepository
    {
    }
}
