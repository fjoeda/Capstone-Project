using PredictionEngine;
using RealsenseHandler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using cv = OpenCvSharp;


namespace SignIt
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private cv.VideoCapture capture;
        private CancellationTokenSource cameraCancellationTokenSource;
        Bitmap bitmapImage;

        private RealsenseManager rm;
        private List<float> dataVals = new List<float>();

        private ISignPreditionEngine preditionEngine;

        public MainPage()
        {
            InitializeComponent();
            StartCameraCapture();
            rm = new RealsenseManager(true);
            rm.DataStreamUpdate += Rm_DataStreamUpdate;
            rm.HandDataChanged += Rm_HandDataChanged;
            rm.Init();

            preditionEngine = new MLNetSignPrediction(MLNetClassifier.LightGBM, true);
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
                PredictionEngine.InputData input = new PredictionEngine.InputData()
                {
                    PixelValues = dataVals.ToArray()
                };

                var pred = preditionEngine.Predict(input);

                //Invoke(new AppendTextOnTextBox(AppendText), new object[] { pred });
                Dispatcher.Invoke(new AppendTextOnTextBox(SetText), new object[] { pred });
                dataVals.Clear();
            }

        }
        private void SetText(string text)
        {
            if (text != null)
                tb_Result.Text = text;
            
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
                capture = new cv.VideoCapture(0, cv.VideoCaptureAPIs.ANY);

            capture.Open(0);

            if (capture.IsOpened())
            {
                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        
                        using MemoryStream memoryStream = capture.RetrieveMat().ToMemoryStream();

                        await Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            try
                            {
                                var imageSource = new BitmapImage();

                                imageSource.BeginInit();
                                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                                imageSource.StreamSource = memoryStream;
                                imageSource.EndInit();

                                img_WebCam.Source = imageSource;
                            }catch(Exception e)
                            {

                            }
                            
                        });

                        bitmapImage = new Bitmap(memoryStream);


                        //await ParseWebCamFrame(bitmapImage, token);

                    }

                    capture.Release();
                }
                catch (Exception e)
                {

                }
                
            }
        }

        #endregion

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            rm.Dispose();
            StopCameraCapture();
        }
    }
}
