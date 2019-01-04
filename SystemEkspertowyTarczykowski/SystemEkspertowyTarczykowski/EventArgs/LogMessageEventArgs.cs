using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemEkspertowyTarczykowski.EventArgs
{
    class LogMessageEventArgs : System.EventArgs
    {
        public string Message { get; private set; }
        public LogMessageEventArgs(string message)
        {
            Message = message;
        }
    }
}
