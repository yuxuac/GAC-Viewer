using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GACViewer
{
    public class Util
    {
        public static string Execute(string fileName, string args)
        {
            using (Process process = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = fileName;
                startInfo.Arguments = args;

                startInfo.UseShellExecute = false;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.RedirectStandardInput = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.CreateNoWindow = true;

                process.StartInfo = startInfo;
                process.Start();
                while (process.HasExited)
                {
                    Thread.Sleep(1000);
                }
                string strOutput = process.StandardOutput.ReadToEnd();
                string errOutput = process.StandardError.ReadToEnd();
                string output = errOutput.ToString() + strOutput.ToString();
                return output;
            }
        }
    }
}
