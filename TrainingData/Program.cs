using System;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;


namespace TrainingData
{

    class InputData
    {
        [ColumnName("PixelValues")]
        [LoadColumn(0,12499)]
        [VectorType(12499)]
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

    class Program
    {
        private static MLContext mlContext;
        static void Main(string[] args)
        {
            mlContext = new MLContext(seed: 0 );
            var allData = mlContext.Data.LoadFromTextFile<InputData>(path: Environment.CurrentDirectory+"/allData.txt",
                        hasHeader: false,
                        separatorChar: ','
                        );

            DataOperationsCatalog.TrainTestData dataSplit = mlContext.Data.TrainTestSplit(allData, testFraction: 0.2);

            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(InputData.Sign)).
                    Append(mlContext.Transforms.Concatenate("Features", nameof(InputData.PixelValues)).AppendCacheCheckpoint(mlContext));

            // STEP 3: Set the training algorithm, then create and config the modelBuilder
            var trainer = mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(labelColumnName: "Label", featureColumnName: "Features");
            var trainingPipeline = dataProcessPipeline.Append(trainer)
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue("Sign", "Label"));


            // STEP 4: Train the model fitting to the DataSet            
            ITransformer trainedModel = trainingPipeline.Fit(dataSplit.TrainSet);
            var predictions = trainedModel.Transform(dataSplit.TestSet);
            var metrics = mlContext.MulticlassClassification.Evaluate(data: predictions, labelColumnName: "Label", scoreColumnName: "Score");


            mlContext.Model.Save(trainedModel, dataSplit.TrainSet.Schema, Environment.CurrentDirectory+"\\model.zip");

            Console.WriteLine("The model is saved");

        }
    }
}
