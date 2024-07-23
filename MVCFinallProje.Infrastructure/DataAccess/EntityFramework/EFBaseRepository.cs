using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MVCFinallProje.Domain.Core.BaseEntities;
using MVCFinallProje.Domain.Core.Interfaces;
using MVCFinallProje.Domain.Enums;
using MVCFinallProje.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.DataAccess.EntityFramework
{
    public class EFBaseRepository<TEntity> : IRepository, IAsyncRepository, IAsyncInsertable<TEntity>, IAsyncUpdatableRepository<TEntity>, IAsyncDeletableRepository<TEntity>, IAsyncFindable<TEntity>, IAsyncQueryableRepository<TEntity>, IAsyncOrderableRepository<TEntity>, IAsyncTransactionRepository where TEntity : BaseEntity
    {
        protected readonly DbContext _context; //Protected kalıtım verdiğim yerlerde kullanılabilir.
        protected readonly DbSet<TEntity> _table; //Dbset için bir field tanımladık.
        public EFBaseRepository(DbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entry = await _table.AddAsync(entity);
            return entry.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _table.AddRangeAsync(entities);  //Task Async nun parametre olarak voidi anlamına geliyor.
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression is null ? await GetAllActives().AnyAsync() : await GetAllActives().AnyAsync(expression);
            //Bir koşul varsa koşula göre sorgula koşul okda tabloda veri varmı yokmu ona göre bool dön.
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public Task<IExecutionStrategy> CreateExecutionStrategy()
        {
            return Task.FromResult(_context.Database.CreateExecutionStrategy());
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.FromResult(_table.Remove(entity));
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true)
        {
            return await GetAllActives(tracking).ToListAsync();  //istenilen kategoriyi getirecek.
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        {
            return await GetAllActives(tracking).Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool orderByDesc, bool tracking = true)
        {
            return orderByDesc ? await GetAllActives(tracking).OrderByDescending(orderBy).ToListAsync() : await GetAllActives(tracking).OrderBy(orderBy).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderBy, bool orderByDesc, bool tracking = true)
        {
            var values = GetAllActives(tracking).Where(expression);
            return orderByDesc ? await values.OrderByDescending(orderBy).ToListAsync() : await values.OrderBy(orderBy).ToListAsync();
            //:Sıralama durumuna göre return ediyoruz.
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        {
            return await GetAllActives(tracking).FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true)
        {
            return await GetAllActives(tracking).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Savechanges()
        {
            return _context.SaveChanges();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.FromResult(_table.Update(entity).Entity);

        }

        protected IQueryable<TEntity> GetAllActives(bool tracking = true)
        {
            var values = _table.Where(x => x.Status != Status.Deleted);  //İçeride statüsü delete olanları getirme.
            return tracking ? values : values.AsNoTracking();  //Takipe dahil olmadan gelir anlamında tracking  metot kullandık.
        }




    }
}
