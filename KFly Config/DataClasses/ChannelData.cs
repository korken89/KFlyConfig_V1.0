using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
    public class ChannelData
    {
        private UInt16[,] _channel = new UInt16[8, 3];
        public byte Role = 0;

        public UInt16[,] Channel
        {
            get { return _channel; }
            set { _channel = value; }
        }

        public ChannelData()
        {
        }

        public List<byte> ToBytes()
        {
            List<byte> data = new List<byte>();
            data.Add(Role);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    data.AddRange(UintToBytes(_channel[i, j]));
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
