using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IdleMove
{
    public partial class Form1 : Form
    {
        private const string ConfigFileName = "config.ini";
        private const string EnableConfigItem = "Enable";
        private const string EnableHotKeyConfigItem = "EnableHotKey";
        private const string EnableScheduleConfigItem = "EnableSchedule";
        private const string IdleDelayConfigItem = "IdleDelay";
        private const string FromTimeConfigItem = "FromTime";
        private const string ToTimeConfigItem = "ToTime";

        private Hotkeys _ghk;
        private int _maxX = SystemInformation.VirtualScreen.Width;
        private int _maxY = SystemInformation.VirtualScreen.Height;
        private int _idleTime = 60;
        private uint _lastIdleTime;
        private bool _hotKeyEnabled;
        private bool _scheduleEnabled;
        private readonly Timer _systemTimer = new Timer();
        private int _cursorMoveDelay;
        private readonly Random _rnd = new Random();
        private DateTime _fromTime, _toTime;

        public Form1()
        {
            InitializeComponent();
            MaximizeBox = false;
            var autoEnable = false;
            try
            {
                var configLines = File.ReadAllLines(ConfigFileName);
                foreach (var line in configLines)
                {
                    var l = line.Split('=');
                    if (l.Length < 2) continue;

                    l[0] = l[0].Trim().ToUpper();
                    if (l[0] == EnableConfigItem.ToUpper())
                    {
                        int.TryParse(l[1], out var value);
                        autoEnable = value > 0;
                    }
                    else if (l[0] == EnableHotKeyConfigItem.ToUpper())
                    {
                        int.TryParse(l[1], out var value);
                        _hotKeyEnabled = value > 0;
                    }
                    else if (l[0] == EnableScheduleConfigItem.ToUpper())
                    {
                        int.TryParse(l[1], out var value);
                        _scheduleEnabled = value > 0;
                    }
                    else if (l[0] == IdleDelayConfigItem.ToUpper())
                    {
                        int.TryParse(l[1], out _idleTime);
                    }
                    else if (l[0] == FromTimeConfigItem.ToUpper())
                    {
                        DateTime.TryParse(l[1], out _fromTime);
                    }
                    else if (l[0] == ToTimeConfigItem.ToUpper())
                    {
                        DateTime.TryParse(l[1], out _toTime);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Can't load settings: " + exception);
            }

            textBox_time.Text = _idleTime.ToString();
            textBox_from.Text = _fromTime.ToShortTimeString();
            textBox_to.Text = _toTime.ToShortTimeString();

            _cursorMoveDelay = _rnd.Next(0, _idleTime);

            _systemTimer.Enabled = false;
            _systemTimer.Interval = 1000;
            _systemTimer.Tick += TimerEvent;

            checkBox_enableHotKey.Checked = _hotKeyEnabled;
            checkBox_enableSchedule.Checked = _scheduleEnabled;

            checkBox_enable.Checked = autoEnable;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (checkBox_enable.Checked)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var configText = "";
            configText += EnableConfigItem + "=" + (_systemTimer.Enabled == true ? 1 : 0) + Environment.NewLine;
            configText += EnableHotKeyConfigItem + "=" + (_hotKeyEnabled == true ? 1 : 0) + Environment.NewLine;
            configText += EnableScheduleConfigItem + "=" + (_scheduleEnabled == true ? 1 : 0) + Environment.NewLine;
            configText += IdleDelayConfigItem + "=" + _idleTime + Environment.NewLine;
            configText += FromTimeConfigItem + "=" + _fromTime.ToShortTimeString() + Environment.NewLine;
            configText += ToTimeConfigItem + "=" + _toTime.ToShortTimeString() + Environment.NewLine;
            try
            {
                File.WriteAllText(ConfigFileName, configText);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Can't save settings: " + exception);
            }
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void CheckBox_enable_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_enable.Checked)
            {
                // start TimerEvent
                _systemTimer.Enabled = true;


                // register hotkey
                if (_hotKeyEnabled)
                {
                    _ghk = new Hotkeys(Hotkeys.Constants.NOMOD, Keys.Escape, this);
                    _ghk.Register();
                }

                // open form window
                textBox_time.Enabled = false;
            }
            else
            {
                // stop timer
                _systemTimer.Enabled = false;

                // unregister hotkey
                if (_hotKeyEnabled) _ghk.Unregister();

                // minimize form
                textBox_time.Enabled = true;
                notifyIcon1.Text = "Inactive";
            }
        }

        private void CheckBox_enableHotKey_CheckedChanged(object sender, EventArgs e)
        {
            _hotKeyEnabled = checkBox_enableHotKey.Checked;

            if (_systemTimer.Enabled)
            {
                if (_hotKeyEnabled)
                {
                    _ghk = new Hotkeys(Hotkeys.Constants.NOMOD, Keys.Escape, this);
                    _ghk.Register();
                }
                else
                {
                    if (_hotKeyEnabled) _ghk.Unregister();
                }
            }
        }

        private void CheckBox_enableSchedule_CheckedChanged(object sender, EventArgs e)
        {
            _scheduleEnabled = checkBox_enableSchedule.Checked;
            textBox_from.Enabled = !_scheduleEnabled;
            textBox_to.Enabled = !_scheduleEnabled;
        }

        private void TextBox_time_Leave(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox_time.Text, out _idleTime)) textBox_time.Text = _idleTime.ToString();

            _cursorMoveDelay = _idleTime;
        }

        private void TextBox_from_Leave(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(textBox_from.Text, out _fromTime)) textBox_from.Text = _fromTime.ToShortTimeString();

            if (_fromTime.TimeOfDay >= _toTime.TimeOfDay)
            {
                _toTime = _fromTime.AddMinutes(1);
                textBox_to.Text = _toTime.ToShortTimeString();
            }
        }

        private void TextBox_to_Leave(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(textBox_to.Text, out _toTime)) textBox_to.Text = _toTime.ToShortTimeString();

            if (_toTime.TimeOfDay <= _fromTime.TimeOfDay)
            {
                _fromTime = _toTime.Subtract(new TimeSpan(0, 1, 0));
                textBox_from.Text = _fromTime.ToShortTimeString();
            }
        }

        private void TimerEvent(object sender, EventArgs args)
        {
            if (_scheduleEnabled)
            {
                var now = DateTime.Now;

                if (now.TimeOfDay < _fromTime.TimeOfDay || now.TimeOfDay > _toTime.TimeOfDay) return;
            }

            var currentIdleTime = GetIdleTime();

            if (currentIdleTime < _lastIdleTime)
            {
                _cursorMoveDelay = _idleTime;
                _lastIdleTime = currentIdleTime;
            }

            if (currentIdleTime - _lastIdleTime > _cursorMoveDelay * 1000)
            {
                // move mouse
                var _toX = _rnd.Next(0, _maxX - 1);
                var _toY = _rnd.Next(0, _maxY - 1);
                MoveCursor(_toX, _toY);
                // refresh timer delay random
                _cursorMoveDelay = _rnd.Next(0, _idleTime - 1);
                _lastIdleTime = currentIdleTime;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Hotkeys.Constants.WM_HOTKEY_MSG_ID)
            {
                _systemTimer.Enabled = false;

                if (_hotKeyEnabled) _ghk.Unregister();

                Invoke((MethodInvoker)delegate
                {
                    checkBox_enable.Checked = false;
                    notifyIcon1.Text = "Inactive";
                    Show();
                    notifyIcon1.Visible = false;
                    WindowState = FormWindowState.Normal;
                });
            }

            base.WndProc(ref m);
        }

        private static uint GetIdleTime()
        {
            var lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return (uint)Environment.TickCount - lastInPut.dwTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

            [MarshalAs(UnmanagedType.U4)] public uint cbSize;
            [MarshalAs(UnmanagedType.U4)] public uint dwTime;
        }

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        private void MoveCursor(int x, int y)
        {
            Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(x, y);
        }
    }
}
