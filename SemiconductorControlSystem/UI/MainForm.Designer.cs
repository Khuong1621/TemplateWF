using System.Drawing;
using System.Windows.Forms;

namespace SemiconductorControlSystem.UI
{
    public partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnStart;
        private Button btnStop;
        private TextBox txtLog;
        private Label lblSensorValue;
        private Label lblSensorTime;
        private GroupBox grpDeviceStatus;
        private FlowLayoutPanel pnlDevices;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnStart = new Button();
            this.btnStop = new Button();
            this.txtLog = new TextBox();
            this.lblSensorValue = new Label();
            this.lblSensorTime = new Label();
            this.grpDeviceStatus = new GroupBox();
            this.pnlDevices = new FlowLayoutPanel();

            this.SuspendLayout();

            // btnStart
            this.btnStart.Location = new Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new Size(120, 50);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "START SYSTEM";
            this.btnStart.BackColor = Color.LightGreen;
            this.btnStart.FlatStyle = FlatStyle.Flat;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            // btnStop
            this.btnStop.Enabled = false;
            this.btnStop.Location = new Point(140, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(120, 50);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "STOP SYSTEM";
            this.btnStop.BackColor = Color.Salmon;
            this.btnStop.FlatStyle = FlatStyle.Flat;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);

            // lblSensorValue
            this.lblSensorValue.AutoSize = true;
            this.lblSensorValue.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            this.lblSensorValue.ForeColor = Color.DarkBlue;
            this.lblSensorValue.Location = new Point(12, 80);
            this.lblSensorValue.Name = "lblSensorValue";
            this.lblSensorValue.Size = new Size(140, 59);
            this.lblSensorValue.TabIndex = 2;
            this.lblSensorValue.Text = "0.0 C";

            // lblSensorTime
            this.lblSensorTime.AutoSize = true;
            this.lblSensorTime.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            this.lblSensorTime.Location = new Point(12, 145);
            this.lblSensorTime.Name = "lblSensorTime";
            this.lblSensorTime.Size = new Size(120, 19);
            this.lblSensorTime.TabIndex = 3;
            this.lblSensorTime.Text = "Last update: N/A";

            // grpDeviceStatus
            this.grpDeviceStatus.Controls.Add(this.pnlDevices);
            this.grpDeviceStatus.Location = new Point(12, 180);
            this.grpDeviceStatus.Name = "grpDeviceStatus";
            this.grpDeviceStatus.Size = new Size(760, 160);
            this.grpDeviceStatus.TabIndex = 4;
            this.grpDeviceStatus.TabStop = false;
            this.grpDeviceStatus.Text = "Realtime Equipment Status";

            // pnlDevices
            this.pnlDevices.Dock = DockStyle.Fill;
            this.pnlDevices.Location = new Point(3, 19);
            this.pnlDevices.Name = "pnlDevices";
            this.pnlDevices.Padding = new Padding(10);
            this.pnlDevices.Size = new Size(754, 138);
            this.pnlDevices.TabIndex = 0;

            // txtLog
            this.txtLog.Location = new Point(12, 350);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = ScrollBars.Vertical;
            this.txtLog.Size = new Size(760, 200);
            this.txtLog.TabIndex = 5;
            this.txtLog.BackColor = Color.Black;
            this.txtLog.ForeColor = Color.Lime;
            this.txtLog.Font = new Font("Consolas", 9F);

            // MainForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(784, 561);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.grpDeviceStatus);
            this.Controls.Add(this.lblSensorTime);
            this.Controls.Add(this.lblSensorValue);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "MainForm";
            this.Text = "Semiconductor Control Dashboard v1.0";
            this.grpDeviceStatus.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
