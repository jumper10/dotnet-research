using CommonLibrary.Utils;
using Data.Local.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Data.Local.Common
{
    public class DbContextFactory: IDesignTimeDbContextFactory<AppDBContext>,
        IDesignTimeDbContextFactory<AppLogDbContext>
    {
        static readonly ConcurrentDictionary<DbContextType, string> _DbContexts;

        public void Migrate()
        {
            foreach(var db in _DbContexts)
            {
                using(var dbContext = GetDbContext(db.Key))
                {
                    dbContext.Database.EnsureCreated();
                    dbContext.Database.Migrate();
                }
            }
        }

        static DbContextFactory()
        {
            _DbContexts = new ConcurrentDictionary<DbContextType, string>();
            _DbContexts[DbContextType.AppLog] =$"Filename = app_log.bat";
            _DbContexts[DbContextType.App] = $"Filename =app_data.bat";
        }

        public string GetDataBaseLocation(DbContextType contextType = DbContextType.App)
        {
            if (_DbContexts.ContainsKey(contextType))
            {
                return _DbContexts[contextType];
            }
            return null;
        }
        public DbContext GetDbContext(DbContextType contextType = DbContextType.App)
        {
            switch (contextType)
            {
                case DbContextType.App:
                    return new AppDBContext(_DbContexts[DbContextType.App]);
                case DbContextType.AppLog:
                    return new AppLogDbContext(_DbContexts[DbContextType.AppLog]);
                default:
                    return new AppDBContext(_DbContexts[DbContextType.App]);
            }
        }

        public AppDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            optionsBuilder.UseSqlite("Data Source=app_data.db");

            return new AppDBContext(optionsBuilder.Options);
        }

        AppLogDbContext IDesignTimeDbContextFactory<AppLogDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppLogDbContext>();
            optionsBuilder.UseSqlite("Data Source=blog.db");

            return new AppLogDbContext(optionsBuilder.Options);
        }
    }

    public enum DbContextType
    {
        AppLog,
        App
    }
}
