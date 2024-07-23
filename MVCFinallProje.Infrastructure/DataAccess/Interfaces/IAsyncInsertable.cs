using MVCFinallProje.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncInsertable<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);  
        Task AddRangeAsync(IEnumerable<TEntity> entities);  
    }
}

//Tekbir Entity gönderirsek ekleyecek. 
//Collection gönderirsek hepsini ekleyecek. Async olduğu için Task Yazılır. (AddRangeAsync)