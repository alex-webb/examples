using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CodeExamples
{
    public class AsyncExamples
    {
        public static async Task ExampleAsync()
        {
            var result = await Task.Run(() => BusyWork());
            
            Console.WriteLine("Result: {0}, Task complete.", result);
        }

        public static string BusyWork() 
        {
            Thread.Sleep(2000);

            return "Finished sleeping";
        }

    }
}
 