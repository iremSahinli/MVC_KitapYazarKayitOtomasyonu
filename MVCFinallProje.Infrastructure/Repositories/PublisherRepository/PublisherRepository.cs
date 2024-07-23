using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Infrastructure.AppContext;
using MVCFinallProje.Infrastructure.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.Repositories.PublisherRepository
{
    public class PublisherRepository : EFBaseRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(AppDbContext context): base(context) { }
        
    }
}
