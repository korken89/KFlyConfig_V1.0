using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    public class MixerData
    {
        private byte[,] _mix = new byte[8, 4];

        public byte[,] Mix
        {
            get { return _mix; }
            set { _mix = value; }
        }

        public MixerData()
        {

        }

        public List<byte> ToBytes()
        {
            List<byte> data = new List<byte>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    data.Add(_mix[i,j]);
                }
            }

            return data;
        }
    }
}
