using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SampleDX;
using RealsenseHandler;
using System.Drawing.Imaging;
using System.IO;
using RS = Intel.RealSense;
using PredictionEngine;

namespace ImageViewerWinforms
{
    public partial class Form1 : Form
    {
        private int timerCountDown = 3;
        private int timerRecordTime = 0;
        private bool isRecording = false;
        private bool isPrediction = false;
        private bool isEvaluating = false;
        private bool isRecordingFinished = false;
        private readonly D2D1Render renderRGB;
        private readonly D2D1Render renderDepth;
        private List<string> streamLines = new List<string>();
        private List<string> preprocessedStreamLines = new List<string>();
        private SignPredicition predicition = new SignPredicition();
        private List<float> dataVals = new List<float>();
        private ISignPreditionEngine preditionEngine;

        private Dictionary<string, int> evaluationDict = new Dictionary<string, int>();


        private readonly RealsenseManager rm;
        public Form1()
        {
            InitializeComponent();
            rm = new RealsenseManager();
            rm.RGBImageRetreived += Rm_RGBImageRetreived;
            rm.DepthImageRetreived += Rm_DepthImageRetreived;
            rm.HandDataChanged += Rm_HandDataChanged;
            rm.DataStreamUpdate += Rm_DataStreamUpdate;
            rm.ImageCaptured += Rm_ImageCaptured;

            pb_ImgCam.Paint += Pb_ImgCam_Paint;
            pb_DepthImage.Paint += Pb_DepthImage_Paint;
            
            renderRGB = new D2D1Render();
            renderDepth = new D2D1Render();
            renderRGB.SetHWND(pb_ImgCam);
            renderDepth.SetHWND(pb_DepthImage);
            rm.Init();

            preditionEngine = new MLNetHierarchicalSignPrediciton(MLNetClassifier.LightGBM);
            InitiateDitionary();
        }

        private void Rm_ImageCaptured(Bitmap bitmap)
        {
            
        }

        private delegate void ShowDataStream(string filename,string data);
        private delegate void AppendTextOnTextBox(string text);

        private void Rm_DataStreamUpdate(string dataStream, string preprocessedDataStream)
        {
            
            var stream = dataStream.Split(",");
            var preprocessedStream = preprocessedDataStream.Split(",");
            
            if (isPrediction)
            {
                string pred = "";
                
                var values = preprocessedDataStream.Split(',');
                for(int i = 0;i<values.Length-1;i++)
                {
                    dataVals.Add(float.Parse(values[i]));
                }
                if (dataVals.Count == 12300)
                {
                    PredictionEngine.InputData input = new PredictionEngine.InputData()
                    {
                        PixelValues = dataVals.ToArray()
                    };
                    pred = preditionEngine.PredictSingle(input);
                    
                    Invoke(new AppendTextOnTextBox(SetPredictionLabel), new object[] { pred });
                   
                    dataVals.Clear();
                }
                else if(dataVals.Count>12420)
                {
                    dataVals.Clear();
                }

                if (isEvaluating && (pred!=null))
                {
                    CountPredition(pred);
                    Invoke(new AppendTextOnTextBox(SetText), new object[] { EvaluationText() });
                }
                else
                {
                    Invoke(new AppendTextOnTextBox(SetText), new object[] { pred });
                }
            }
            else
            {
                Invoke(new ShowDataStream(ShowDataStreamOnTextBox), new object[] { "raw_data", dataStream });
                Invoke(new ShowDataStream(ShowPreprocessedData), new object[] { "preprocessed_data", preprocessedDataStream });
            }
        }

        private void SetPredictionLabel(string text)
        {
            if (text != null)
                lbl_prediction.Text = "Prediction : " + text;
        }

        private void AppendText(string text)
        {
            if(text!=null)
                tb_HandData.Text += text+" ";
            
        }

        private void SetText(string text)
        {
            if (text != null)
                tb_HandData.Text = text;
            if (isEvaluating && isRecordingFinished)
            {
                SaveEvaluationResult($"Evaluation for {tb_Label.Text}");
                isRecordingFinished = false;
                InitiateDitionary();
            }
        }

        private void SaveEvaluationResult(string filename)
        {
            string folderName = tb_Label.Text;
            string collFolder = tb_Collection.Text;
            int fileCount = 0;
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Evaluation\\" + collFolder + "\\" + folderName))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Evaluation\\" + collFolder + "\\" + folderName);
            }
            else
            {
                fileCount = Directory.GetFiles(Environment.CurrentDirectory + "\\Evaluation\\" + collFolder + "\\" + folderName).Length;
            }

            using (StreamWriter file = new StreamWriter(Environment.CurrentDirectory + "\\Evaluation\\" + collFolder + "\\" + folderName + $"\\{filename}_{fileCount + 1}.txt"))
            {
                file.WriteLine(tb_HandData.Text);
            }
        }

        private void ShowDataStreamOnTextBox(string fileName,string data)
        {
            if(!isPrediction)
                tb_HandData.Text = data;

            if (isRecording && !isPrediction)
            {
                streamLines.Add(data);
            }
            else 
            {
                if (!(streamLines.Count == 0))
                {
                    //save data
                    SaveDataStream(fileName,streamLines);
                    streamLines.Clear();
                }
            }
        }

        private void ShowPreprocessedData(string fileName, string data)
        {
            //tb_HandData.Text = data;
            if (isRecording)
            {
                preprocessedStreamLines.Add(data);
            }
            else
            {
                if (!(preprocessedStreamLines.Count == 0))
                {
                    //save data
                    SaveDataStream(fileName, preprocessedStreamLines);
                    preprocessedStreamLines.Clear();
                }
            }
        }

        private void SaveDataStream(string filename,List<string> dataList)
        {
            string folderName = tb_Label.Text;
            string collFolder = tb_Collection.Text;
            int fileCount = 0;
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Data\\"+ collFolder+ "\\" + folderName))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Data\\" + collFolder + "\\" + folderName);
            }
            else
            {
                fileCount = Directory.GetFiles(Environment.CurrentDirectory + "\\Data\\" + collFolder + "\\" + folderName).Length;
            }

            using(StreamWriter file = new StreamWriter(Environment.CurrentDirectory + "\\Data\\" + collFolder + "\\" + folderName+$"\\{filename}_{fileCount+1}.txt"))
            {
                for(int i = 0; i < dataList.Count-30; i++)
                {
                    var line = "";
                    for(int j = 0; j < 30; j++)
                    {
                        line += dataList[i + j];
                    }
                    file.WriteLine(line+folderName);
                    string[] vec = (line + folderName).Split(",");

                }
            }
        }

        private void Rm_HandDataChanged(RealsenseHandler.RsHand.Hand rightHand, RealsenseHandler.RsHand.Hand leftHand)
        {
           
        }

        private void Pb_DepthImage_Paint(object sender, PaintEventArgs e)
        {
            renderDepth.UpdatePanel();

        }

        private void Rm_DepthImageRetreived(RS.Image handImage)
        {
            renderDepth.UpdatePanel(handImage);
        }

        private void Rm_RGBImageRetreived(RS.Image handImage)
        {
            renderRGB.UpdatePanel(handImage);
        }

        private void Pb_ImgCam_Paint(object sender, PaintEventArgs e)
        {
            renderRGB.UpdatePanel();
            
        }

        private void btn_Record_Click(object sender, EventArgs e)
        {
            if (!tb_Label.Text.Equals("") && !tb_Collection.Text.Equals(""))
            {
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timerCountDown--;
            btn_Record.Text = "recording in " + timerCountDown.ToString() + "...";
            if (timerCountDown == 0)
            {
                isRecording = true;
                timer1.Stop();
                tm_Record.Start();
                btn_Record.Text = "Recording...";
                btn_Record.Enabled = false;
                timerCountDown = 4;
                isRecordingFinished = false;
                InitiateDitionary();
            }
        }

        private void tm_Record_Tick(object sender, EventArgs e)
        {
            timerRecordTime++;
            if (timerRecordTime == 6)
            {
                isRecording = false;
                btn_Record.Text = "Start Recording";
                btn_Record.Enabled = true;
                tm_Record.Stop();
                timerRecordTime = 0;
                isRecordingFinished = true;
                MessageBox.Show($"Recording for {tb_Label.Text} is finished");
            }
        }

        private void btn_TakePicture_Click(object sender, EventArgs e)
        {
                        
        }

        private void cb_Test_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_Test.Checked)
            {
                tb_HandData.Clear();
                isPrediction = true;
                //btn_Record.Enabled = false;
                //tb_Label.Enabled = false;
            }
            else
            {
                isPrediction = false;
                //btn_Record.Enabled = true;
                //tb_Label.Enabled = true;
            }
        }

        private void btn_CombineData_Click(object sender, EventArgs e)
        {
            List<string> allLines = new List<string>();
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string[] filePaths = Directory.GetFiles(dialog.SelectedPath, "preprocessed*.txt",SearchOption.AllDirectories);
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Text File|*.txt|Csv file|*.csv";
                saveDialog.Title = "Save the combined files";

                DialogResult saveResult = saveDialog.ShowDialog();
                if (saveResult == DialogResult.OK)
                {
                    foreach (string path in filePaths)
                    {
                        string[] lines = System.IO.File.ReadAllLines(path);
                        foreach(string line in lines)
                        {
                            allLines.Add(line.ToLower());

                            GC.Collect();
                        }
                    }

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveDialog.FileName))
                    {
                        foreach (string line in allLines)
                        {
                            file.WriteLine(line);
                        }
                    }
                }
                saveDialog.Dispose();
            }

            MessageBox.Show("File Combined");

            allLines.Clear();
            dialog.Dispose();
            GC.Collect();
        }

        private void InitiateDitionary()
        {
            evaluationDict.Clear();
            foreach(var item in MLNetSignPrediction.labels)
            {
                evaluationDict.Add(item, 0);
            }
        }

        private void tb_Label_TextChanged(object sender, EventArgs e)
        {
            if (isPrediction)
            {
                if (!tb_Label.Text.Equals(""))
                {
                    isEvaluating = true;
                }
                else
                {
                    isEvaluating = false;
                }
            }
        }

        private void CountPredition(string pred)
        {
            if (!pred.Equals(""))
            {
                var currentVal = evaluationDict[pred];
                evaluationDict[pred] +=1;
            }
            
        }

        private string EvaluationText()
        {
            string evalText = $"Evaluation for {tb_Label.Text} :";
            foreach(var item in evaluationDict)
            {
                evalText += Environment.NewLine + $"{item.Key} : {item.Value}";
            }
            return evalText;
        }
    }
}
