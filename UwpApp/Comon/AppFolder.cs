using CommonLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace UwpApp.Comon
{
    public class AppFolder : IAppFolder
    {
        public string AppLocalFolder => ApplicationData.Current.LocalFolder.Path;

        public string AppTemporaryFolder => ApplicationData.Current.TemporaryFolder.Path;
    }
}
