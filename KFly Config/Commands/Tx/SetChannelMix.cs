using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    class SetChannelMix : KFlyCommand
    {
        private MixerData _data;

        public SetChannelMix(MixerData data) : base(Command.SetChannelMix)
        {
            _data = data;
        }

        protected override List<Byte> GetData()
        {
            return _data.ToBytes();
        }
    }
}
