using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MVCFinallProje.Domain.Core.BaseEntities;
using MVCFinallProje.Domain.Entities;
using MVCFinallProje.Domain.Enums;
using MVCFinallProje.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinallProje.Infrastructure.AppContext
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Yukarıdaki kod bloğu IEntityConfiguration interface'inin bulunduğu namespace deki tüm sınıflara configuration uygulamasını sağlar.


        //DB setlerimiz: 
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(IEntityConfiguration).Assembly);
            base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            SetBaseProperties();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) //Bu parametreli olan
        {
            SetBaseProperties();
            return base.SaveChangesAsync(cancellationToken);

        }

        private void SetBaseProperties()
        {
            //context tarafından DB ye gönderilen işlem bloklarını colleciton olarak barındırır.Her işlem bloğunun işlem tipi (State) ve entityleri de içerisinde bulunur. ChangeTracker işlemleri takip eder.

            var entries = ChangeTracker.Entries<BaseEntity>();
            var userId = "UserBulunamadı";
            foreach (var entry in entries)
            {
                //Metotlarımızı buraya ekleyip çağırıyoruz.
                SetIfAdded(entry, userId);
                SetIfModified(entry, userId);
                SetIfDeleted(entry, userId);
            }
        }

        private void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State != EntityState.Deleted)
            {
                return;
            }
            if (entry.Entity is not AuditableEntity entity)
            {
                return;

            }
            entry.State = EntityState.Modified;
            entry.Entity.Status = Status.Deleted;
            entity.DeletedDate = DateTime.Now;
            entity.DeletedBy = userId;

        }

        private void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.Status = Status.Modified;
                entry.Entity.UpdatedBy = userId;
                entry.Entity.UpdatedDate = DateTime.Now;
            }
        }

        private void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Status = Status.Added;
                entry.Entity.CreatedBy = userId;
                entry.Entity.CreatedDate = DateTime.Now;
            }
        }
    }
}
