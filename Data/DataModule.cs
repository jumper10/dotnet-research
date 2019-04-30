using CommonLibrary;
using Data.Local.Common;
using Data.Local.Data;
using Data.Local.Services;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
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
