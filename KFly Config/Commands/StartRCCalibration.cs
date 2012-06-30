using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    public class StartRCCalibration : KFlyCommand
    {
        public StartRCCalibration() : base(Command.StartRCCalibration)
        {
        }
    }
}
