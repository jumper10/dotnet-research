using CommonLibrary.Utils;
using Data.Local.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Data.Local.Common
{
    public class DbContextFactory
    {
        static readonly ConcurrentDictionary<DbContextType, string> _DbContexts;

        static DbContextFactory()
        {
            _DbContexts = new ConcurrentDictionary<DbContextType, string>();
            _DbContexts[DbContextType.AppLog] = PathUtil.CombinePath(PathUtil.GetAppDataPath(), "app.bat"); ;
            _DbContexts[DbContextType.App] = PathUtil.CombinePath(PathUtil.GetAppDataPath(), "app_log.bat");
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

    public enum DbContextType
    {
        AppLog,
        App
    }
}
