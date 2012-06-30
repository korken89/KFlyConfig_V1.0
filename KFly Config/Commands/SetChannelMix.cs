using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    class SetChannelMix : KFlyCommand
    {
        private List<byte> _datalist;

        public SetChannelMix(List<byte> stuff) : base(Command.SetChannelMix)
        {
            _datalist = stuff;
        }

        protected override List<Byte> GetData()
        {
            return _datalist;
        }
    }
}
