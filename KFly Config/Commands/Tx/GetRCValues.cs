using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    public class GetRCValues : KFlyCommand
    {
        private List<byte> _data;

        public GetRCValues() : base(Command.GetRCValues)
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
