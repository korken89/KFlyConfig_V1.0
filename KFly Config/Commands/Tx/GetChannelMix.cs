using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    public class GetChannelMix : KFlyCommand
    {
        private List<byte> _data;

        public GetChannelMix() : base(Command.GetChannelMix)
        {
        }

        public override void SetData(List<byte> data)
        {
            _data = data;
        }

        public List<byte> Data
        {
            get { return _data; }
        }
    }
}
