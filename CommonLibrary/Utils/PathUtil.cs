using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonLibrary.Utils
{
    public class PathUtil
    {
        public static string GetAppPath()
        {
            return AppContext.BaseDirectory;
        }

        public static string GetAppDataPath()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Data");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public static string CombinePath(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public static string CombineOnAppPath(params string[] paths)
        {
            var path = Path.Combine(GetAppPath(), Path.Combine(paths));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
        public static string CombineOnAppDataPath(params string[] paths)
        {
            var path = Path.Combine(GetAppDataPath(), Path.Combine(paths));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

    }
}
