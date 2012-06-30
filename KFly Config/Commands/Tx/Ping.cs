using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    public class Ping : KFlyCommand
    {
        public Ping() : base(Command.Ping)
        {
        }
    }
}
