using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PredictionEngine
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

    public class OutPutData
    {
        public string Sign;

        [ColumnName("Score")]
        public float[] Score;
    }

    public interface ISignPreditionEngine
    {
        public void InitiateClassifier(MLNetClassifier clf);
        public void InitiateClassifierWithPCA(MLNetClassifier clf);
        public string Predict(InputData input);
        public string PredictSingle(InputData input);

    }
    public class MLNetSignPrediction : ISignPreditionEngine
    {
        
        private MLContext mLContext;
        ITransformer trainedModel;
        PredictionEngine<InputData, OutPutData> prediction;
        DataViewSchema schema;

        public MLNetSignPrediction(MLNetClassifier clf, bool UsePCA = false)
        {
            if (!UsePCA)
                InitiateClassifier(clf);
            else
                InitiateClassifierWithPCA(clf);
        }

        public void InitiateClassifier(MLNetClassifier clf)
        {
            if (clf == MLNetClassifier.LightGBM)
            {
                mLContext = new MLContext(seed: 0);
                trainedModel = mLContext.Model.Load(Environment.CurrentDirectory + "\\Models\\model_LightGBM Single Model.zip", out schema);
                prediction = mLContext.Model.CreatePredictionEngine<InputData, OutPutData>(trainedModel);
            }
            else if (clf == MLNetClassifier.L_BFGS)
            {
                mLContext = new MLContext(seed: 0);
                trainedModel = mLContext.Model.Load(Environment.CurrentDirectory + "\\Models\\model_LBFGS Single Model.zip", out schema);
                prediction = mLContext.Model.CreatePredictionEngine<InputData, OutPutData>(trainedModel);
            }
        }

        public void InitiateClassifierWithPCA(MLNetClassifier clf)
        {
            if (clf == MLNetClassifier.LightGBM)
            {
                mLContext = new MLContext(seed: 0);
                trainedModel = mLContext.Model.Load(Environment.CurrentDirectory + "\\Models\\model_LightGBM Single Model pca 100.zip", out schema);
                prediction = mLContext.Model.CreatePredictionEngine<InputData, OutPutData>(trainedModel);
            }
            else if (clf == MLNetClassifier.L_BFGS)
            {
                mLContext = new MLContext(seed: 0);
                trainedModel = mLContext.Model.Load(Environment.CurrentDirectory + "\\Models\\model_L-BFGS Single Model PCA 100.zip", out schema);
                prediction = mLContext.Model.CreatePredictionEngine<InputData, OutPutData>(trainedModel);
            }
        }

        public string Predict(InputData input)
        {
            var result = prediction.Predict(input);
            var maxScore = result.Score.Max();
            Console.WriteLine(result.ToString());
            Console.WriteLine(maxScore);
            if (maxScore > 0.3)
                return "Predicted : " + labels[Array.IndexOf(result.Score, maxScore)] +
                    Environment.NewLine + "Confidence : " + maxScore.ToString();
            else
                return null;
        }

        public string PredictSingle(InputData input)
        {
            var result = prediction.Predict(input);
            var maxScore = result.Score.Max();
            //Console.WriteLine(result.ToString());
            //Console.WriteLine(maxScore);
            if (maxScore > 0.7)
                return labels[Array.IndexOf(result.Score, maxScore)];
            else
                return null;
        }

        public static string[] labels = new string[] {
            "1", "10","2","3","4","5","6","7","8","9","A","Apa","B","Belum",
            "Berapa","C","D","Dia","E","F","G","H","Halo","I","Isyarat","J",
            "K","Kamu","L","M","Malam","N","O","Ok","P","Pagi",
            "Q","R","S","Sekarang","Siang","Siapa","Sore",
            "Sudah","T" ,"Teman-teman","Terimakasih","U","V","W",
            "X","Y","Z"
        };

    }

    public class MLNetHierarchicalSignPrediciton : ISignPreditionEngine
    {
        
        PredictionEngine<InputData, OutPutData> signClassification, numberPrediciton,alphabetPredicion,commonSignPrediciton;
        DataViewSchema schema;

        public MLNetHierarchicalSignPrediciton(MLNetClassifier clf, bool UsePCA = false)
        {
            if (!UsePCA)
                InitiateClassifier(clf);
            else
                InitiateClassifierWithPCA(clf);
        }

        private PredictionEngine<InputData,OutPutData> LoadModel(string filename)
        {
            var mLContext = new MLContext(seed: 0);
            var trainedModel = mLContext.Model.Load(Environment.CurrentDirectory + "\\Models\\"+filename, out schema);
            return mLContext.Model.CreatePredictionEngine<InputData, OutPutData>(trainedModel);
        }

        public void InitiateClassifier(MLNetClassifier clf)
        {
            if(clf == MLNetClassifier.L_BFGS)
            {
                signClassification = LoadModel("model_LBFGS SignClassification Model.zip");
                numberPrediciton = LoadModel("model_LBFGS Number Model.zip");
                alphabetPredicion = LoadModel("model_LBFGS Alphabet Model.zip");
                commonSignPrediciton = LoadModel("model_LBFGS CommonSign Model.zip");
            }
            else if(clf == MLNetClassifier.LightGBM)
            {
                signClassification = LoadModel("model_LightGBM SignCategorical Model.zip");
                numberPrediciton = LoadModel("model_LightGBM Number Model.zip");
                alphabetPredicion = LoadModel("model_LightGBM Alphabet Model.zip");
                commonSignPrediciton = LoadModel("model_LightGBM CommonSign Model.zip");
            }
        }

        public void InitiateClassifierWithPCA(MLNetClassifier clf)
        {
            //throw new NotImplementedException();
        }

        public string Predict(InputData input)
        {
            var signClassificationResult = signClassification.Predict(input);
            var signClassificationResultStr = signClassLabels[Array.IndexOf(signClassificationResult.Score, signClassificationResult.Score.Max())];
            OutPutData predResult = new OutPutData();
            string signResult = "";
            if (signClassificationResultStr.Equals("num"))
            {
                predResult = numberPrediciton.Predict(input);
                signResult = numberLabels[Array.IndexOf(predResult.Score, predResult.Score.Max())];
            }
            else if (signClassificationResultStr.Equals("char"))
            {
                predResult = alphabetPredicion.Predict(input);
                signResult = alphabetLabels[Array.IndexOf(predResult.Score, predResult.Score.Max())];
            }
            else if (signClassificationResultStr.Equals("greet"))
            {
                predResult = commonSignPrediciton.Predict(input);
                signResult = commonSignLabels[Array.IndexOf(predResult.Score, predResult.Score.Max())];
            }

            var confidence = predResult.Score.Max() * signClassificationResult.Score.Max();

            if (confidence > 0.6)
                return "Predicted : " + signResult +
                    Environment.NewLine + "Confidence : " + confidence.ToString();
            else
                return null;
        }

        public string PredictSingle(InputData input)
        {
            var signClassificationResult = signClassification.Predict(input);
            var signClassificationResultStr = signClassLabels[Array.IndexOf(signClassificationResult.Score, signClassificationResult.Score.Max())];
            string signResult = "";
            OutPutData predResult = new OutPutData();
            if (signClassificationResultStr.Equals("num"))
            {
                predResult = numberPrediciton.Predict(input);
                signResult = numberLabels[Array.IndexOf(predResult.Score, predResult.Score.Max())];
            }
            else if (signClassificationResultStr.Equals("char"))
            {
                predResult = alphabetPredicion.Predict(input);
                signResult = alphabetLabels[Array.IndexOf(predResult.Score, predResult.Score.Max())];
            }
            else if (signClassificationResultStr.Equals("greet"))
            {
                predResult = commonSignPrediciton.Predict(input);
                signResult = commonSignLabels[Array.IndexOf(predResult.Score, predResult.Score.Max())];
            }

            var confidence = predResult.Score.Max() * signClassificationResult.Score.Max();

            if (confidence > 0.6)
                return signResult;
            else
                return null;
        }

        private static string[] signClassLabels = new string[] {"num","char","greet"
        };

        private static string[] numberLabels = new string[] {
            "1", "10","2","3","4","5","6","7","8","9"
        };

        private static string[] alphabetLabels = new string[] {
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P",
            "Q","R","S","T","U","V","W",
            "X","Y","Z"
        };

        private static string[] commonSignLabels = new string[] {
            "Apa","Belum","Berapa","Dia","Halo","Isyarat",
            "Kamu","Malam","Ok","Pagi","Sekarang","Siang","Siapa","Sore",
            "Sudah","Teman-teman","Terimakasih"
        };
    }

    public class EnsembleLearningPredictionEngine : ISignPreditionEngine
    {
        private MLContext mLContext;
        List<PredictionEngine<InputData, OutPutData>> prediction = new List<PredictionEngine<InputData, OutPutData>>();
        DataViewSchema schema;

        public EnsembleLearningPredictionEngine(MLNetClassifier clf, bool UsePCA = false)
        {
            if (!UsePCA)
                InitiateClassifier(clf);
            else
                InitiateClassifierWithPCA(clf);
        }

        public void InitiateClassifier(MLNetClassifier clf)
        {
            mLContext = new MLContext();
            if (clf == MLNetClassifier.LightGBM)
            {
                var filenames = Directory.GetFiles(Environment.CurrentDirectory + $"\\Models\\Bagging\\Light GBM");
                foreach(var file in filenames)
                {
                    var trainedModel = mLContext.Model.Load(file, out schema);
                    prediction.Add(mLContext.Model.CreatePredictionEngine<InputData,OutPutData>(trainedModel));
                }
            }
            else if (clf == MLNetClassifier.L_BFGS)
            {
                var filenames = Directory.GetFiles(Environment.CurrentDirectory + $"\\Models\\Bagging\\LBFGS");
                foreach (var file in filenames)
                {
                    var trainedModel = mLContext.Model.Load(file, out schema);
                    prediction.Add(mLContext.Model.CreatePredictionEngine<InputData, OutPutData>(trainedModel));
                }
            }
        }

        public void InitiateClassifierWithPCA(MLNetClassifier clf)
        {
           
        }

        public string Predict(InputData input)
        {
            Dictionary<string, int> predictionResult = new Dictionary<string, int>();
            foreach(var model in prediction)
            {
                var result = model.Predict(input);
                var maxScore = result.Score.Max();
                var resultString = labels[Array.IndexOf(result.Score, maxScore)];
                if (predictionResult.ContainsKey(resultString))
                {
                    predictionResult[resultString] += 1;
                }
                else
                {
                    predictionResult.Add(resultString, 1);
                }
            }
            return predictionResult.FirstOrDefault(x => x.Value == predictionResult.Values.Max()).Key;
        }

        public string PredictSingle(InputData input)
        {
            Dictionary<string, int> predictionResult = new Dictionary<string, int>();
            foreach (var model in prediction)
            {
                var result = model.Predict(input);
                var maxScore = result.Score.Max();
                var resultString = labels[Array.IndexOf(result.Score, maxScore)];
                if (predictionResult.ContainsKey(resultString))
                {
                    predictionResult[resultString] += 1;
                }
                else
                {
                    predictionResult.Add(resultString, 1);
                }
            }
            return predictionResult.FirstOrDefault(x => x.Value == predictionResult.Values.Max()).Key;
        }

        public static string[] labels = new string[] {
            "1", "10","2","3","4","5","6","7","8","9","A","Apa","B","Belum",
            "Berapa","C","D","Dia","E","F","G","H","Halo","I","Isyarat","J",
            "K","Kamu","L","M","Malam","N","O","Ok","P","Pagi",
            "Q","R","S","Sekarang","Siang","Siapa","Sore",
            "Sudah","T" ,"Teman-teman","Terimakasih","U","V","W",
            "X","Y","Z"
        };
    }

    public enum MLNetClassifier { L_BFGS, LightGBM}
}
