using System.Drawing;
using System.Windows.Forms;

namespace SemiconductorControlSystem.UI
{
    public partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel mainLayout;
        private FlowLayoutPanel topActionPanel;
        private Panel sensorValuePanel;
        private Label lblSensorValue;
        private Label lblSensorTime;
        private GroupBox grpDeviceStatus;
        private FlowLayoutPanel pnlDevices;
        private TextBox txtLog;
        private Button btnStart;
        private Button btnStop;

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
            this.mainLayout = new TableLayoutPanel();
            this.topActionPanel = new FlowLayoutPanel();
            this.btnStart = new Button();
            this.btnStop = new Button();
            this.sensorValuePanel = new Panel();
            this.lblSensorValue = new Label();
            this.lblSensorTime = new Label();
            this.grpDeviceStatus = new GroupBox();
            this.pnlDevices = new FlowLayoutPanel();
            this.txtLog = new TextBox();

            this.SuspendLayout();

            // Main Layout Container (3 rows: Controls, Devices, Logs)
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.mainLayout.RowCount = 3;
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F));
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            this.mainLayout.Dock = DockStyle.Fill;
            this.mainLayout.Padding = new Padding(15);
            this.mainLayout.BackColor = Color.FromArgb(30, 30, 35);

            // Row 1: Top Panel (Buttons + Sensor Value)
            this.topActionPanel.Dock = DockStyle.Top;
            this.topActionPanel.Height = 60;
            this.topActionPanel.BackColor = Color.Transparent;

            this.btnStart.Size = new Size(150, 45);
            this.btnStart.Text = "▶ START SYSTEM";
            this.btnStart.BackColor = Color.FromArgb(46, 204, 113);
            this.btnStart.ForeColor = Color.White;
            this.btnStart.FlatStyle = FlatStyle.Flat;
            this.btnStart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnStart.Cursor = Cursors.Hand;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            this.btnStop.Size = new Size(150, 45);
            this.btnStop.Text = "■ STOP SYSTEM";
            this.btnStop.BackColor = Color.FromArgb(231, 76, 60);
            this.btnStop.ForeColor = Color.White;
            this.btnStop.FlatStyle = FlatStyle.Flat;
            this.btnStop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnStop.Enabled = false;
            this.btnStop.Cursor = Cursors.Hand;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);

            this.topActionPanel.Controls.Add(this.btnStart);
            this.topActionPanel.Controls.Add(this.btnStop);

            this.sensorValuePanel.Dock = DockStyle.Fill;
            this.sensorValuePanel.BackColor = Color.Transparent;
            this.sensorValuePanel.Padding = new Padding(0, 5, 0, 0);

            this.lblSensorValue.AutoSize = true;
            this.lblSensorValue.Font = new Font("Consolas", 36F, FontStyle.Bold);
            this.lblSensorValue.ForeColor = Color.FromArgb(52, 152, 219);
            this.lblSensorValue.Location = new Point(0, 5);
            this.lblSensorValue.Text = "0.0 °C";

            this.lblSensorTime.AutoSize = true;
            this.lblSensorTime.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.lblSensorTime.ForeColor = Color.Gray;
            this.lblSensorTime.Location = new Point(5, 65);
            this.lblSensorTime.Text = "Status: OFFLINE";

            this.sensorValuePanel.Controls.Add(this.lblSensorValue);
            this.sensorValuePanel.Controls.Add(this.lblSensorTime);

            Panel topRowContainer = new Panel { Dock = DockStyle.Fill };
            topRowContainer.Controls.Add(this.sensorValuePanel);
            topRowContainer.Controls.Add(this.topActionPanel);
            this.mainLayout.Controls.Add(topRowContainer, 0, 0);

            // Row 2: Equipment Status Group
            this.grpDeviceStatus.Dock = DockStyle.Fill;
            this.grpDeviceStatus.ForeColor = Color.LightGray;
            this.grpDeviceStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.grpDeviceStatus.Text = " EQUIPMENT REAL-TIME MONITORING ";
            this.grpDeviceStatus.Padding = new Padding(10);

            this.pnlDevices.Dock = DockStyle.Fill;
            this.pnlDevices.AutoScroll = true;
            this.pnlDevices.BackColor = Color.FromArgb(40, 40, 45);
            this.grpDeviceStatus.Controls.Add(this.pnlDevices);
            this.mainLayout.Controls.Add(this.grpDeviceStatus, 0, 1);

            // Row 3: System Logs
            this.txtLog.Dock = DockStyle.Fill;
            this.txtLog.Multiline = true;
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = ScrollBars.Vertical;
            this.txtLog.BackColor = Color.FromArgb(20, 20, 25);
            this.txtLog.ForeColor = Color.FromArgb(180, 180, 180);
            this.txtLog.Font = new Font("Consolas", 9.5F);
            this.txtLog.BorderStyle = BorderStyle.None;
            this.mainLayout.Controls.Add(this.txtLog, 0, 2);

            // MainForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(900, 700);
            this.MinimumSize = new Size(600, 500);
            this.Controls.Add(this.mainLayout);
            this.Name = "MainForm";
            this.Text = "Semiconductor Control System v2.0 - Responsive Dashboard";
            this.BackColor = Color.FromArgb(30, 30, 35);
            this.mainLayout.ResumeLayout(false);
            this.mainLayout.PerformLayout();
            this.topActionPanel.ResumeLayout(false);
            this.sensorValuePanel.ResumeLayout(false);
            this.sensorValuePanel.PerformLayout();
            this.grpDeviceStatus.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
