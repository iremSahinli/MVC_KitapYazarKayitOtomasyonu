using MVCFinallProje.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncFindable<TEntity> where TEntity : BaseEntity
    {
        //Gönderilen sayının değeri budur, Göndermezsem ayı değeri null olacaktır.
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true);

        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true);
    }
}
