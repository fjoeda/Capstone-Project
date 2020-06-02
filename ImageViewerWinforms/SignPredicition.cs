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
        [LoadColumn(0, 12499)]
        [VectorType(12500)]
        public float[] PixelValues;

        
        [ColumnName("Sign")]
        [LoadColumn(12500)]
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

        public SignPredicition()
        {
            mLContext = new MLContext(seed: 0);
            trainedModel = mLContext.Model.Load(Environment.CurrentDirectory + "\\model.zip", out var nodelSchema);
            prediction = mLContext.Model.CreatePredictionEngine<InputData, OutPutData>(trainedModel);
        }

        public string Predict(InputData input)
        {
            var result = prediction.Predict(input);
            var maxScore = result.Score.Max();
            if (maxScore > 0.85)
                return labels[Array.IndexOf(result.Score, maxScore)];
            else
                return null;
        }

        private static string[] labels = new string[] { "Halo", "Kamu", "OK", "Siapa", "Terima Kasih" };
       
    }
}
