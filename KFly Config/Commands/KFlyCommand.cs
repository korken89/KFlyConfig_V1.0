using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KFly_Config
{
   public abstract class KFlyCommand
    {
        public enum Command
        {
            None = 0,
            Ping = 1,
            SaveToFlash,
            GetRegulatorData,
            SetRegulatorData,
            GetChannelMix,
            SetChannelMix,
            StartRCCalibration,
            StopRCCalibration,
            CalibrateRCCenters,
            GetRCCalibration,
            SetRCCalibration,
            GetRCValues
        };

        private Command _cmd;
        private bool _ack = false;

        public Command Cmd
        {
            get { return _cmd; }
        }

       public Boolean ACK
        {
            get { return _ack; }
            set { _ack = value; }
        }

        public KFlyCommand(Command cmd)
        {
            _cmd = cmd;
        }

        protected virtual List<Byte> GetData()
        {
            return new List<byte>();
        }

        public virtual void SetData(List<byte> data)
        {
            
        }

        public static KFlyCommand FromByteArray(List<byte> data)
        {
            if (data.Count < 3)
            {
                return null;
            }
            byte crc = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);
            if (crc8(data) == crc)
            {
                try
                {
                    Command type = (Command)data[0];
                    data.RemoveRange(0, 2);
                    KFlyCommand command = null;
                    switch (type)
                    {
                        case Command.Ping:
                            command = new Ping();
                            break;
                        case Command.GetChannelMix:
                            command = new GetChannelMix();
                            break;
                        case Command.GetRCCalibration:
                            command = new GetRCCalibration();
                            break;
                        case Command.GetRCValues:
                            command = new GetRCValues();
                            break;
                        case Command.GetRegulatorData:
                            command = new GetRegulatorData();
                            break;

                    }
                    command.SetData(data);
                    return command;
                }
                catch
                {
                    return null;
                }
            }
            else
                return null;
        }

        public byte[] ToByteArray()
        {
            List<byte> all = new List<byte>();
            all.Add((byte)Cmd);
            if (ACK)
            {
                all[0] |= 0x80;
            }
            List<byte> data = GetData();
            all.Add((byte)data.Count);
            all.AddRange(data);
            all.Add(crc8(all));
            return all.ToArray();
        }

        private static byte crc8(List<byte> message)
        {
            byte remainder = 0;	

            for (int bte = 0; bte < message.Count; ++bte)
            {
                remainder ^= (message[bte]);
                for (byte bit = 8; bit > 0; --bit)
                {
                    if ((remainder & 0x80) > 0)
                    {
                        remainder = (byte)((remainder << 1) ^ 0xD8);
                    }
                    else
                    {
                        remainder = (byte)(remainder << 1);
                    }
                }
            }

            return (remainder);
        }
    }
}
