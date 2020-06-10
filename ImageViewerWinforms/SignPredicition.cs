using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace ImageViewerWinforms
{
    public class InputData
    {
        [ColumnName("PixelValues")]
        [LoadColumn(0, 12299)]
        [VectorType(12300)]
        public float[] PixelValues;

        
        [ColumnName("Sign")]
        [LoadColumn(12300)]
        public string Sign;
    }

    class OutPutData
    {
        

        [ColumnName("Score")]
        public float[] Score;
    }
    public class SignPredicition
    {
        private MLContext mLContext;
        ITransformer trainedModel;
        PredictionEngine<InputData, OutPutData> prediction;
        DataViewSchema schema;
        public SignPredicition()
        {
            mLContext = new MLContext(seed: 0);
            trainedModel = mLContext.Model.Load(Environment.CurrentDirectory + "\\model.zip", out schema);
            prediction = mLContext.Model.CreatePredictionEngine<InputData, OutPutData>(trainedModel);
        }

        public string Predict(InputData input)
        {
            var result = prediction.Predict(input);
            var maxScore = result.Score.Max();
            Console.WriteLine(result.ToString());
            Console.WriteLine(maxScore);
            if (maxScore > 0.7)
                return "Predicted : " + labels[Array.IndexOf(result.Score, maxScore)] +
                    Environment.NewLine + " Confidence : " + maxScore.ToString();
            else
                return null;
            
        }

        private static string[] labels = new string[] { 
            "1", "10","2","3","4","5","6","7","8","9","A","Apa","B","Belum",
            "Berapa","C","D","Dia","E","F","G","H","Halo","I","Isyarat","J",
            "K","Kamu","L","Lebih","M","Malam","N","O","Ok","P","Pagi",
            "Pukul (jam)","Q","R","S","Sekarang","Siang","Siapa","Sore",
            "Sudah","T","Teman","Teman-teman","Terimakasih","U","V","W",
            "X","Y"
        };
       
    }
}
