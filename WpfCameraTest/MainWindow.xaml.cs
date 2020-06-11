using cv = OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.IO;
using RealsenseHandler;

namespace WpfCameraTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private cv.VideoCapture capture;
        private CancellationTokenSource cameraCancellationTokenSource;
        private bool performVerification = false;

        private RealsenseManager rm;
        private SignPredicition predicition = new SignPredicition();
        private List<float> dataVals = new List<float>();


        Bitmap bitmapImage;
        public MainWindow()
        {
            InitializeComponent();
            rm = new RealsenseManager(true);
            rm.DataStreamUpdate += Rm_DataStreamUpdate;
            rm.HandDataChanged += Rm_HandDataChanged;
            rm.Init();
        }

        private void Rm_HandDataChanged(RealsenseHandler.RsHand.Hand rightHand, RealsenseHandler.RsHand.Hand leftHand)
        {
           
        }

        private delegate void AppendTextOnTextBox(string text);
        private void Rm_DataStreamUpdate(string dataStream, string preprocessedDataStream)
        {
            var values = preprocessedDataStream.Split(',');
            for (int i = 0; i < values.Length - 1; i++)
            {
                dataVals.Add(float.Parse(values[i]));
            }
            
            if (dataVals.Count == 12300)
            {
                InputData input = new InputData()
                {
                    PixelValues = dataVals.ToArray()
                };
                var pred = predicition.Predict(input);

                //Invoke(new AppendTextOnTextBox(AppendText), new object[] { pred });
                Dispatcher.Invoke(new AppendTextOnTextBox(SetText), new object[] { pred });
                dataVals.Clear();
            }

        }
        private void SetText(string text)
        {
            if (text != null)
                tb_Result.Text = text;
            else
                tb_Result.Text = "";
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            StartCameraCapture();
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            StopCameraCapture();
        }

        #region Camera Capture
        private void StartCameraCapture()
        {
            cameraCancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => CaptureCamera(cameraCancellationTokenSource.Token), cameraCancellationTokenSource.Token);
        }

        private void StopCameraCapture() => cameraCancellationTokenSource?.Cancel();

        private async Task CaptureCamera(CancellationToken token)
        {
            if (capture == null)
                capture = new cv.VideoCapture(0,cv.VideoCaptureAPIs.ANY);

            capture.Open(0);

            if (capture.IsOpened())
            {
                while (!token.IsCancellationRequested)
                {
                    using MemoryStream memoryStream = capture.RetrieveMat().ToMemoryStream();

                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        var imageSource = new BitmapImage();

                        imageSource.BeginInit();
                        imageSource.CacheOption = BitmapCacheOption.OnLoad;
                        imageSource.StreamSource = memoryStream;
                        imageSource.EndInit();

                        img_WebCam.Source = imageSource;
                    });

                    bitmapImage = new Bitmap(memoryStream);
                   

                    //await ParseWebCamFrame(bitmapImage, token);
                    
                }

                capture.Release();
            }
        }
        #endregion
    }
}
