using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    public class RegulatorData
    {
        private UInt16[,] _rate = new UInt16[3, 3];

        public UInt16[,] Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }
        private UInt16[,] _attitude = new UInt16[3, 3];

        public UInt16[,] Attitude
        {
            get { return _attitude; }
            set { _attitude = value; }
        }

        public RegulatorData()
        {

        }

        public List<byte> ToBytes()
        {
            List<byte> data = new List<byte>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    data.AddRange(UintToBytes(_rate[i,j]));
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    data.AddRange(UintToBytes(_attitude[i, j]));
                }
            }
            return data;
        }

        private byte[] UintToBytes(UInt16 value)
        {
            byte[] bytes = new byte[2];
            bytes[0] = (byte)value;
            bytes[1] = (byte)(value >> 8);
            return bytes;
        }

    }
}
