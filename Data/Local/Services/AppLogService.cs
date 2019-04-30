using Data.Local.Common;
using Data.Local.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Local.Services
{
    public class AppLogService : LongServiceBase<AppLog>
    {
        protected override DbContextType DbContextType { get; } = DbContextType.AppLog;

        public AppLogService(DbContextFactory dbContextFactory) : base(dbContextFactory) { }

        public async Task MarkAllAsReadAsync()
        {
            using (var dbContext = GetDbContext())
            {
                var items = await dbContext.Set<AppLog>().Where(r => !r.IsRead).ToListAsync();
                foreach (var item in items)
                {
                    item.IsRead = true;
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
