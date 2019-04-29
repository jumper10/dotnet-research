using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    public class AppExceptionCodes
    {
        private static readonly ConcurrentDictionary<string, string> _exceptionCodes = new ConcurrentDictionary<string, string>();


        public static readonly string DbConnectionNull = "10000";
        public static readonly string UnKnow = "-9999";

        static AppExceptionCodes()
        {
            _exceptionCodes.GetOrAdd(DbConnectionNull,"数据库连接为空！");
            _exceptionCodes.GetOrAdd(UnKnow, "数据库连接为空！");
        }

        public static string GetCodeMessage(string code)
        {
           if(_exceptionCodes.ContainsKey(code))
            {
                return _exceptionCodes[code];
            }
            return "未知错误码！";
        }

    }
}
