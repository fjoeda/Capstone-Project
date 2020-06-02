using Intel.RealSense;
using Intel.RealSense.Hand;
using RealsenseHandler;
using RealsenseHandler.RsHand;
using RealsenseHandML.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandConsole
{
    class Program
    {
        static RealsenseManager rm;
        static void Main(string[] args)
        {
            rm = new RealsenseManager();
            rm.HandDataChanged += Rm_handDataChanged;
            rm.Init();
            Console.ReadLine();
            rm.Dispose();
        }

        private static void Rm_handDataChanged(RealsenseHandler.RsHand.Hand rightHand, RealsenseHandler.RsHand.Hand leftHand)
        {
            ModelInput handInput = new ModelInput();
            try
            {
                if (rightHand != null && rightHand.IsTracked)
                {
                    //float flexion = rightHand.Index.Foldness;
                    //Console.WriteLine(flexion);
                    MapHandRightDataToModelInput(ref rightHand, ref handInput);
                }
                else
                {

                }

                if (leftHand != null && leftHand.IsTracked)
                {
                    MapHandRightDataToModelInput(ref leftHand, ref handInput);
                }
                else
                {

                }

                ModelOutput modelOutput = ConsumeModel.Predict(handInput);
                if(modelOutput.Score.Max()>0.85)
                    Console.WriteLine($"Predicted Value : {modelOutput.Prediction}, Score : {modelOutput.Score.Max()}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.InnerException);

            }
            
        }

        private static void MapHandRightDataToModelInput(ref Hand hand, ref ModelInput input)
        {
            input.R_Thumb_flexion = hand.Thumb.Foldness;

            input.R_Rot_Thumb_base_w = hand.Thumb.JointsOrientation[0].w;
            input.R_Rot_Thumb_base_x = hand.Thumb.JointsOrientation[0].x;
            input.R_Rot_Thumb_base_y = hand.Thumb.JointsOrientation[0].y;
            input.R_Rot_Thumb_base_z = hand.Thumb.JointsOrientation[0].z;

            input.R_Rot_Thumb_jt1_w = hand.Thumb.JointsOrientation[1].w;
            input.R_Rot_Thumb_jt1_x = hand.Thumb.JointsOrientation[1].x;
            input.R_Rot_Thumb_jt1_y = hand.Thumb.JointsOrientation[1].y;
            input.R_Rot_Thumb_jt1_z = hand.Thumb.JointsOrientation[1].z;

            input.R_Rot_Thumb_jt2_w = hand.Thumb.JointsOrientation[2].w;
            input.R_Rot_Thumb_jt2_x = hand.Thumb.JointsOrientation[2].x;
            input.R_Rot_Thumb_jt2_y = hand.Thumb.JointsOrientation[2].y;
            input.R_Rot_Thumb_jt2_z = hand.Thumb.JointsOrientation[2].z;

            input.R_Rot_Thumb_tip_w = hand.Thumb.JointsOrientation[3].w;
            input.R_Rot_Thumb_tip_x = hand.Thumb.JointsOrientation[3].x;
            input.R_Rot_Thumb_tip_y = hand.Thumb.JointsOrientation[3].y;
            input.R_Rot_Thumb_tip_z = hand.Thumb.JointsOrientation[3].z;

            input.R_Index_flexion = hand.Index.Foldness;

            input.R_Rot_Index_base_w = hand.Index.JointsOrientation[0].w;
            input.R_Rot_Index_base_x = hand.Index.JointsOrientation[0].x;
            input.R_Rot_Index_base_y = hand.Index.JointsOrientation[0].y;
            input.R_Rot_Index_base_z = hand.Index.JointsOrientation[0].z;

            input.R_Rot_Index_jt1_w = hand.Index.JointsOrientation[1].w;
            input.R_Rot_Index_jt1_x = hand.Index.JointsOrientation[1].x;
            input.R_Rot_Index_jt1_y = hand.Index.JointsOrientation[1].y;
            input.R_Rot_Index_jt1_z = hand.Index.JointsOrientation[1].z;

            input.R_Rot_Index_jt2_w = hand.Index.JointsOrientation[2].w;
            input.R_Rot_Index_jt2_x = hand.Index.JointsOrientation[2].x;
            input.R_Rot_Index_jt2_y = hand.Index.JointsOrientation[2].y;
            input.R_Rot_Index_jt2_z = hand.Index.JointsOrientation[2].z;

            input.R_Rot_Index_tip_w = hand.Index.JointsOrientation[3].w;
            input.R_Rot_Index_tip_x = hand.Index.JointsOrientation[3].x;
            input.R_Rot_Index_tip_y = hand.Index.JointsOrientation[3].y;
            input.R_Rot_Index_tip_z = hand.Index.JointsOrientation[3].z;

            input.R_Middle_flexion = hand.Middle.Foldness;

            input.R_Rot_Middle_base_w = hand.Middle.JointsOrientation[0].w;
            input.R_Rot_Middle_base_x = hand.Middle.JointsOrientation[0].x;
            input.R_Rot_Middle_base_y = hand.Middle.JointsOrientation[0].y;
            input.R_Rot_Middle_base_z = hand.Middle.JointsOrientation[0].z;

            input.R_Rot_Middle_jt1_w = hand.Middle.JointsOrientation[1].w;
            input.R_Rot_Middle_jt1_x = hand.Middle.JointsOrientation[1].x;
            input.R_Rot_Middle_jt1_y = hand.Middle.JointsOrientation[1].y;
            input.R_Rot_Middle_jt1_z = hand.Middle.JointsOrientation[1].z;

            input.R_Rot_Middle_jt2_w = hand.Middle.JointsOrientation[2].w;
            input.R_Rot_Middle_jt2_x = hand.Middle.JointsOrientation[2].x;
            input.R_Rot_Middle_jt2_y = hand.Middle.JointsOrientation[2].y;
            input.R_Rot_Middle_jt2_z = hand.Middle.JointsOrientation[2].z;

            input.R_Rot_Middle_tip_w = hand.Middle.JointsOrientation[3].w;
            input.R_Rot_Middle_tip_x = hand.Middle.JointsOrientation[3].x;
            input.R_Rot_Middle_tip_y = hand.Middle.JointsOrientation[3].y;
            input.R_Rot_Middle_tip_z = hand.Middle.JointsOrientation[3].z;

            input.R_Ring_flexion = hand.Ring.Foldness;

            input.R_Rot_Ring_base_w = hand.Ring.JointsOrientation[0].w;
            input.R_Rot_Ring_base_x = hand.Ring.JointsOrientation[0].x;
            input.R_Rot_Ring_base_y = hand.Ring.JointsOrientation[0].y;
            input.R_Rot_Ring_base_z = hand.Ring.JointsOrientation[0].z;

            input.R_Rot_Ring_jt1_w = hand.Ring.JointsOrientation[1].w;
            input.R_Rot_Ring_jt1_x = hand.Ring.JointsOrientation[1].x;
            input.R_Rot_Ring_jt1_y = hand.Ring.JointsOrientation[1].y;
            input.R_Rot_Ring_jt1_z = hand.Ring.JointsOrientation[1].z;

            input.R_Rot_Ring_jt2_w = hand.Ring.JointsOrientation[2].w;
            input.R_Rot_Ring_jt2_x = hand.Ring.JointsOrientation[2].x;
            input.R_Rot_Ring_jt2_y = hand.Ring.JointsOrientation[2].y;
            input.R_Rot_Ring_jt2_z = hand.Ring.JointsOrientation[2].z;

            input.R_Rot_Ring_tip_w = hand.Ring.JointsOrientation[3].w;
            input.R_Rot_Ring_tip_x = hand.Ring.JointsOrientation[3].x;
            input.R_Rot_Ring_tip_y = hand.Ring.JointsOrientation[3].y;
            input.R_Rot_Ring_tip_z = hand.Ring.JointsOrientation[3].z;

            input.R_Pingky_flexion = hand.Pingky.Foldness;

            input.R_Rot_Pingky_base_w = hand.Pingky.JointsOrientation[0].w;
            input.R_Rot_Pingky_base_x = hand.Pingky.JointsOrientation[0].x;
            input.R_Rot_Pingky_base_y = hand.Pingky.JointsOrientation[0].y;
            input.R_Rot_Pingky_base_z = hand.Pingky.JointsOrientation[0].z;

            input.R_Rot_Pingky_jt1_w = hand.Pingky.JointsOrientation[1].w;
            input.R_Rot_Pingky_jt1_x = hand.Pingky.JointsOrientation[1].x;
            input.R_Rot_Pingky_jt1_y = hand.Pingky.JointsOrientation[1].y;
            input.R_Rot_Pingky_jt1_z = hand.Pingky.JointsOrientation[1].z;

            input.R_Rot_Pingky_jt2_w = hand.Pingky.JointsOrientation[2].w;
            input.R_Rot_Pingky_jt2_x = hand.Pingky.JointsOrientation[2].x;
            input.R_Rot_Pingky_jt2_y = hand.Pingky.JointsOrientation[2].y;
            input.R_Rot_Pingky_jt2_z = hand.Pingky.JointsOrientation[2].z;

            input.R_Rot_Pingky_tip_w = hand.Pingky.JointsOrientation[3].w;
            input.R_Rot_Pingky_tip_x = hand.Pingky.JointsOrientation[3].x;
            input.R_Rot_Pingky_tip_y = hand.Pingky.JointsOrientation[3].y;
            input.R_Rot_Pingky_tip_z = hand.Pingky.JointsOrientation[3].z;
        }

        private static void MapHandLeftDataToModelInput(ref Hand hand, ref ModelInput input)
        {
            input.L_Thumb_flexion = hand.Thumb.Foldness;

            input.L_Rot_Thumb_base_w = hand.Thumb.JointsOrientation[0].w;
            input.L_Rot_Thumb_base_x = hand.Thumb.JointsOrientation[0].x;
            input.L_Rot_Thumb_base_y = hand.Thumb.JointsOrientation[0].y;
            input.L_Rot_Thumb_base_z = hand.Thumb.JointsOrientation[0].z;

            input.L_Rot_Thumb_jt1_w = hand.Thumb.JointsOrientation[1].w;
            input.L_Rot_Thumb_jt1_x = hand.Thumb.JointsOrientation[1].x;
            input.L_Rot_Thumb_jt1_y = hand.Thumb.JointsOrientation[1].y;
            input.L_Rot_Thumb_jt1_z = hand.Thumb.JointsOrientation[1].z;

            input.L_Rot_Thumb_jt2_w = hand.Thumb.JointsOrientation[2].w;
            input.L_Rot_Thumb_jt2_x = hand.Thumb.JointsOrientation[2].x;
            input.L_Rot_Thumb_jt2_y = hand.Thumb.JointsOrientation[2].y;
            input.L_Rot_Thumb_jt2_z = hand.Thumb.JointsOrientation[2].z;

            input.L_Rot_Thumb_tip_w = hand.Thumb.JointsOrientation[3].w;
            input.L_Rot_Thumb_tip_x = hand.Thumb.JointsOrientation[3].x;
            input.L_Rot_Thumb_tip_y = hand.Thumb.JointsOrientation[3].y;
            input.L_Rot_Thumb_tip_z = hand.Thumb.JointsOrientation[3].z;

            input.L_Index_flexion = hand.Index.Foldness;

            input.L_Rot_Index_base_w = hand.Index.JointsOrientation[0].w;
            input.L_Rot_Index_base_x = hand.Index.JointsOrientation[0].x;
            input.L_Rot_Index_base_y = hand.Index.JointsOrientation[0].y;
            input.L_Rot_Index_base_z = hand.Index.JointsOrientation[0].z;

            input.L_Rot_Index_jt1_w = hand.Index.JointsOrientation[1].w;
            input.L_Rot_Index_jt1_x = hand.Index.JointsOrientation[1].x;
            input.L_Rot_Index_jt1_y = hand.Index.JointsOrientation[1].y;
            input.L_Rot_Index_jt1_z = hand.Index.JointsOrientation[1].z;

            input.L_Rot_Index_jt2_w = hand.Index.JointsOrientation[2].w;
            input.L_Rot_Index_jt2_x = hand.Index.JointsOrientation[2].x;
            input.L_Rot_Index_jt2_y = hand.Index.JointsOrientation[2].y;
            input.L_Rot_Index_jt2_z = hand.Index.JointsOrientation[2].z;

            input.L_Rot_Index_tip_w = hand.Index.JointsOrientation[3].w;
            input.L_Rot_Index_tip_x = hand.Index.JointsOrientation[3].x;
            input.L_Rot_Index_tip_y = hand.Index.JointsOrientation[3].y;
            input.L_Rot_Index_tip_z = hand.Index.JointsOrientation[3].z;

            input.L_Middle_flexion = hand.Middle.Foldness;

            input.L_Rot_Middle_base_w = hand.Middle.JointsOrientation[0].w;
            input.L_Rot_Middle_base_x = hand.Middle.JointsOrientation[0].x;
            input.L_Rot_Middle_base_y = hand.Middle.JointsOrientation[0].y;
            input.L_Rot_Middle_base_z = hand.Middle.JointsOrientation[0].z;

            input.L_Rot_Middle_jt1_w = hand.Middle.JointsOrientation[1].w;
            input.L_Rot_Middle_jt1_x = hand.Middle.JointsOrientation[1].x;
            input.L_Rot_Middle_jt1_y = hand.Middle.JointsOrientation[1].y;
            input.L_Rot_Middle_jt1_z = hand.Middle.JointsOrientation[1].z;

            input.L_Rot_Middle_jt2_w = hand.Middle.JointsOrientation[2].w;
            input.L_Rot_Middle_jt2_x = hand.Middle.JointsOrientation[2].x;
            input.L_Rot_Middle_jt2_y = hand.Middle.JointsOrientation[2].y;
            input.L_Rot_Middle_jt2_z = hand.Middle.JointsOrientation[2].z;

            input.L_Rot_Middle_tip_w = hand.Middle.JointsOrientation[3].w;
            input.L_Rot_Middle_tip_x = hand.Middle.JointsOrientation[3].x;
            input.L_Rot_Middle_tip_y = hand.Middle.JointsOrientation[3].y;
            input.L_Rot_Middle_tip_z = hand.Middle.JointsOrientation[3].z;

            input.L_Ring_flexion = hand.Ring.Foldness;

            input.L_Rot_Ring_base_w = hand.Ring.JointsOrientation[0].w;
            input.L_Rot_Ring_base_x = hand.Ring.JointsOrientation[0].x;
            input.L_Rot_Ring_base_y = hand.Ring.JointsOrientation[0].y;
            input.L_Rot_Ring_base_z = hand.Ring.JointsOrientation[0].z;

            input.L_Rot_Ring_jt1_w = hand.Ring.JointsOrientation[1].w;
            input.L_Rot_Ring_jt1_x = hand.Ring.JointsOrientation[1].x;
            input.L_Rot_Ring_jt1_y = hand.Ring.JointsOrientation[1].y;
            input.L_Rot_Ring_jt1_z = hand.Ring.JointsOrientation[1].z;

            input.L_Rot_Ring_jt2_w = hand.Ring.JointsOrientation[2].w;
            input.L_Rot_Ring_jt2_x = hand.Ring.JointsOrientation[2].x;
            input.L_Rot_Ring_jt2_y = hand.Ring.JointsOrientation[2].y;
            input.L_Rot_Ring_jt2_z = hand.Ring.JointsOrientation[2].z;

            input.L_Rot_Ring_tip_w = hand.Ring.JointsOrientation[3].w;
            input.L_Rot_Ring_tip_x = hand.Ring.JointsOrientation[3].x;
            input.L_Rot_Ring_tip_y = hand.Ring.JointsOrientation[3].y;
            input.L_Rot_Ring_tip_z = hand.Ring.JointsOrientation[3].z;

            input.L_Pingky_flexion = hand.Pingky.Foldness;

            input.L_Rot_Pingky_base_w = hand.Pingky.JointsOrientation[0].w;
            input.L_Rot_Pingky_base_x = hand.Pingky.JointsOrientation[0].x;
            input.L_Rot_Pingky_base_y = hand.Pingky.JointsOrientation[0].y;
            input.L_Rot_Pingky_base_z = hand.Pingky.JointsOrientation[0].z;

            input.L_Rot_Pingky_jt1_w = hand.Pingky.JointsOrientation[1].w;
            input.L_Rot_Pingky_jt1_x = hand.Pingky.JointsOrientation[1].x;
            input.L_Rot_Pingky_jt1_y = hand.Pingky.JointsOrientation[1].y;
            input.L_Rot_Pingky_jt1_z = hand.Pingky.JointsOrientation[1].z;

            input.L_Rot_Pingky_jt2_w = hand.Pingky.JointsOrientation[2].w;
            input.L_Rot_Pingky_jt2_x = hand.Pingky.JointsOrientation[2].x;
            input.L_Rot_Pingky_jt2_y = hand.Pingky.JointsOrientation[2].y;
            input.L_Rot_Pingky_jt2_z = hand.Pingky.JointsOrientation[2].z;

            input.L_Rot_Pingky_tip_w = hand.Pingky.JointsOrientation[3].w;
            input.L_Rot_Pingky_tip_x = hand.Pingky.JointsOrientation[3].x;
            input.L_Rot_Pingky_tip_y = hand.Pingky.JointsOrientation[3].y;
            input.L_Rot_Pingky_tip_z = hand.Pingky.JointsOrientation[3].z;
        }

        /*
        private void DisplayDataExample()
        {
            var indexfinger = rightHand.Index.JointsPosition;
            var foldness = rightHand.Index.Foldness;
            var orientation = rightHand.orientation.Quarternion;
            Console.WriteLine($"Quarternion : w = {orientation.w}   x = {orientation.x} y = {orientation.y} z = {orientation.z}");
            //Console.WriteLine($"Index finger joint position :  x = {indexfinger[0].x} y = {indexfinger[0].y} z = {indexfinger[0].z}");
            Console.WriteLine($"Index finger foldness : {foldness}");
        }
        */
    }
}
