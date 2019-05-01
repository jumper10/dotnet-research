using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    public interface IAppFolder
    {
        string AppLocalFolder { get; }
        string AppTemporaryFolder { get; }
    }
}
