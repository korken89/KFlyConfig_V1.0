using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace KFly_Config
{
    class TelemetryLink
    {
        private delegate void VoidCallbackType(int data);

        // Property variables
        private string _baudRate = string.Empty;
        private string _parity = string.Empty;
        private string _stopBits = string.Empty;
        private string _dataBits = string.Empty;
        private string _portName = string.Empty;

        private SerialPort comPort = new SerialPort();
        private KFlyConfig pointertoform;

        public enum State
        {
            Wait,
            GetDataCount,
            GetData,
            WaitForCRC
        };

        public State CurrentState = State.Wait;

        public string BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        public string PortName
        {
            get { return _portName; }
            set { _portName = value; }
        }

        public TelemetryLink(KFlyConfig from)
        {
            _baudRate = "115200";
            _parity = "None";
            _stopBits = "One";
            _dataBits = "8";
            _portName = string.Empty;
            pointertoform = from;

            

            // Add event handler
            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
        }

        public void SetPortNameValues(object obj)
        {
            foreach (string str in SerialPort.GetPortNames())
            {
                ((ComboBox)obj).Items.Add(str);
            }
        }

        public bool OpenPort()
        {
            try
            {
                if (comPort.IsOpen == true) comPort.Close();

                comPort.BaudRate = int.Parse(_baudRate);
                comPort.DataBits = int.Parse(_dataBits);
                comPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), _stopBits);
                comPort.Parity = (Parity)Enum.Parse(typeof(Parity), _parity);
                comPort.PortName = _portName;

                comPort.Open();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ClosePort()
        {
            if (comPort.IsOpen == true) comPort.Close();
        }

        public void SendData(KFlyCommand cmd)
        {
            byte[] data = cmd.ToByteArray();
            comPort.Write(data, 0, data.Length);
        }

        public void SendData(List<KFlyCommand> cmds)
        {
            foreach (KFlyCommand cmd in cmds)
            {
                SendData(cmd);
            }
        }

        private List<byte> _received = new List<byte>();
        private int _dataLeft = 0;
        private KFlyCommand.Command _command;

        private State _processWait(int data)
        {
            try
            {
                _command = (KFlyCommand.Command)data;
                _received.Clear();
                _received.Add((byte)data);
                return State.GetDataCount;
            }
            catch
            {
                return State.Wait;
            }
        }

        private State _processGetDataCount(int data)
        {
            _dataLeft = data;
            _received.Add((byte)data);
            if (_dataLeft > 0)
            {
                return State.GetData;
            }
            else
            {
                return State.WaitForCRC;
            }
        }

        private State _processGetData(int data)
        {
            _dataLeft--;
            _received.Add((byte)data);
            if (_dataLeft > 0)
            {
                return State.GetData;
            }
            else
            {
                return State.WaitForCRC;
            }
        }

        private State _processWaitForCRC(int data)
        {
            _received.Add((byte)data);
            KFlyCommand command = KFlyCommand.FromByteArray(_received);
            if (command != null)
            {
                if (command is Ping)
                {

                }

                else if (command is GetChannelMix)
                {

                }

                else if (command is GetRCCalibration)
                {

                }

                else if (command is GetRCValues)
                {

                }

                else if (command is GetRegulatorData)
                {

                }
            }

            return State.Wait;
        }

        private void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (comPort.BytesToRead > 0)
            {
                int data = comPort.ReadByte();

                switch (CurrentState)
                {
                    case State.Wait:
                        CurrentState = _processWait(data);
                        break;

                    case State.GetDataCount:
                        CurrentState = _processGetDataCount(data);
                        break;

                    case State.GetData:
                        CurrentState = _processGetData(data);
                        break;

                    case State.WaitForCRC:
                        CurrentState = _processWaitForCRC(data);
                        break;
                }
            }

            //pointertoform.appendToTextbox();
        }
    }
}
