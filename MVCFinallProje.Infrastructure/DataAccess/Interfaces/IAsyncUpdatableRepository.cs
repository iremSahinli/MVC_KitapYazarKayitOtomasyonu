using MVCFinallProje.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncUpdatableRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
//New() işlemi nesnesi üretilebilen bir entity olmak zorunda şartı getirir.