using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncRepository
    {
        Task<int> SaveChangeAsync();
    }
}
