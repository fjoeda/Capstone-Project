using RS = Intel.RealSense;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using Intel.RealSense;
using System.Timers;

namespace ImageViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private RS.Pipeline pipeline;
        private RS.Colorizer colorizer;
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private System.Timers.Timer timer;
        private System.Timers.Timer timerCountdown;


        private int imageCount = 0;
        private int countDownCount = 3;
        private int recordCount = 0;
        private int currentFileTotal = 0;
        private bool isRecording = false;

        static Action<RS.VideoFrame> UpdateImage(Image Image)
        {
            var wbmp = Image.Source as WriteableBitmap;
            return new Action<RS.VideoFrame>(frame =>
            {
                var rect = new Int32Rect(0, 0, frame.Width, frame.Height);
                wbmp.WritePixels(rect, frame.Data, frame.Stride * frame.Height, frame.Stride);
            });
        }
       
        public MainWindow()
        {
            InitializeComponent();
            pipeline = new RS.Pipeline();
            colorizer = new RS.Colorizer();
            

            Action<RS.VideoFrame> mainAction;
            Action<RS.VideoFrame> depthAction;

            var config = new RS.Config();
            config.EnableStream(RS.Stream.Color,640,480,RS.Format.Rgb8);
            config.EnableStream(RS.Stream.Depth, 640, 480);

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000;
            timer.Enabled = false;
            timerCountdown = new System.Timers.Timer();
            timerCountdown.Elapsed += TimerCountdown_Elapsed;
            timerCountdown.Interval = 1000;
            timerCountdown.Enabled = false;


            try
            {
                var pp = pipeline.Start(config);
                

                SetupWindow(pp,out mainAction ,out depthAction);

                Task.Factory.StartNew(() =>
                {
                    while (!tokenSource.Token.IsCancellationRequested)
                    {
                        using (var frames = pipeline.WaitForFrames())
                        {
                            var mainFrame = frames.ColorFrame.DisposeWith(frames);
                            var depthFrame = frames.DepthFrame.DisposeWith(frames);
                            var colorizedDepth = colorizer.Process<VideoFrame>(depthFrame).DisposeWith(frames);
                            Dispatcher.Invoke(DispatcherPriority.Render, mainAction, mainFrame);
                            Dispatcher.Invoke(DispatcherPriority.Render, depthAction, colorizedDepth);
                        }

                        if (isRecording)
                        {
                            imageCount++;
                            Dispatcher.Invoke(new SaveImagesDelegate(SaveImage), new object[] { "image_",imageCount });
                        }
                        else
                        {
                            imageCount = 0;
                        }
                    }
                }, tokenSource.Token);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public delegate void SetButtonProp(Button btn, string text, bool state);

        public void SetButtonProperties(Button btn, string text, bool state)
        {
            btn.Content = text;
            btn.IsEnabled = state;
        }

        private void TimerCountdown_Elapsed(object sender, ElapsedEventArgs e)
        {
            countDownCount--;
            var text = $"Recording in {countDownCount} ...";
            Dispatcher.Invoke(new SetButtonProp(SetButtonProperties), new object[] { btn_Record, text, false });
            if (countDownCount == 0)
            {
                countDownCount = 4;
                timer.Enabled = true;
                timerCountdown.Enabled = false;
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            recordCount++;
            isRecording = true;
            var text = "Recording...";
            Dispatcher.Invoke(new SetButtonProp(SetButtonProperties), new object[] { btn_Record, text, false });
            if (recordCount > 5)
            {
                recordCount = 0;
                timer.Enabled = false;
                Dispatcher.Invoke(new SetButtonProp(SetButtonProperties), new object[] { btn_Record, "Record", true});
                isRecording = false;
            }
        }

        private void SetupWindow(RS.PipelineProfile pipelineProfile,out Action<RS.VideoFrame> color ,out Action<RS.VideoFrame> depth)
        {
            
            using (var p = pipelineProfile.GetStream(RS.Stream.Depth).As<RS.VideoStreamProfile>())
                img_Depth.Source = new WriteableBitmap(p.Width, p.Height, 96d, 96d, PixelFormats.Rgb24, null);
            depth = UpdateImage(img_Depth);
            
            
            using (var p = pipelineProfile.GetStream(RS.Stream.Color).As<RS.VideoStreamProfile>())
                img_Main.Source = new WriteableBitmap(p.Width, p.Height, 96d, 96d, PixelFormats.Rgb24, null);
            color = UpdateImage(img_Main);
            
        }

        private void btn_TakePicture_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private delegate void SaveImagesDelegate(string fileName, int fileNum);
        private void SaveImage(string fileName, int fileNum)
        {
            string folderName = tb_Label.Text;
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Data\\" + folderName))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Data\\" + folderName);
                currentFileTotal = 0;
            }
            else
            {
                if (fileNum == 1)
                {
                    currentFileTotal = Directory.GetFiles(Environment.CurrentDirectory + "\\Data\\" + folderName).Length;
                }
            }

            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)img_Depth.Source));
            using (FileStream stream = new FileStream(Environment.CurrentDirectory + "\\Data\\"+folderName+"\\"+fileName+$"{fileNum+currentFileTotal}.jpg", FileMode.Append))
            {
                encoder.Save(stream);
            }
        }

        private void btn_Record_Click(object sender, RoutedEventArgs e)
        {
            if (!tb_Label.Text.Equals(""))
            {
                timerCountdown.Start();
            }
        }
    }
}
