using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Dashboard.ServiceBus
{
    public interface ILogMessage
    {
        void Log(string message);
    }
}
