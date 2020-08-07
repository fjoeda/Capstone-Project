using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using PredictionEngine;

namespace TrainingData
{

    class InputData
    {
        [ColumnName("PixelValues")]
        [LoadColumn(0,12299)]
        [VectorType(12299)]
        public float[] PixelValues;

        [ColumnName("Sign")]
        [LoadColumn(12300)]
        public string Sign;
    }

    class OutPutData
    {
        
        public string Sign;

        [ColumnName("Score")]
        public float[] Score;
    }

    class Program
    {
        private static MLContext mlContext;
        static void Main(string[] args)
        {
            DoEvaluation();
            /*
            string name = "LBFGS SignClassification Model";
            Console.WriteLine($"Begin Tranining With {name}");
            Console.WriteLine("============================================");
            Console.WriteLine("Load Data...");
            mlContext = new MLContext(seed: 0 );
            var allData = mlContext.Data.LoadFromTextFile<InputData>(path: Environment.CurrentDirectory+"/SignCategorical.csv",
                        hasHeader: false,
                        separatorChar: ','
                        );

            DataOperationsCatalog.TrainTestData dataSplit = mlContext.Data.TrainTestSplit(allData, testFraction: 0.2);
            Console.WriteLine("Creating Pipeline...");
            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(InputData.Sign)).
                    Append(mlContext.Transforms.NormalizeMinMax(nameof(InputData.PixelValues), nameof(InputData.PixelValues))).
                    Append(mlContext.Transforms.Concatenate("Features", nameof(InputData.PixelValues)).
                    AppendCacheCheckpoint(mlContext));

            // STEP 3: Set the training algorithm, then create and config the modelBuilder
            var trainer = mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(labelColumnName: "Label", featureColumnName: "Features");

            //var trainer = mlContext.MulticlassClassification.Trainers.OneVersusAll(mlContext.BinaryClassification.Trainers.FastTree(labelColumnName: "Label", featureColumnName: "Features"));
            var trainingPipeline = dataProcessPipeline.Append(trainer)
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue("Sign", "Label"));

            Console.WriteLine("Start Training....");
            // STEP 4: Train the model fitting to the DataSet            
            ITransformer trainedModel = trainingPipeline.Fit(dataSplit.TrainSet);
            var predictions = trainedModel.Transform(dataSplit.TestSet);
            var metrics = mlContext.MulticlassClassification.Evaluate(data: predictions, labelColumnName: "Label", scoreColumnName: "Score");


            mlContext.Model.Save(trainedModel, dataSplit.TrainSet.Schema, Environment.CurrentDirectory+$"\\model_{name}.zip");
            Console.WriteLine("============================================");
            Console.WriteLine("Evaluation");
            Console.WriteLine("============================================");

            Console.WriteLine($"Macro Accuracy : {metrics.MacroAccuracy}");
            Console.WriteLine($"Micro Accuracy : {metrics.MicroAccuracy}");

            Console.WriteLine("============================================");
            Console.WriteLine("The model is saved");
            
            */
        }


        private static void DoEvaluation()
        {
            string title = "Light GBM";
            ISignPreditionEngine preditionEngine = new MLNetHierarchicalSignPrediciton(MLNetClassifier.LightGBM);
            float[] input_feature = new float[12300];
            string[] lines = File.ReadAllLines(Environment.CurrentDirectory + "\\SignTestData.csv");
            int count = 0;
            int totalPred = 0;
            foreach(string line in lines)
            {
                var input = line.Split(',');
                var output = input[input.Length - 1];
                for(int i = 0; i < input.Length-1; i++)
                {
                    input_feature[i] = float.Parse(input[i]);
                }

                var inputData = new PredictionEngine.InputData { PixelValues = input_feature };

                var pred = preditionEngine.PredictSingle(inputData);
                if (pred != null)
                {
                    if (pred.ToLower().Equals(output.ToLower()))
                    {
                        count++;
                    }
                    Console.WriteLine($"Real : {output} ; Pred : {pred}");
                    totalPred++;
                }
                
            }

            var accuracy = (float)count / totalPred;
            Console.WriteLine("====================================");
            Console.WriteLine($"Evaluation for {title}");
            Console.WriteLine("====================================");
            Console.WriteLine($"Accuracy : {accuracy}");
        }
    }
}
