using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemEkspertowyTarczykowski.ViewModels
{
    public static class Logger
    {
        public static void WriteLog(string messageToLog)
        {
            using (StreamWriter streamWriter = File.AppendText("Log.txt"))
            {
                streamWriter.WriteLine(messageToLog);
            }
        }
    }
}
