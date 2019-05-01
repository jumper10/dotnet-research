using CommonLibrary;
using DataAccess.Local.Common;
using DataAccess.Local.Data;
using DataAccess.Local.Services;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class DataModule : Module
    {
        static DataModule()
        {
            SimpleIoc.Default.Register<IModule>(() => new DataModule(), typeof(DataModule).Name);
        }

        public override void Inital()
        {
            base.Inital();
            SimpleIoc.Default.Register<DbContextFactory>();
            SimpleIoc.Default.Register<AppLogService>();

            SimpleIoc.Default.Register<LongServiceBase<Music>>();
            SimpleIoc.Default.Register<LongServiceBase<Video>>();
        }
    }
}
