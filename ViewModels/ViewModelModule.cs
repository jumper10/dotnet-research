using CommonLibrary;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class ViewModelModule:Module
    {
        static ViewModelModule()
        {
            SimpleIoc.Default.Register<IModule, ViewModelModule>();
        }

        public override void Inital()
        {
            base.Inital();
        }
    }
}
