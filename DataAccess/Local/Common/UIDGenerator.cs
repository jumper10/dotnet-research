using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Local.Common
{
    static class UIDGenerator
    {
        static private readonly DateTime DateSeed = DateTime.Parse("2019/04/18");

        static public long Next(int prefix = 1)
        {
            return (long)(DateTime.UtcNow - DateSeed).TotalMilliseconds + prefix * 100000000000;
        }
    }
}
