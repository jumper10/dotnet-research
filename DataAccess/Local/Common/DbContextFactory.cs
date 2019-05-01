using CommonLibrary.Utils;
using DataAccess.Local.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Collections.Concurrent;

namespace DataAccess.Local.Common
{
    public class DbContextFactory
    {
        static readonly ConcurrentDictionary<DbContextType, string> _DbContexts;

        public void Migrate()
        {
            foreach(var db in _DbContexts)
            {
                using(var dbContext = GetDbContext(db.Key))
                {
                    dbContext.Database.Migrate();
                }
            }
        }

        static DbContextFactory()
        {
            _DbContexts = new ConcurrentDictionary<DbContextType, string>();
            _DbContexts[DbContextType.AppLog] =$"Filename ={PathUtil.CombinePath(PathUtil.CombineOnAppLocalPath("Data"),"app_log.bat")}";
            _DbContexts[DbContextType.App] = $"Filename ={PathUtil.CombinePath(PathUtil.CombineOnAppLocalPath("Data"), "app_data.bat")}";
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
    }
    #region Create Migaration Code Surport

    public class AppLogDbContextFactory : IDesignTimeDbContextFactory<AppLogDbContext>
    {
        public AppLogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppLogDbContext>();
            optionsBuilder.UseSqlite("Data Source=blog.db");

            return new AppLogDbContext(optionsBuilder.Options);
        }
    }
    public class AppDBContextFactory : IDesignTimeDbContextFactory<AppDBContext>
    {
        public AppDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            optionsBuilder.UseSqlite("Data Source=app_data.db");

            return new AppDBContext(optionsBuilder.Options);
        }
    }
    #endregion
    public enum DbContextType
    {
        AppLog,
        App
    }
}
