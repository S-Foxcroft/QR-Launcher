using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace QR_Launcher_Process_Wrapper
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 1)
            {
                byte[] data = Convert.FromBase64String(args[0]);
                string[] path = Encoding.UTF8.GetString(data).Split('\t');
                if (!File.Exists(path[0])) return -1;
                Process p;
                ProcessStartInfo psi = new ProcessStartInfo()
                {
                    FileName = path[0],
                    Arguments = path.Length == 2 ? path[1] : ""
                };
                p = new Process() { StartInfo = psi };
                p.Start();
                p.WaitForExit();
                return p.ExitCode;
            }
            else return -1;
        }
    }
}
