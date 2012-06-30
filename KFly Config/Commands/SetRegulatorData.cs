using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    class SetRegulatorData : KFlyCommand
    {
        private List<byte> _datalist;

        public SetRegulatorData(List<byte> stuff) : base(Command.SetRegulatorData)
        {
            _datalist = stuff;
        }

        protected override List<Byte> GetData()
        {
            return _datalist;
        }
    }
}
