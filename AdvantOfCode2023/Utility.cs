using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvantOfCode2023
{
    public static class Utility
    {
        public static void PrintList(object list)
        {
            Console.WriteLine(JsonConvert.SerializeObject(list, Formatting.Indented));
        }
    }
}
