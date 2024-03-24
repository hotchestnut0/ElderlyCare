using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElderlyCareApp.Utils
{
    internal static class ProcessUtil
    {
        public static Process StartProcessShellExecute(string url)
        {
            Process process = new Process(); 
            process.StartInfo = new()
            {
                FileName = url,
                UseShellExecute = true
            };
            process.Start();

            return process;
        }
    }
}
