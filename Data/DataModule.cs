using CommonLibrary;
using Data.Local.Common;
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
            SimpleIoc.Default.Register<IModule, DataModule>();
        }

        public override void Inital()
        {
            base.Inital();
            SimpleIoc.Default.Register<DbContextFactory>();
        }
    }
}
