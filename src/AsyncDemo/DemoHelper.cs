namespace AsyncDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DemoHelper
    {
        public static List<string> GetStringList()
        {
            return Enumerable.Range(0, 10000).Select(i => Guid.NewGuid().ToString("N")).ToList();
        }
    }
}
