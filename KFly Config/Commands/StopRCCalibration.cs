using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    public class StopRCCalibration : KFlyCommand
    {
        public StopRCCalibration() : base(Command.StopRCCalibration)
        {
        }
    }
}
