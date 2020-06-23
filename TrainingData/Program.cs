using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;


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
        
        public string PredictedLabel;

        [ColumnName("Score")]
        public float[] Score;
    }

    class Program
    {
        private static MLContext mlContext;
        static void Main(string[] args)
        {
            string name = "Linear SVM OVA";
            Console.WriteLine($"Begin Tranining With {name}");
            Console.WriteLine("============================================");
            Console.WriteLine("Load Data...");
            mlContext = new MLContext(seed: 0 );
            var allData = mlContext.Data.LoadFromTextFile<InputData>(path: Environment.CurrentDirectory+"/allData.txt",
                        hasHeader: false,
                        separatorChar: ','
                        );

            DataOperationsCatalog.TrainTestData dataSplit = mlContext.Data.TrainTestSplit(allData, testFraction: 0.2);
            Console.WriteLine("Creating Pipeline...");
            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(InputData.Sign)).
                    Append(mlContext.Transforms.Concatenate("Features", nameof(InputData.PixelValues)).
                    AppendCacheCheckpoint(mlContext));

            // STEP 3: Set the training algorithm, then create and config the modelBuilder
            //var trainer = mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(labelColumnName: "Label", featureColumnName: "Features");

            var trainer = mlContext.MulticlassClassification.Trainers.OneVersusAll(mlContext.BinaryClassification.Trainers.LinearSvm(labelColumnName: "Label", featureColumnName: "Features"));
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

        }
    }
}
