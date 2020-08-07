using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnnxPredictionEngine
{
    public class OnnxSignPrediction
    {
        private InferenceSession session;
        private static OnnxSignPrediction _instance;
        public static OnnxSignPrediction Instance { 
            get
            {
                if (_instance == null)
                    _instance = new OnnxSignPrediction();
                return _instance;
            } 
        }

        private OnnxSignPrediction()
        {
            session = new InferenceSession("AllModel.onnx");
        }

        public string Infer(float[] input)
        {
            string result_str = "";
            int[] dimensions = { 12300 };    // and the dimensions of the input is stored here
            Tensor<float> t1 = new DenseTensor<float>(input, dimensions);


            var inputs = new List<NamedOnnxValue>()
            {
                 NamedOnnxValue.CreateFromTensor<float>("input", t1),
            };
            using (var results = session.Run(inputs))
            {
                // manipulate the results
                var result = results.First();
                var probs = ((DenseTensor<float>)result.Value).ToArray();
                result_str = labels[Array.IndexOf(probs, probs.Max())];
            }
            return result_str;
        }

        private static string[] labels = new string[] {
            "1", "10","2","3","4","5","6","7","8","9","A","Apa","B","Belum",
            "Berapa","C","D","Dia","E","F","G","H","Halo","I","Isyarat","J",
            "K","Kamu","L","M","Malam","N","O","Ok","P","Pagi",
            "Q","R","S","Sekarang","Siang","Siapa","Sore",
            "Sudah","T" ,"Teman-teman","Terimakasih","U","V","W",
            "X","Y","Z"
        };
    }
}
