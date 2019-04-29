using Data.Local.Common;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_research
{
    class BootStrap
    {
        public void Run()
        {
            Config();
            Migrate();
            InitShell();
        }

        private void InitShell()
        {
           
        }

        private void Config()
        {
           
        }

        void Migrate()
        {
            var dbFactory = SimpleIoc.Default.GetInstance<DbContextFactory>();
            dbFactory.Migrate();
        }
    }
}
