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
            SimpleIoc.Default.Register<IModule>(()=>new ViewModelModule(), typeof(ViewModelModule).Name);
        }

        public override void Inital()
        {
            base.Inital();
        }
    }
}
