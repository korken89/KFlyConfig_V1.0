using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    class SetRCCalibration : KFlyCommand
    {
        private ChannelData _data;

        public SetRCCalibration(ChannelData data) : base(Command.SetRCCalibration)
        {
            _data = data;
        }

        protected override List<Byte> GetData()
        {
            return _data.ToBytes();
        }
    }
}
