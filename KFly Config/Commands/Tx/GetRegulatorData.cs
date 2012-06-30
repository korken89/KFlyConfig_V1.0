using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    class GetRegulatorData : KFlyCommand
    {

        private List<byte> _data;

        public GetRegulatorData() : base(Command.GetRegulatorData)
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
