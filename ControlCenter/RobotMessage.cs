using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCenter
{
    internal class RobotMessage
    {
        public string CommandName { get; set; }
        public string[] Parameters { get; set; }

        public RobotMessage(string dataReceived)
        {
            string[] tokens = dataReceived.Split(':');
            CommandName = tokens[0];
            Parameters = tokens.Length > 1 ? tokens[1].Split('|') : Array.Empty<string>();
        }
    }
}