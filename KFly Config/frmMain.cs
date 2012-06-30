using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KFly_Config
{
    public partial class KFlyConfig : Form
    {
        TelemetryLink _telLink;
        private bool _connected = false;

        public KFlyConfig()
        {
            InitializeComponent();
            _telLink = new TelemetryLink(this);
        }

        private void KFlyConfig_Load(object sender, EventArgs e)
        {
            _telLink.SetPortNameValues(comboBox1);
            comboBox2.SelectedIndex = 0;
        }

        private void closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string ByteToHex(byte[] comByte)
        {
            StringBuilder builder = new StringBuilder(comByte.Length * 3);

            foreach (byte data in comByte)
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));

            return builder.ToString().ToUpper();
        }

        public void appendToTextbox(byte[] msg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    appendToTextbox(msg);
                });
                return;
            } 

            richTextBox1.AppendText(ByteToHex(msg));
        }

        private RegulatorData _getRegulatorData()
        {
            const decimal dec2fp = 256; // 8 fraction bits
            RegulatorData data = new RegulatorData();

            // Decimal to 16 bit 8.8 fixedpoint. 16 bit fixed point to 2 bytes.
            data.Rate[0, 0] = (UInt16)(decimal.Round(rate_pkp.Value * dec2fp, 0));
            data.Rate[0, 1] = (UInt16)(decimal.Round(rate_pki.Value * dec2fp, 0));
            data.Rate[0, 2] = (UInt16)(decimal.Round(rate_pil.Value * dec2fp, 0));

            data.Rate[1, 0] = (UInt16)(decimal.Round(rate_rkp.Value * dec2fp, 0));
            data.Rate[1, 1] = (UInt16)(decimal.Round(rate_rki.Value * dec2fp, 0));
            data.Rate[1, 2] = (UInt16)(decimal.Round(rate_ril.Value * dec2fp, 0));

            data.Rate[2, 0] = (UInt16)(decimal.Round(rate_ykp.Value * dec2fp, 0));
            data.Rate[2, 1] = (UInt16)(decimal.Round(rate_yki.Value * dec2fp, 0));
            data.Rate[2, 2] = (UInt16)(decimal.Round(rate_yil.Value * dec2fp, 0));

            data.Attitude[0, 0] = (UInt16)(decimal.Round(attitude_pkp.Value * dec2fp, 0));
            data.Attitude[0, 1] = (UInt16)(decimal.Round(attitude_pki.Value * dec2fp, 0));
            data.Attitude[0, 2] = (UInt16)(decimal.Round(attitude_pil.Value * dec2fp, 0));

            data.Attitude[1, 0] = (UInt16)(decimal.Round(attitude_rkp.Value * dec2fp, 0));
            data.Attitude[1, 1] = (UInt16)(decimal.Round(attitude_rki.Value * dec2fp, 0));
            data.Attitude[1, 2] = (UInt16)(decimal.Round(attitude_ril.Value * dec2fp, 0));

            data.Attitude[2, 0] = (UInt16)(decimal.Round(attitude_ykp.Value * dec2fp, 0));
            data.Attitude[2, 1] = (UInt16)(decimal.Round(attitude_yki.Value * dec2fp, 0));
            data.Attitude[2, 2] = (UInt16)(decimal.Round(attitude_yil.Value * dec2fp, 0));

            return data;
        }

        private ChannelData _getChannelCalibrationData()
        {
            ChannelData data = new ChannelData();

            data.Role = (byte)cal1role.SelectedIndex;
            data.Role <<= 2;
            data.Role |= (byte)cal2role.SelectedIndex;
            data.Role <<= 2;
            data.Role |= (byte)cal3role.SelectedIndex;
            data.Role <<= 2;
            data.Role |= (byte)cal4role.SelectedIndex;

            data.Channel[0, 0] = (UInt16)(cal11.Value);
            data.Channel[0, 1] = (UInt16)(cal12.Value);
            data.Channel[0, 2] = (UInt16)(cal13.Value);

            data.Channel[1, 0] = (UInt16)(cal21.Value);
            data.Channel[1, 1] = (UInt16)(cal22.Value);
            data.Channel[1, 2] = (UInt16)(cal23.Value);

            data.Channel[2, 0] = (UInt16)(cal31.Value);
            data.Channel[2, 1] = (UInt16)(cal32.Value);
            data.Channel[2, 2] = (UInt16)(cal33.Value);

            data.Channel[3, 0] = (UInt16)(cal41.Value);
            data.Channel[3, 1] = (UInt16)(cal42.Value);
            data.Channel[3, 2] = (UInt16)(cal43.Value);

            data.Channel[4, 0] = (UInt16)(cal51.Value);
            data.Channel[4, 1] = (UInt16)(cal52.Value);
            data.Channel[4, 2] = (UInt16)(cal53.Value);

            data.Channel[5, 0] = (UInt16)(cal61.Value);
            data.Channel[5, 1] = (UInt16)(cal62.Value);
            data.Channel[5, 2] = (UInt16)(cal63.Value);

            data.Channel[6, 0] = (UInt16)(cal71.Value);
            data.Channel[6, 1] = (UInt16)(cal72.Value);
            data.Channel[6, 2] = (UInt16)(cal73.Value);

            data.Channel[7, 0] = (UInt16)(cal81.Value);
            data.Channel[7, 1] = (UInt16)(cal82.Value);
            data.Channel[7, 2] = (UInt16)(cal83.Value);

            return data;
        }

        private MixerData _getMixData()
        {
            MixerData data = new MixerData();

            data.Mix[0, 0] = ((byte)((sbyte)mix11.Value));
            data.Mix[0, 1] = ((byte)((sbyte)mix12.Value));
            data.Mix[0, 2] = ((byte)((sbyte)mix13.Value));
            data.Mix[0, 3] = ((byte)((sbyte)mix14.Value));

            data.Mix[1, 0] = ((byte)((sbyte)mix21.Value));
            data.Mix[1, 1] = ((byte)((sbyte)mix22.Value));
            data.Mix[1, 2] = ((byte)((sbyte)mix23.Value));
            data.Mix[1, 3] = ((byte)((sbyte)mix24.Value));

            data.Mix[2, 0] = ((byte)((sbyte)mix31.Value));
            data.Mix[2, 1] = ((byte)((sbyte)mix32.Value));
            data.Mix[2, 2] = ((byte)((sbyte)mix33.Value));
            data.Mix[2, 3] = ((byte)((sbyte)mix34.Value));

            data.Mix[3, 0] = ((byte)((sbyte)mix41.Value));
            data.Mix[3, 1] = ((byte)((sbyte)mix42.Value));
            data.Mix[3, 2] = ((byte)((sbyte)mix43.Value));
            data.Mix[3, 3] = ((byte)((sbyte)mix44.Value));

            data.Mix[4, 0] = ((byte)((sbyte)mix51.Value));
            data.Mix[4, 1] = ((byte)((sbyte)mix52.Value));
            data.Mix[4, 2] = ((byte)((sbyte)mix53.Value));
            data.Mix[4, 3] = ((byte)((sbyte)mix54.Value));

            data.Mix[5, 0] = ((byte)((sbyte)mix61.Value));
            data.Mix[5, 1] = ((byte)((sbyte)mix62.Value));
            data.Mix[5, 2] = ((byte)((sbyte)mix63.Value));
            data.Mix[5, 3] = ((byte)((sbyte)mix64.Value));

            data.Mix[6, 0] = ((byte)((sbyte)mix71.Value));
            data.Mix[6, 1] = ((byte)((sbyte)mix72.Value));
            data.Mix[6, 2] = ((byte)((sbyte)mix73.Value));
            data.Mix[6, 3] = ((byte)((sbyte)mix74.Value));

            data.Mix[7, 0] = ((byte)((sbyte)mix81.Value));
            data.Mix[7, 1] = ((byte)((sbyte)mix82.Value));
            data.Mix[7, 2] = ((byte)((sbyte)mix83.Value));
            data.Mix[7, 3] = ((byte)((sbyte)mix84.Value));

            return data;
        }

        private void conStartStop_Click(object sender, EventArgs e)
        {
            _telLink.PortName = comboBox1.Text;

            if (_connected)
            {
                _telLink.ClosePort();
                _connected = false;
                conStartStop.Text = "Connect";
            }
            else
            {
                if (_telLink.OpenPort())
                {
                    _connected = true;
                    conStartStop.Text = "Disconnect";
                    _telLink.SendData(new Ping());
                }
                else
                {
                    richTextBox1.AppendText("\nError\n");
                } 
            }
        }

        private void regSave_Click(object sender, EventArgs e)
        {
            if (_connected)
            {
                _telLink.SendData(new SetRegulatorData(_getRegulatorData()));
            }
            else
            {
                MessageBox.Show("You must first connect to the target.", "Connection Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void calStart_Click(object sender, EventArgs e)
        {
            if (_connected)
            {
                calStart.Enabled = false;
                calStop.Enabled = true;
                _telLink.SendData(new StartRCCalibration());
            }
            else
            {
                MessageBox.Show("You must first connect to the target.", "Connection Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }     
        }

        private void calStop_Click(object sender, EventArgs e)
        {
            if (_connected)
            {
                calStart.Enabled = true;
                calStop.Enabled = false;
                _telLink.SendData(new StopRCCalibration());
            }
            else
            {
                MessageBox.Show("You must first connect to the target.", "Connection Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void calCenters_Click(object sender, EventArgs e)
        {
            if (_connected)
            {
                _telLink.SendData(new CalibrateRCCenters());
            }
            else
            {
                MessageBox.Show("You must first connect to the target.", "Connection Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void calSave_Click(object sender, EventArgs e)
        {
            if (_connected)
            {
                int[] rolecheck = new int[4];
                rolecheck[0] = cal1role.SelectedIndex;
                rolecheck[1] = cal2role.SelectedIndex;
                rolecheck[2] = cal3role.SelectedIndex;
                rolecheck[3] = cal4role.SelectedIndex;

                for (int i = 0; i < 4; i++)
                {
                    if (rolecheck[i] == -1)
                    {
                        MessageBox.Show("All roles must be assigned.", "Role Error",
                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if ((rolecheck[i] == rolecheck[j]) && (i != j))
                        {
                            MessageBox.Show("Each role must only be used once.", "Role Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }

                _telLink.SendData(new SetRCCalibration(_getChannelCalibrationData()));
            }
            else
            {
                MessageBox.Show("You must first connect to the target.", "Connection Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void mixSave_Click(object sender, EventArgs e)
        {
            if (_connected)
            {
                _telLink.SendData(new SetChannelMix(_getMixData()));
            }
            else
            {
                MessageBox.Show("You must first connect to the target.", "Connection Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void saveFlash_Click(object sender, EventArgs e)
        {
            if (_connected)
            {
                _telLink.SendData(new SaveToFlash());
            }
            else
            {
                MessageBox.Show("You must first connect to the target.", "Connection Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_connected)
            {
                _telLink.SendData(new GetRegulatorData());
            }
            else
            {
                MessageBox.Show("You must first connect to the target.", "Connection Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_connected)
            {
                _telLink.SendData(new GetChannelMix());
            }
            else
            {
                MessageBox.Show("You must first connect to the target.", "Connection Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_connected)
            {
                _telLink.SendData(new GetRCCalibration());
            }
            else
            {
                MessageBox.Show("You must first connect to the target.", "Connection Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
