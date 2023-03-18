using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Gma.System.MouseKeyHook;
using System.IO;
using System.Windows.Input;
using WindowsFormsApp4;
using System.Timers;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using UIAutomationClient;
using System;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Windows.Automation;
using TreeScope = System.Windows.Automation.TreeScope;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Web;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private GlobalKeyboardHook _globalKeyboardHook;

        System.Timers.Timer t;
        int h, m, s;
        string appPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetCursorPos(int X, int Y);

        private IKeyboardMouseEvents m_Events;

        void DisableGetFrontWindow()
        {
            LogEvent("GetFrontWindow = OFF");
        }

        void EnableGetFrontWindow()
        {
            LogEvent("GetFrontWindow = ON");
        }

        void LogEvent(string message)
        {
            string temp;

            temp = string.Format(message);
            appfront.AppendText(temp + Environment.NewLine);
        }

        void MouseMessage(string message)
        {
            string temp;

            temp = string.Format(message);
            textBox_AllLog.Text = temp;
        }

        void FirefoxUrlMessage(string message)
        {
            string temp;

            temp = string.Format(message);
            textBox_ActiveBrowserWindowURL.AppendText("firefox URL found: " + message + Environment.NewLine);

        }

        void frontwindow(string message)
        {
            string temp;

            temp = string.Format(message);
            appfront.AppendText(temp + Environment.NewLine);
        }
        private void OnMouseMove(object sender, EventArgs e)
        {

            if (timerOn)
            {
                MouseEventArgs me = e as MouseEventArgs;
                if (me != null)
                {
                    //toolStripStatusLabel_CountMouse.Text = string.Format("[{0},{1}]", me.X, me.Y);
                    //LogMessage($"X: {me.X} | Y: {me.Y}");
                    activity_percent = 1;   // register activity detected in this 9Sec Slice
                }

            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;
            m_Events.MouseMove += M_Events_MouseMove;
        }

        private void Unsubscribe()
        {
            if (m_Events == null) return;
            m_Events.MouseMove -= M_Events_MouseMove;
            m_Events.Dispose();
            m_Events = null;
        }

        private void M_Events_MouseMove(object sender, MouseEventArgs e)
        {
            if (timerOn)
            {
                GetMousePosition(e.X, e.Y);
                activity_percent = 1;
            }
        }
        //timer on
        //mouse track
        //keyboard track
        //activity generate




        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Unsubscribe();
            t.Stop();
        }

        private void Form1_Load_1(object sender, System.EventArgs e)
        {
            CreateDirectory();
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
            Unsubscribe();
            Subscribe(Hook.GlobalEvents());
            t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += OnTimeEvent;
        }

        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                s += 1;
                SendActivity(s);
                if (s == 60)
                {
                    s = 0;
                    m += 1;
                }
                if (m == 60)
                {
                    m = 0;
                    h += 1;
                }
                label1.Text = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
            }


                ));
        }
        bool send = false;
        void SendActivity(int time)
        {
            if (time % 10 == 0)
            {
                send = true;
            }
            send = false;
        }
        void ReturnApps()
        {
            // This is a new Process Array populated with All the Processes Currently Running.
            List<Process> processlist = new List<Process>();

            foreach (var process in Process.GetProcesses())
            {
                processlist.Add(process);

                ////If the DNS Suffix is Empty remove it from the Interfaces List.
                //if (string.IsNullOrEmpty(process.MainWindowTitle))
                //{
                //    processlist.Remove(process);
                //}
            }

            // This message is the start of the output of the List to the Console.
            frontwindow("Current Process List: \n");
            // For reach Process in 'processlist'
            foreach (Process process in processlist)
            {
                // Log the Process name and ID.
                LogEvent($"Process: {process.ProcessName}ID: {process.Id}");
            }
            // Just to see how many Processes are running print out the length of the array.
            frontwindow($"Current Process Count: {processlist.ToArray().Length}");
        }

        bool timerOn = false;

        private void button1_Click(object sender, System.EventArgs e)
        {


        }
        string title = "title";
        string temp = "";
        string recieved = "";
        List<int> firefoxProcesses = new List<int>();
        List<int> chromeProcesses = new List<int>();
        List<int> iExplorerProcesses = new List<int>();
        List<string> edgeurls = new List<string>();
        List<string> Firefoxurls = new List<string>();
        List<string> Chromeurls = new List<string>();
        List<string> Braveurls = new List<string>();


        Thread thread1;
        Thread thread2;
        Thread thread3;
        Thread thread4;
        void Start()
        {

            try
            {
                t.Start();
                timer10_min.Enabled = true;
                timer2min.Enabled = true;
                timer_10sec.Enabled = true;
                
                CheckScreen();

                var windowTitle = Frontwindow.GetActiveWindowTitle();
                if (windowTitle != null)
                {
                    if (windowTitle.ToLower().Contains("firefox"))
                    {
                        thread2 = new Thread(new ThreadStart(FirefoxUrl));
                        thread2.IsBackground = true;
                        thread2.Start();
                    }
                    if (windowTitle.ToLower().Contains("edge"))
                    {
                        thread1 = new Thread(new ThreadStart(SetEdgeUrl));
                        thread1.IsBackground = true;
                        thread1.Start();
                        
                        //SetEdgeUrl();
                    }
                    if (windowTitle.ToLower().Contains("chrome"))
                    {
                        thread3 = new Thread(new ThreadStart(GetChromeUrls));
                        thread3.IsBackground = true;
                        thread3.Start();

                        //SetEdgeUrl();
                    }
                    if (windowTitle.ToLower().Contains("brave"))
                    {
                        thread4 = new Thread(new ThreadStart(GetBraveUrls));
                        thread4.IsBackground = true;
                        thread4.Start();

                        //SetEdgeUrl();
                    }


                    recieved = string.Format(windowTitle, "'");
                    if (recieved != title)
                    {

                        temp = recieved;
                        title = recieved;
                        if (temp == "")
                            temp = "_";

                        frontwindow(temp);
                    }
                }
            }
            catch (Exception)
            {

            }

        }
        //void FirefoxUrl()
        //{

        //    Process[] procsFirefox = Process.GetProcessesByName("Firefox");
        //    foreach (Process Firefox in procsFirefox)
        //    {
        //        the chrome process must have a window
        //        if (Firefox.MainWindowHandle == IntPtr.Zero)
        //        {
        //            continue;
        //        }

        //        find the automation element
        //        AutomationElement elm = AutomationElement.FromHandle(Firefox.MainWindowHandle);
        //        AutomationElement elmUrlBar = elm.FindFirst(TreeScope.Descendants,
        //          new PropertyCondition(AutomationElement.NameProperty, "Search with Google or enter address"));

        //        if it can be found, get the value from the URL bar
        //        if (elmUrlBar != null)
        //        {
        //            AutomationPattern[] patterns = elmUrlBar.GetSupportedPatterns();
        //            if (patterns.Length > 0)
        //            {
        //                ValuePattern val = (ValuePattern)elmUrlBar.GetCurrentPattern(ValuePattern.Pattern);
        //                FirefoxNewValue = val.Current.Value;

        //                if (FirefoxOldValue != FirefoxNewValue && FirefoxNewValue.StartsWith("https://"))
        //                {
        //                    FirefoxUrlMessage(FirefoxNewValue);
        //                    textBox_ActiveBrowserWindowURL.AppendText("firefox URL found: " + FirefoxNewValue + Environment.NewLine);
        //                    FirefoxOldValue = FirefoxNewValue;
        //                }
        //            }
        //        }

        //    }
        //}
        //string MicrosoftEdgeUrl()
        //{

        //Process[] edgeprocesses = Process.GetProcessesByName("msedge");
        //foreach (Process process in edgeprocesses)
        //{
        //    // the chrome process must have a window
        //    if (process.MainWindowHandle != IntPtr.Zero)
        //    {
        //        if (process == null)
        //            throw new ArgumentNullException("process");
        //        if (process.MainWindowHandle == IntPtr.Zero)
        //            return null;
        //        AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
        //        if (element == null)
        //            return null;
        //        AutomationElement edit = element.FindFirst(TreeScope.Subtree,
        //             new AndCondition(
        //                  new PropertyCondition(AutomationElement.NameProperty, "address and search bar", System.Windows.Automation.PropertyConditionFlags.IgnoreCase),
        //                  new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit)));
        //        if (edit != null)
        //        {
        //            var i = ((ValuePattern)edit.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string;
        //            return i;
        //        }
        //        else
        //        {
        //            return "";
        //        }


        //    }
        //}
        //return "";
        // }
        void FirefoxUrl()
        {

            Process[] procsFirefox = Process.GetProcessesByName("Firefox");
            foreach (Process Firefox in procsFirefox)
            {
                // the chrome process must have a window
                if (Firefox.MainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }

                // find the automation element
                AutomationElement elm = AutomationElement.FromHandle(Firefox.MainWindowHandle);
                AutomationElement elmUrlBar = elm.FindFirst(TreeScope.Descendants,
                  new PropertyCondition(AutomationElement.NameProperty, "Search with Google or enter address"));

                // if it can be found, get the value from the URL bar
                if (elmUrlBar != null)
                {
                    AutomationPattern[] patterns = elmUrlBar.GetSupportedPatterns();
                    if (patterns.Length > 0)
                    {
                        ValuePattern val = (ValuePattern)elmUrlBar.GetCurrentPattern(ValuePattern.Pattern);
                        var i = val.Current.Value as string;
                        if (i != "" && i.StartsWith("https://"))
                        {

                            var uri = new Uri(i);
                            string path = uri.GetLeftPart(UriPartial.Path);

                            if (Firefoxurls.Count() == 0 || Firefoxurls.Last() != path)
                            {
                                Firefoxurls.Add(path);
                            }
                            else
                                continue;
                        }
                        else
                            continue;
                    }
                }

            }
        }


        public void SetEdgeUrl()
        {

            string oldvalue = "";
            while (true)
            {
                Process[] edgeprocesses = Process.GetProcessesByName("msedge");
                foreach (Process process in edgeprocesses)
                {
                    // the chrome process must have a window
                    if (process.MainWindowHandle != IntPtr.Zero)
                    {
                        if (process == null)
                            throw new ArgumentNullException("process");
                        if (process.MainWindowHandle != IntPtr.Zero)
                        {
                            AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
                            if (element != null)
                            {
                                AutomationElement edit = element.FindFirst(TreeScope.Subtree,
                                     new AndCondition(
                                          new PropertyCondition(AutomationElement.NameProperty, "address and search bar",
                                          System.Windows.Automation.PropertyConditionFlags.IgnoreCase),
                                          new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit)));
                                if (edit != null)
                                {
                                    var i = ((ValuePattern)edit.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string;
                                    
                                    if (i != "" && i.StartsWith("https://"))
                                    {
                                        
                                        var uri = new Uri(i);
                                        string path = uri.GetLeftPart(UriPartial.Path);

                                        if (edgeurls.Count() == 0 || edgeurls.Last() != path)
                                        {
                                            edgeurls.Add(path);
                                        }
                                        else
                                            continue;
                                    }
                                    else
                                        continue;
                                }
                            }
                        }
                    }
                }
            }
        }
        
        void GetChromeUrls()
        {
            Process[] procsChrome = Process.GetProcessesByName("chrome");
            foreach (Process chrome in procsChrome)
            {
                // the chrome process must have a window
                if (chrome.MainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }

                // find the automation element
                AutomationElement elm = AutomationElement.FromHandle(chrome.MainWindowHandle);
                AutomationElement elmUrlBar = elm.FindFirst(TreeScope.Descendants,
                  new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));

                // if it can be found, get the value from the URL bar
                if (elmUrlBar != null)
                {
                    AutomationPattern[] patterns = elmUrlBar.GetSupportedPatterns();
                    if (patterns.Length > 0)
                    {
                        ValuePattern val = (ValuePattern)elmUrlBar.GetCurrentPattern(patterns[0]);
                       string i = val.Current.Value as string;
                        //Uri uriResult;
                        //bool result = Uri.TryCreate(i, UriKind.Absolute, out uriResult)
                        //    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                        if (i != "" && i.Contains("."))
                        {
                            i = "https://" + i;
                            var uri = new Uri(i);
                            string path = uri.GetLeftPart(UriPartial.Path);

                            if (Chromeurls.Count() == 0 || Chromeurls.Last() != path)
                            {
                                Chromeurls.Add(path);
                            }
                            else
                                continue;
                        }
                        else
                            continue;
                    }
                }
            }
        }
        void GetBraveUrls()
        {
            
                Process[] procsBrave = Process.GetProcessesByName("brave");
                foreach (Process process in procsBrave)
                {
                    if (process.MainWindowHandle != IntPtr.Zero)
                    {
                        if (process == null)
                            throw new ArgumentNullException("process");
                        if (process.MainWindowHandle != IntPtr.Zero)
                        {
                            AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
                            if (element != null)
                            {
                                AutomationElement edit = element.FindFirst(TreeScope.Subtree,
                                     new AndCondition(
                                          new PropertyCondition(AutomationElement.NameProperty, "address and search bar",
                                          System.Windows.Automation.PropertyConditionFlags.IgnoreCase),
                                          new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit)));
                                if (edit != null)
                                {
                                    var i = ((ValuePattern)edit.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string;

                                if (i != "" && i.Contains("."))
                                {
                                    i = "https://" + i;
                                    var uri = new Uri(i);
                                    string path = uri.GetLeftPart(UriPartial.Path);

                                    if (Braveurls.Count() == 0 || Braveurls.Last() != path)
                                    {
                                        Braveurls.Add(path);
                                    }
                                    else
                                        continue;
                                }
                                else
                                    continue;
                            }


                        }
                        
                        
                    
                }
            }
        }
    }
    void Stop()
        {
            t.Stop();
            timer10_min.Enabled = false;
            timer_10sec.Enabled = false;
            textBox_ActiveBrowserWindowURL.Clear();
            textBox_ActiveBrowserWindowURL.AppendText("\r\nMicrosoft Edge URLs\r\n");
            textBox_ActiveBrowserWindowURL.AppendText(String.Join("\r\n", edgeurls.ToArray()));
            textBox_ActiveBrowserWindowURL.AppendText("\r\nFirefox URLs\r\n");
            textBox_ActiveBrowserWindowURL.AppendText(String.Join("\r\n", Firefoxurls.ToArray()));
            textBox_ActiveBrowserWindowURL.AppendText("\r\nChrome URLs\r\n");
            textBox_ActiveBrowserWindowURL.AppendText(String.Join("\r\n", Chromeurls.ToArray()));
            textBox_ActiveBrowserWindowURL.AppendText("\r\nBrave URLs\r\n");
            textBox_ActiveBrowserWindowURL.AppendText(String.Join("\r\n", Braveurls.ToArray()));

            frontwindow("Duration : " + i);
            DisableGetFrontWindow();
            //File.AppendAllLines("read.txt", edgeurls);
        }
        public int timetick;

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Start")
            {
                EnableGetFrontWindow();
                button2.Text = "Stop";
                timerOn = true;
                TakeFullScreenshot(directoryPath);

            }
            else
            {
                button2.Text = "Start";
                timerOn = false;
                Stop();
            }
        }

        private void timer1_tick(object sender, EventArgs e)
        {
            if (timerOn)
            {
                Start();
                if (send)
                {
                    string count = activity_percent.ToString();
                    frontwindow(count);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ReturnApps();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            appfront.Clear();
            textBox_ActiveBrowserWindowURL.Clear();
        }
        double totalActivityIn15SecSlice = 0;
        int totalDurationIn15SecSlice = 0;
        double activity_percent = 0;
        double timeticked = 0;
        private void timer_10sec_Tick(object sender, EventArgs e)
        {
            textBox_ActiveBrowserWindowURL.Clear();
            textBox_ActiveBrowserWindowURL.AppendText("\r\nMicrosoft Edge URLs\r\n");
            textBox_ActiveBrowserWindowURL.AppendText(String.Join("\r\n", edgeurls.ToArray()));
            textBox_ActiveBrowserWindowURL.AppendText("\r\nFirefox URLs\r\n");
            textBox_ActiveBrowserWindowURL.AppendText(String.Join("\r\n", Firefoxurls.ToArray()));
            textBox_ActiveBrowserWindowURL.AppendText("\r\nChrome URLs\r\n");
            textBox_ActiveBrowserWindowURL.AppendText(String.Join("\r\n", Chromeurls.ToArray()));
            textBox_ActiveBrowserWindowURL.AppendText("\r\nBrave URLs\r\n");
            textBox_ActiveBrowserWindowURL.AppendText(String.Join("\r\n", Braveurls.ToArray()));

            DateTime slot_time = DateTime.UtcNow;
            int hour = slot_time.Hour;
            int minute = slot_time.Minute;
            int second = slot_time.Second;
            int millisecond = slot_time.Millisecond;

            minute %= 10;   //minute = minute % 15;     // minute MOD 15
            int totalSeconds = (minute * 60) + second + 1;        // 1 is a fudge
            int ts = s % 10;

            // Update 15Min slice totals
            totalActivityIn15SecSlice += activity_percent;
            totalDurationIn15SecSlice += 9;
            //activity = 0;

            activity_percent = 0;
        }

        int activity = 0;
        void CalculateActivity(string message)
        {

            string last_message = "";
            if (message != last_message && timerOn)
            {
                activity = 1;
            }
            activity = 0;

        }
        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            // EDT: No need to filter for VkSnapshot anymore. This now gets handled
            // through the constructor of GlobalKeyboardHook(...).
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyDown && timerOn)
            {
                // Now you can access both, the key and virtual code
                Keys loggedKey = e.KeyboardData.Key;
                frontwindow(" - Key pressed");
            }
        }
        public void GetMousePosition(int X, int Y)
        {
            string Message = string.Format("x={0:0000}; y={1:0000}", X, Y);
            CalculateActivity(Message);
            MouseMessage(Message);
        }
        bool IsScreenSaverOn = false;
        bool IsScreenLocked = false;

        private void button4_Click(object sender, EventArgs e)
        {
            TakeWindowScreenshot();
        }

        int durationscreensaver = 0;
        void CheckScreen()
        {
            IsScreenLocked = ScreenSaver.IsWorkstationLocked();
            if (IsScreenLocked)
            {
                Stop();
                frontwindow("ScreenLocked=On");
                Console.Beep();
            }


            IsScreenSaverOn = ScreenSaver.IsScreensaverRunning();
            if (IsScreenSaverOn)
            {
                Stop();
                durationscreensaver++;
                frontwindow("ScreenSaver=On and Duration = " + durationscreensaver);
                Console.Beep();
            }
            if (!IsScreenSaverOn)
            {
                durationscreensaver = 0;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TakeFullScreenshot(directoryPath);
        }

        void TakeWindowScreenshot()
        {
            string n = string.Format("ScreenShot-{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            var image = ScreenCapture.CaptureDesktop();
            image.Save($@"T:\screenshots\{n}.png", ImageFormat.Png);
        }
        int i = 0;
        public string directoryPath;
        private void timer10_min_Tick(object sender, EventArgs e)
        {
            CreateDirectory();
            i++;
        }

        private void timer2min_Tick(object sender, EventArgs e)
        {
            //Random ss = new Random();
            //int num = ss.Next(100);
            //if
            //TakeFullScreenshot(directoryPath);
            RandomTimeSet();
        }

        private void timer_random_Tick(object sender, EventArgs e)
        {
            TakeFullScreenshot(directoryPath);

        }
        public int count_ss ;
        void TakeFullScreenshot(string dir)
        {
            count_ss = 0;

            string folderName = string.Format($"ScreenShot-{DateTime.UtcNow}");
            var image = ScreenCapture.CaptureDesktop();
            string timestamp = (new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()).ToString();
            string imagePath = Path.Combine(directoryPath, $"image-{timestamp}.png");
            image.Save(imagePath, ImageFormat.Png);
            //image.Save($@"T:\screenshots\{dir}\{folderName}.png", ImageFormat.Png);
            frontwindow("--Active window Screenshot Captured--");
            count_ss++;
        }

        void RandomTimeSet()
        {
            timer_random.Enabled = true;
            Random ss = new Random();
            int num = ss.Next(10000, 119000);
            timer_random.Interval = num;
        }
        //void Check()
        //{object sender, ElapsedEventArgs e
        //    Timer t = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds); // Set the time (5 mins in this case)
        //    t.AutoReset = true;
        //    t.Elapsed += new System.Timers.ElapsedEventHandler(TakeFullScreenshot);
        //    t.Start();
        //}

        void CreateDirectory()
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = dt1.AddSeconds(10);


            //string dir1 = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", dt1);
            //string dir2 = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", dt2);

            string timestamp = (new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()).ToString();
            directoryPath = $@"C:\screenshots\{timestamp}";

            // If directory does not exist, create it
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

        }
        //void CheckchromeUrl()
        //    {
        //        Process[] procsChrome = Process.GetProcessesByName("chrome");

        //        foreach (Process chrome in procsChrome)
        //        {
        //            // the chrome process must have a window
        //            if (chrome.MainWindowHandle == IntPtr.Zero)
        //            {
        //                continue;
        //            }

        //            // find the automation element
        //            AutomationElement elm =
        //            AutomationElement.FromHandle(chrome.MainWindowHandle);
        //            AutomationElement elmUrlBar = elm.FindFirst(TreeScope.Descendants,
        //            new PropertyCondition(AutomationElement.NameProperty, "Address and search bar"));

        //            // if it can be found, get the value from the URL bar
        //            while (true)
        //            {
        //                if (elmUrlBar != null)
        //                {
        //                    AutomationPattern[] patterns = elmUrlBar.GetSupportedPatterns();
        //                    if (patterns.Length > 0)
        //                    {
        //                        ValuePattern val =
        //                        (ValuePattern)elmUrlBar.GetCurrentPattern(patterns[0]);
        //                        Console.WriteLine("Chrome URL found: " + val.Current.Value);

        //                        Console.ReadLine();
        //                    }
        //                }
        //            }
        //        }
        //    }
    }

}