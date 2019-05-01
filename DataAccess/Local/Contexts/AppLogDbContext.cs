using CommonLibrary;
using DataAccess.Local.Common;
using DataAccess.Local.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Local.Contexts
{
    public class AppLogDbContext:DbContext
    {
        private string _connetionString  = "Data Source=app_log.bat";

        public AppLogDbContext(DbContextOptions<AppLogDbContext> optionsBuilder)
        {
           
        }

        public AppLogDbContext(string connectionString= "Data Source=app_log.bat")
        {
            _connetionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(_connetionString);
        }

        public void EnsureCreated()
        {
            Database.EnsureCreated();
        }

        public DbSet<AppLog> Logs { get; set; }

        public async Task<AppLog> GetLogAsync(long id)
        {
            return await Logs.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IList<AppLog>> GetLogsAsync(int skip, int take, DataRequest<AppLog> request)
        {
            IQueryable<AppLog> items = GetLogs(request);

            // Execute
            var records = await items.Skip(skip).Take(take)
                .AsNoTracking()
                .ToListAsync();

            return records;
        }

        public async Task<IList<AppLog>> GetLogKeysAsync(int skip, int take, DataRequest<AppLog> request)
        {
            IQueryable<AppLog> items = GetLogs(request);

            // Execute
            var records = await items.Skip(skip).Take(take)
                .Select(r => new AppLog
                {
                    Id = r.Id,
                })
                .AsNoTracking()
                .ToListAsync();

            return records;
        }


        private IQueryable<AppLog> GetLogs(DataRequest<AppLog> request)
        {
            IQueryable<AppLog> items = Logs;

            // Query
            if (!String.IsNullOrEmpty(request.Query))
            {
                items = items.Where(r => r.Message.Contains(request.Query.ToLower()));
            }

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

        public async Task<int> GetLogsCountAsync(DataRequest<AppLog> request)
        {
            IQueryable<AppLog> items = Logs;

            // Query
            if (!String.IsNullOrEmpty(request.Query))
            {
                items = items.Where(r => r.Message.Contains(request.Query.ToLower()));
            }

            // Where
            if (request.Where != null)
            {
                items = items.Where(request.Where);
            }

            return await items.CountAsync();
        }

        public async Task<int> CreateLogAsync(AppLog appLog)
        {
            appLog.CreateDate = DateTime.UtcNow;
            Entry(appLog).State = EntityState.Added;
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteLogsAsync(params AppLog[] logs)
        {
            Logs.RemoveRange(logs);
            return await SaveChangesAsync();
        }

        public async Task MarkAllAsReadAsync()
        {
            var items = await Logs.Where(r => !r.IsRead).ToListAsync();
            foreach (var item in items)
            {
                item.IsRead = true;
            }
            await SaveChangesAsync();
        }
    }
}
