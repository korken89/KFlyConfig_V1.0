using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    class SetRCCalibration : KFlyCommand
    {
        private List<byte> _datalist;

        public SetRCCalibration(List<byte> stuff) : base(Command.SetRCCalibration)
        {
            _datalist = stuff;
        }

        protected override List<Byte> GetData()
        {
            return _datalist;
        }
    }
}
