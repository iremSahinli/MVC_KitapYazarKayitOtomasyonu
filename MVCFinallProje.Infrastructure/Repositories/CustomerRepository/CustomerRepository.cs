using Microsoft.EntityFrameworkCore;
using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Infrastructure.AppContext;
using MVCFinallProje.Infrastructure.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.Repositories.CustomerRepository
{
    public class CustomerRepository : EFBaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
