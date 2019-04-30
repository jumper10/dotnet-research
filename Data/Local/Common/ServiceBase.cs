using CommonLibrary;
using Data.Local.Common;
using Data.Local.Contexts;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Local.Common
{
    public abstract class ServiceBase<T, TKey> where T : EntityBase<TKey>, new()
    {
        protected virtual DbContextType DbContextType { get;} = DbContextType.App;

        protected DbContextFactory _dbContextFactory;

        public ServiceBase()
        {
            _dbContextFactory = SimpleIoc.Default.GetInstance<DbContextFactory>();
        }

        public ServiceBase(DbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected DbContext GetDbContext()
        {
            return _dbContextFactory.GetDbContext(DbContextType);
        }
        public async Task<T> GetItemAsync(TKey id)
        {
            using (var dbContext = GetDbContext())
            {
                return await dbContext.Set<T>().Where(r => r.Id.ToString() == id.ToString()).FirstOrDefaultAsync();
            }
        }

        public async Task<IList<T>> GetItemsAsync(int skip, int take, DataRequest<T> request)
        {
            using (var dbContext = GetDbContext())
            {
                IQueryable<T> items = GetDbSet(request, dbContext);

                // Execute
                var records = await items.Skip(skip).Take(take)
                    .AsNoTracking()
                    .ToListAsync();

                return records;
            }
        }

        public async Task<IList<T>> GetItemKeysAsync(int skip, int take, DataRequest<T> request)
        {
            using (var dbContext = GetDbContext())
            {
                IQueryable<T> items = GetDbSet(request, dbContext);

                // Execute
                var records = await items.Skip(skip).Take(take)
                    .Select(r => new T
                    {
                        Id = r.Id,
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return records;
            }
        }

        public async Task<int> GetItemsCountAsync(DataRequest<T> request)
        {
            using (var dbContext = GetDbContext())
            {
                IQueryable<T> items = GetDbSet(request, dbContext);

                // Where
                if (request.Where != null)
                {
                    items = items.Where(request.Where);
                }

                return await items.CountAsync();
            }
        }

        public async Task<int> CreateItemAsync(T item)
        {
            using (var dbContext = GetDbContext())
            {
                item.CreateDate = DateTime.UtcNow;
                dbContext.Entry(item).State = EntityState.Added;
                return await dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteItemsAsync(params T[] logs)
        {
            using (var dbContext = GetDbContext())
            {
                var items = dbContext.Set<T>();
                items.RemoveRange(logs);
                return await dbContext.SaveChangesAsync();
            }
        }

        //public async Task MarkAllAsReadAsync()
        //{
        //    var items = await Logs.Where(r => !r.IsRead).ToListAsync();
        //    foreach (var item in items)
        //    {
        //        item.IsRead = true;
        //    }
        //    await SaveChangesAsync();
        //}
        protected IQueryable<T> GetDbSet(DataRequest<T> request, DbContext dbContext)
        {
            IQueryable<T> items = dbContext.Set<T>();

            //// Query
            //if (!String.IsNullOrEmpty(request.Query))
            //{
            //    items = items.Where(r => r.Message.Contains(request.Query.ToLower()));
            //}

            // Where
            if (request.Where != null)
            {
                items = items.Where(request.Where);
            }

            // Order By
            if (request.OrderBy != null)
            {
                items = items.OrderBy(request.OrderBy);
            }
            if (request.OrderByDesc != null)
            {
                items = items.OrderByDescending(request.OrderByDesc);
            }

            return items;
        }
    }

    public class LongServiceBase<T> :ServiceBase<T,long> where T : LongEntityBase,new()
    {
        public LongServiceBase(){ }
        [PreferredConstructor]
        public LongServiceBase(DbContextFactory dbContextFactory):base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }

    public class StringServiceBase<T> : ServiceBase<T, string> where T : StringEntityBase, new()
    {
        public StringServiceBase() { }
        [PreferredConstructor]
        public StringServiceBase(DbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
