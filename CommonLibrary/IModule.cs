using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{

    /// <summary>
    /// Need Used Static Constructor Regist Self
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Module Init Function
        /// </summary>
        void Inital();
        void OnLoad();
        void UnLoad();
    }
}
