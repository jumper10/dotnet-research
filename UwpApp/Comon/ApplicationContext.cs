using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace UwpApp
{
    public class ApplicationContext
    {
        public static readonly Size WindowLaunchSize = new Size(801,532);
        public static readonly Color TitleBarBackgroud = ResourceUtil.GetResource<Color>("TitleBarBackgroud");
        public static readonly Color TitleBarForegroud = ResourceUtil.GetResource<Color>("TitleBarForegroud");
        public static readonly Color InactiveTitleBarBackgroud = ResourceUtil.GetResource<Color>("InactiveTitleBarBackgroud");
        public static readonly Color TitleBarButtonBackground = ResourceUtil.GetResource<Color>("TitleBarButtonBackground");
        public static readonly Color InactiveTitleBarButtonBackground = ResourceUtil.GetResource<Color>("InactiveTitleBarButtonBackground");
        public static readonly Color TitleBarButtonHoverBackgroud = ResourceUtil.GetResource<Color>("TitleBarButtonHoverBackgroud");

       
        
    }
}
