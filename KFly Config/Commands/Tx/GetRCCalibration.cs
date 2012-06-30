using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    public class GetRCCalibration : KFlyCommand
    {
        private List<byte> _data;

        public GetRCCalibration() : base(Command.GetRCCalibration)
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
