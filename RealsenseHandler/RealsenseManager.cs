using Intel.RealSense.Hand;
using RealsenseHandler.RsHand;
using SampleDX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RS = Intel.RealSense;

namespace RealsenseHandler
{
    // delegate methods
    public delegate void HandDataChanged(Hand rightHand, Hand leftHand);
    public delegate void DataStreamUpdate(string dataStream, string preprocessedStream);
    public delegate void HandImageRetreived(RS.Image handImage);
    public delegate void RGBImageRetreived(RS.Image handImage);
    public delegate void ImageCaptured(Bitmap bitmap);
    public delegate void DepthImageRetreived(RS.Image handImage);
    public class RealsenseManager : IDisposable
    {
        //private attributes
        private readonly RS.SenseManager sm;
        private readonly HandModule handModule;
        private readonly HandConfiguration handConfig;
        private readonly HandData handData;
        readonly CultureInfo ci = new CultureInfo("en-US");
        private readonly HandViewModel data = new HandViewModel();
        private RS.SampleReader rgbReader;
        private RS.SampleReader depthReader;
        //private RS.SenseManager.Handler handler = new RS.SenseManager.Handler();



        //public attribute
        public Hand rightHand;
        public Hand leftHand;
        public System.Drawing.Bitmap handImage;
        public RS.Image rgbImage;
        public RS.Image depthImage;
        public string dataStreamString = "";

        //event
        public event HandDataChanged HandDataChanged;
        public event RGBImageRetreived RGBImageRetreived;
        public event DepthImageRetreived DepthImageRetreived;
        public event DataStreamUpdate DataStreamUpdate;
        public event ImageCaptured ImageCaptured;

        public RealsenseManager()
        {
            sm = RS.SenseManager.CreateInstance();
            rgbReader = RS.SampleReader.Activate(sm);
            depthReader = RS.SampleReader.Activate(sm);
            rgbReader.EnableStream(RS.StreamType.STREAM_TYPE_COLOR);
            depthReader.EnableStream(RS.StreamType.STREAM_TYPE_DEPTH);
            rgbReader.SampleArrived += RgbReader_SampleArrived;
            depthReader.SampleArrived += DepthReader_SampleArrived;

            // config hand


            handModule = HandModule.Activate(sm);
            handConfig = handModule.CreateActiveConfiguration();
            handConfig.TrackedJointsEnabled = true;
            handConfig.StabilizerEnabled = true;
            handConfig.TrackingMode = TrackingModeType.TRACKING_MODE_FULL_HAND;
            handConfig.ApplyChanges();
            handData = handModule.CreateOutput();

            handModule.FrameProcessed += HandModule_FrameProcessed;
            
            

        }

        private void HandModule_FrameProcessed(object sender, RS.FrameProcessedEventArgs args)
        {
            handData.Update();
            IHand[] ileftHand = null;
            IHand[] irightHand = null;

            if(handData.QueryHandData(AccessOrderType.ACCESS_ORDER_LEFT_HANDS,out ileftHand) == RS.Status.STATUS_NO_ERROR)
            {
                leftHand = new Hand(ileftHand[0]);
            }
            else
            {
                leftHand = null;
            }

            if (handData.QueryHandData(AccessOrderType.ACCESS_ORDER_RIGHT_HANDS, out irightHand) == RS.Status.STATUS_NO_ERROR)
            {
                rightHand = new Hand(irightHand[0]);
            }
            else
            {
                rightHand = null;
            }

            if (irightHand != null || ileftHand != null)
            {
                HandDataChanged.Invoke(rightHand, leftHand);
                DataStreamUpdate.Invoke(GetAllHandDataAttribute(rightHand, leftHand),GetPreprocessedHand(rightHand,leftHand));
            }
            sm.ReleaseFrame();

        }

        private void DepthReader_SampleArrived(object sender, RS.SampleArrivedEventArgs args)
        {
            if (args.sample != null)
            {
                DepthImageRetreived.Invoke(args.sample.Depth);
            }
        }

        private void RgbReader_SampleArrived(object sender, RS.SampleArrivedEventArgs args)
        {
            if (args.sample != null)
            {
                RGBImageRetreived.Invoke(args.sample.Color);
            }
        }

        public void Init()
        {
            sm.Init();
            sm.StreamFrames(false);
        }
        public void Stop()
        {
            sm.Close();
        }

        public void Dispose()
        {
            handData.Dispose();
            sm.Dispose();
        }
        
        public string GetPreprocessedHand(Hand rightHand, Hand leftHand)
        {
            string dataStringStream = "";
            if(rightHand!=null && rightHand.IsTracked)
            {
                dataStringStream += (new HandPreprocessed(rightHand)).GetStream();
            }
            else
            {
                for(int i = 0; i < 5; i++)
                {
                    dataStringStream += "0,";
                    for(int j = 0; j < 8; j++)
                    {
                        dataStringStream += "0,0,0,";
                    }
                    for (int j = 0; j < 4; j++)
                    {
                        dataStringStream += "0,0,0,0,";
                    }
                }
                //dataStreamString += "0,0,0,0,0,0,0,0,";
            }

            if (leftHand != null && leftHand.IsTracked)
            {
                dataStringStream += (new HandPreprocessed(leftHand)).GetStream();
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    dataStringStream += "0,";
                    for (int j = 0; j < 8; j++)
                    {
                        dataStringStream += "0,0,0,";
                    }
                    for (int j = 0; j < 4; j++)
                    {
                        dataStringStream += "0,0,0,0,";
                    }
                }
                //dataStreamString += "0,0,0,0,0,0,0,0,";
            }

            return dataStringStream;
        }

        public string GetAllHandDataAttribute(Hand rightHand, Hand leftHand)
        {
            dataStreamString = "";
            try
            {
                if(rightHand!=null && rightHand.IsTracked)
                {
                    PopulateRightThumbData(rightHand, true, true, true);
                    PopulateRightIndexData(rightHand, true, true, true);
                    PopulateRightMiddleData(rightHand, true, true, true);
                    PopulateRightRingData(rightHand, true, true, true);
                    PopulateRightPingkyData(rightHand, true, true, true);
                }
                else
                {
                    for(int i = 0; i < 5; i++)
                    {
                        ShowNullHandDataOnTextbox(true, true, true);
                    }
                }

                if (leftHand != null && leftHand.IsTracked)
                {
                    PopulateLeftThumbData(leftHand, true, true, true);
                    PopulateLeftIndexData(leftHand, true, true, true);
                    PopulateLeftMiddleData(leftHand, true, true, true);
                    PopulateLeftRingData(leftHand, true, true, true);
                    PopulateLeftPingkyData(leftHand, true, true, true);
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        ShowNullHandDataOnTextbox(true, true, true);
                    }
                }
            }
            catch (Exception)
            {

            }

            return dataStreamString;
        }
        
        #region Hand Functions

        private void PopulateRightThumbData(Hand hand, bool flexsion, bool position, bool rotation)
        {
            //compose string
            var flexionString = "";
            var positionString = "";
            var rotationString = "";
            try
            {

                data.ThumbFlexsionRight = hand.Thumb.Foldness.ToString();

                //Position
                data.ThumbPositionTipRight = PopulateFingerPositionDataToString(hand.Thumb.JointsPosition[3]);
                data.ThumbPositionJT2Right = PopulateFingerPositionDataToString(hand.Thumb.JointsPosition[2]);
                data.ThumbPositionJT1Right = PopulateFingerPositionDataToString(hand.Thumb.JointsPosition[1]);
                data.ThumbPositionBaseRight = PopulateFingerPositionDataToString(hand.Thumb.JointsPosition[0]);
                //Rotation
                data.ThumbRotationTipRight = PopulateFingerRotationDataToString(hand.Thumb.JointsOrientation[3]);
                data.ThumbRotationJT2Right = PopulateFingerRotationDataToString(hand.Thumb.JointsOrientation[2]);
                data.ThumbRotationJT1Right = PopulateFingerRotationDataToString(hand.Thumb.JointsOrientation[1]);
                data.ThumbRotationBaseRight = PopulateFingerRotationDataToString(hand.Thumb.JointsOrientation[0]);

                if (flexsion)
                {
                    //Flexion
                    flexionString = data.ThumbFlexsionRight + ",";
                }

                if (position)
                {
                    //Position
                    positionString += data.ThumbPositionTipRight + ",";
                    positionString += data.ThumbPositionJT2Right + ",";
                    positionString += data.ThumbPositionJT1Right + ",";
                    positionString += data.ThumbPositionBaseRight + ",";
                }

                if (rotation)
                {
                    //Rotation
                    rotationString += data.ThumbRotationTipRight + ",";
                    rotationString += data.ThumbRotationJT2Right + ",";
                    rotationString += data.ThumbRotationJT1Right + ",";
                    rotationString += data.ThumbRotationBaseRight + ",";
                }

            }
            catch (Exception e)
            {

            }

            ComposeDataStreamString(flexsion, position, rotation, flexionString, positionString, rotationString);

        }

        private void PopulateRightIndexData(Hand hand, bool flexsion, bool position, bool rotation)
        {
            //compose string
            var flexionString = "";
            var positionString = "";
            var rotationString = "";
            try
            {
                data.IndexFlexsionRight = hand.Index.Foldness.ToString();
                //Position
                data.IndexPositionTipRight = PopulateFingerPositionDataToString(hand.Index.JointsPosition[3]);
                data.IndexPositionJT2Right = PopulateFingerPositionDataToString(hand.Index.JointsPosition[2]);
                data.IndexPositionJT1Right = PopulateFingerPositionDataToString(hand.Index.JointsPosition[1]);
                data.IndexPositionBaseRight = PopulateFingerPositionDataToString(hand.Index.JointsPosition[0]);
                //Rotation
                data.IndexRotationTipRight = PopulateFingerRotationDataToString(hand.Index.JointsOrientation[3]);
                data.IndexRotationJT2Right = PopulateFingerRotationDataToString(hand.Index.JointsOrientation[2]);
                data.IndexRotationJT1Right = PopulateFingerRotationDataToString(hand.Index.JointsOrientation[1]);
                data.IndexRotationBaseRight = PopulateFingerRotationDataToString(hand.Index.JointsOrientation[0]);

                if (flexsion)
                {
                    //Flexion
                    flexionString = data.IndexFlexsionRight + ",";
                }

                if (position)
                {
                    //Position
                    positionString += data.IndexPositionTipRight + ",";
                    positionString += data.IndexPositionJT2Right + ",";
                    positionString += data.IndexPositionJT1Right + ",";
                    positionString += data.IndexPositionBaseRight + ",";
                }

                if (rotation)
                {
                    //Rotation
                    rotationString += data.IndexRotationTipRight + ",";
                    rotationString += data.IndexRotationJT2Right + ",";
                    rotationString += data.IndexRotationJT1Right + ",";
                    rotationString += data.IndexRotationBaseRight + ",";
                }
            }
            catch (Exception)
            {

            }
            ComposeDataStreamString(flexsion, position, rotation, flexionString, positionString, rotationString);
        }

        private void PopulateRightMiddleData(Hand hand, bool flexsion, bool position, bool rotation)
        {
            //compose string
            var flexionString = "";
            var positionString = "";
            var rotationString = "";
            try
            {
                data.MiddleFlexsionRight = hand.Middle.Foldness.ToString();
                //Position
                data.MiddlePositionTipRight = PopulateFingerPositionDataToString(hand.Middle.JointsPosition[3]);
                data.MiddlePositionJT2Right = PopulateFingerPositionDataToString(hand.Middle.JointsPosition[2]);
                data.MiddlePositionJT1Right = PopulateFingerPositionDataToString(hand.Middle.JointsPosition[1]);
                data.MiddlePositionBaseRight = PopulateFingerPositionDataToString(hand.Middle.JointsPosition[0]);
                //Rotation
                data.MiddleRotationTipRight = PopulateFingerRotationDataToString(hand.Middle.JointsOrientation[3]);
                data.MiddleRotationJT2Right = PopulateFingerRotationDataToString(hand.Middle.JointsOrientation[2]);
                data.MiddleRotationJT1Right = PopulateFingerRotationDataToString(hand.Middle.JointsOrientation[1]);
                data.MiddleRotationBaseRight = PopulateFingerRotationDataToString(hand.Middle.JointsOrientation[0]);

                if (flexsion)
                {
                    //Flexion
                    flexionString = data.MiddleFlexsionRight + ",";
                }

                if (position)
                {
                    //Position
                    positionString += data.MiddlePositionTipRight + ",";
                    positionString += data.MiddlePositionJT2Right + ",";
                    positionString += data.MiddlePositionJT1Right + ",";
                    positionString += data.MiddlePositionBaseRight + ",";
                }

                if (rotation)
                {
                    //Rotation
                    rotationString += data.MiddleRotationTipRight + ",";
                    rotationString += data.MiddleRotationJT2Right + ",";
                    rotationString += data.MiddleRotationJT1Right + ",";
                    rotationString += data.MiddleRotationBaseRight + ",";
                }
            }
            catch (Exception)
            {

            }
            ComposeDataStreamString(flexsion, position, rotation, flexionString, positionString, rotationString);
        }

        private void PopulateRightRingData(Hand hand, bool flexsion, bool position, bool rotation)
        {
            //compose string
            var flexionString = "";
            var positionString = "";
            var rotationString = "";
            try
            {
                data.RingFlexsionRight = hand.Ring.Foldness.ToString();
                //Position
                data.RingPositionTipRight = PopulateFingerPositionDataToString(hand.Ring.JointsPosition[3]);
                data.RingPositionJT2Right = PopulateFingerPositionDataToString(hand.Ring.JointsPosition[2]);
                data.RingPositionJT1Right = PopulateFingerPositionDataToString(hand.Ring.JointsPosition[1]);
                data.RingPositionBaseRight = PopulateFingerPositionDataToString(hand.Ring.JointsPosition[0]);
                //Rotation
                data.RingRotationTipRight = PopulateFingerRotationDataToString(hand.Ring.JointsOrientation[3]);
                data.RingRotationJT2Right = PopulateFingerRotationDataToString(hand.Ring.JointsOrientation[2]);
                data.RingRotationJT1Right = PopulateFingerRotationDataToString(hand.Ring.JointsOrientation[1]);
                data.RingRotationBaseRight = PopulateFingerRotationDataToString(hand.Ring.JointsOrientation[0]);

                if (flexsion)
                {
                    //Flexion
                    flexionString = data.RingFlexsionRight + ",";
                }

                if (position)
                {
                    //Position
                    positionString += data.RingPositionTipRight + ",";
                    positionString += data.RingPositionJT2Right + ",";
                    positionString += data.RingPositionJT1Right + ",";
                    positionString += data.RingPositionBaseRight + ",";
                }

                if (rotation)
                {
                    //Rotation
                    rotationString += data.RingRotationTipRight + ",";
                    rotationString += data.RingRotationJT2Right + ",";
                    rotationString += data.RingRotationJT1Right + ",";
                    rotationString += data.RingRotationBaseRight + ",";
                }
            }
            catch (Exception)
            {

            }
            ComposeDataStreamString(flexsion, position, rotation, flexionString, positionString, rotationString);
        }

        private void PopulateRightPingkyData(Hand hand, bool flexsion, bool position, bool rotation)
        {
            //compose string
            var flexionString = "";
            var positionString = "";
            var rotationString = "";
            try
            {
                data.PingkyFlexsionRight = hand.Pingky.Foldness.ToString();
                //Position
                data.PingkyPositionTipRight = PopulateFingerPositionDataToString(hand.Pingky.JointsPosition[3]);
                data.PingkyPositionJT2Right = PopulateFingerPositionDataToString(hand.Pingky.JointsPosition[2]);
                data.PingkyPositionJT1Right = PopulateFingerPositionDataToString(hand.Pingky.JointsPosition[1]);
                data.PingkyPositionBaseRight = PopulateFingerPositionDataToString(hand.Pingky.JointsPosition[0]);
                //Rotation
                data.PingkyRotationTipRight = PopulateFingerRotationDataToString(hand.Pingky.JointsOrientation[3]);
                data.PingkyRotationJT2Right = PopulateFingerRotationDataToString(hand.Pingky.JointsOrientation[2]);
                data.PingkyRotationJT1Right = PopulateFingerRotationDataToString(hand.Pingky.JointsOrientation[1]);
                data.PingkyRotationBaseRight = PopulateFingerRotationDataToString(hand.Pingky.JointsOrientation[0]);

                if (flexsion)
                {
                    //Flexion
                    flexionString = data.PingkyFlexsionRight + ",";
                }

                if (position)
                {
                    //Position
                    positionString += data.PingkyPositionTipRight + ",";
                    positionString += data.PingkyPositionJT2Right + ",";
                    positionString += data.PingkyPositionJT1Right + ",";
                    positionString += data.PingkyPositionBaseRight + ",";
                }

                if (rotation)
                {
                    //Rotation
                    rotationString += data.PingkyRotationTipRight + ",";
                    rotationString += data.PingkyRotationJT2Right + ",";
                    rotationString += data.PingkyRotationJT1Right + ",";
                    rotationString += data.PingkyRotationBaseRight + ",";
                }
            }
            catch (Exception)
            {

            }
            ComposeDataStreamString(flexsion, position, rotation, flexionString, positionString, rotationString);
        }

        private void PopulateLeftThumbData(Hand hand, bool flexsion, bool position, bool rotation)
        {
            //compose string
            var flexionString = "";
            var positionString = "";
            var rotationString = "";
            try
            {
                data.ThumbFlexsionLeft = hand.Thumb.Foldness.ToString();
                //Position
                data.ThumbPositionTipLeft = PopulateFingerPositionDataToString(hand.Thumb.JointsPosition[3]);
                data.ThumbPositionJT2Left = PopulateFingerPositionDataToString(hand.Thumb.JointsPosition[2]);
                data.ThumbPositionJT1Left = PopulateFingerPositionDataToString(hand.Thumb.JointsPosition[1]);
                data.ThumbPositionBaseLeft = PopulateFingerPositionDataToString(hand.Thumb.JointsPosition[0]);
                //Rotation
                data.ThumbRotationTipLeft = PopulateFingerRotationDataToString(hand.Thumb.JointsOrientation[3]);
                data.ThumbRotationJT2Left = PopulateFingerRotationDataToString(hand.Thumb.JointsOrientation[2]);
                data.ThumbRotationJT1Left = PopulateFingerRotationDataToString(hand.Thumb.JointsOrientation[1]);
                data.ThumbRotationBaseLeft = PopulateFingerRotationDataToString(hand.Thumb.JointsOrientation[0]);

                if (flexsion)
                {
                    //Flexion
                    flexionString = data.ThumbFlexsionLeft + ",";
                }

                if (position)
                {
                    //Position
                    positionString += data.ThumbPositionTipLeft + ",";
                    positionString += data.ThumbPositionJT2Left + ",";
                    positionString += data.ThumbPositionJT1Left + ",";
                    positionString += data.ThumbPositionBaseLeft + ",";
                }

                if (rotation)
                {
                    //Rotation
                    rotationString += data.ThumbRotationTipLeft + ",";
                    rotationString += data.ThumbRotationJT2Left + ",";
                    rotationString += data.ThumbRotationJT1Left + ",";
                    rotationString += data.ThumbRotationBaseLeft + ",";
                }
            }
            catch (Exception)
            {

            }
            ComposeDataStreamString(flexsion, position, rotation, flexionString, positionString, rotationString);
        }

        private void PopulateLeftIndexData(Hand hand, bool flexsion, bool position, bool rotation)
        {
            //compose string
            var flexionString = "";
            var positionString = "";
            var rotationString = "";
            try
            {
                data.IndexFlexsionLeft = hand.Index.Foldness.ToString();
                //Position
                data.IndexPositionTipLeft = PopulateFingerPositionDataToString(hand.Index.JointsPosition[3]);
                data.IndexPositionJT2Left = PopulateFingerPositionDataToString(hand.Index.JointsPosition[2]);
                data.IndexPositionJT1Left = PopulateFingerPositionDataToString(hand.Index.JointsPosition[1]);
                data.IndexPositionBaseLeft = PopulateFingerPositionDataToString(hand.Index.JointsPosition[0]);
                //Rotation
                data.IndexRotationTipLeft = PopulateFingerRotationDataToString(hand.Index.JointsOrientation[3]);
                data.IndexRotationJT2Left = PopulateFingerRotationDataToString(hand.Index.JointsOrientation[2]);
                data.IndexRotationJT1Left = PopulateFingerRotationDataToString(hand.Index.JointsOrientation[1]);
                data.IndexRotationBaseLeft = PopulateFingerRotationDataToString(hand.Index.JointsOrientation[0]);

                if (flexsion)
                {
                    //Flexion
                    flexionString = data.IndexFlexsionLeft + ",";
                }

                if (position)
                {
                    //Position
                    positionString += data.IndexPositionTipLeft + ",";
                    positionString += data.IndexPositionJT2Left + ",";
                    positionString += data.IndexPositionJT1Left + ",";
                    positionString += data.IndexPositionBaseLeft + ",";
                }

                if (rotation)
                {
                    //Rotation
                    rotationString += data.IndexRotationTipLeft + ",";
                    rotationString += data.IndexRotationJT2Left + ",";
                    rotationString += data.IndexRotationJT1Left + ",";
                    rotationString += data.IndexRotationBaseLeft + ",";
                }

            }
            catch (Exception)
            {

            }
            ComposeDataStreamString(flexsion, position, rotation, flexionString, positionString, rotationString);
        }

        private void PopulateLeftMiddleData(Hand hand, bool flexsion, bool position, bool rotation)
        {
            //compose string
            var flexionString = "";
            var positionString = "";
            var rotationString = "";
            try
            {
                data.MiddleFlexsionLeft = hand.Middle.Foldness.ToString();
                //Position
                data.MiddlePositionTipLeft = PopulateFingerPositionDataToString(hand.Middle.JointsPosition[3]);
                data.MiddlePositionJT2Left = PopulateFingerPositionDataToString(hand.Middle.JointsPosition[2]);
                data.MiddlePositionJT1Left = PopulateFingerPositionDataToString(hand.Middle.JointsPosition[1]);
                data.MiddlePositionBaseLeft = PopulateFingerPositionDataToString(hand.Middle.JointsPosition[0]);
                //Rotation
                data.MiddleRotationTipLeft = PopulateFingerRotationDataToString(hand.Middle.JointsOrientation[3]);
                data.MiddleRotationJT2Left = PopulateFingerRotationDataToString(hand.Middle.JointsOrientation[2]);
                data.MiddleRotationJT1Left = PopulateFingerRotationDataToString(hand.Middle.JointsOrientation[1]);
                data.MiddleRotationBaseLeft = PopulateFingerRotationDataToString(hand.Middle.JointsOrientation[0]);

                if (flexsion)
                {
                    //Flexion
                    flexionString = data.MiddleFlexsionLeft + ",";
                }

                if (position)
                {
                    //Position
                    positionString += data.MiddlePositionTipLeft + ",";
                    positionString += data.MiddlePositionJT2Left + ",";
                    positionString += data.MiddlePositionJT1Left + ",";
                    positionString += data.MiddlePositionBaseLeft + ",";
                }

                if (rotation)
                {
                    //Rotation
                    rotationString += data.MiddleRotationTipLeft + ",";
                    rotationString += data.MiddleRotationJT2Left + ",";
                    rotationString += data.MiddleRotationJT1Left + ",";
                    rotationString += data.MiddleRotationBaseLeft + ",";
                }
            }
            catch (Exception)
            {

            }
            ComposeDataStreamString(flexsion, position, rotation, flexionString, positionString, rotationString);
        }

        private void PopulateLeftRingData(Hand hand, bool flexsion, bool position, bool rotation)
        {
            //compose string
            var flexionString = "";
            var positionString = "";
            var rotationString = "";
            try
            {
                data.RingFlexsionLeft = hand.Ring.Foldness.ToString();
                //Position
                data.RingPositionTipLeft = PopulateFingerPositionDataToString(hand.Ring.JointsPosition[3]);
                data.RingPositionJT2Left = PopulateFingerPositionDataToString(hand.Ring.JointsPosition[2]);
                data.RingPositionJT1Left = PopulateFingerPositionDataToString(hand.Ring.JointsPosition[1]);
                data.RingPositionBaseLeft = PopulateFingerPositionDataToString(hand.Ring.JointsPosition[0]);
                //Rotation
                data.RingRotationTipLeft = PopulateFingerRotationDataToString(hand.Ring.JointsOrientation[3]);
                data.RingRotationJT2Left = PopulateFingerRotationDataToString(hand.Ring.JointsOrientation[2]);
                data.RingRotationJT1Left = PopulateFingerRotationDataToString(hand.Ring.JointsOrientation[1]);
                data.RingRotationBaseLeft = PopulateFingerRotationDataToString(hand.Ring.JointsOrientation[0]);

                if (flexsion)
                {
                    //Flexion
                    flexionString = data.RingFlexsionLeft + ",";
                }

                if (position)
                {
                    //Position
                    positionString += data.RingPositionTipLeft + ",";
                    positionString += data.RingPositionJT2Left + ",";
                    positionString += data.RingPositionJT1Left + ",";
                    positionString += data.RingPositionBaseLeft + ",";
                }

                if (rotation)
                {
                    //Rotation
                    rotationString += data.RingRotationTipLeft + ",";
                    rotationString += data.RingRotationJT2Left + ",";
                    rotationString += data.RingRotationJT1Left + ",";
                    rotationString += data.RingRotationBaseLeft + ",";
                }
            }
            catch (Exception)
            {

            }
            ComposeDataStreamString(flexsion, position, rotation, flexionString, positionString, rotationString);
        }

        private void PopulateLeftPingkyData(Hand hand, bool flexsion, bool position, bool rotation)
        {
            //compose string
            var flexionString = "";
            var positionString = "";
            var rotationString = "";
            try
            {
                data.PingkyFlexsionLeft = hand.Pingky.Foldness.ToString();
                //Position
                data.PingkyPositionTipLeft = PopulateFingerPositionDataToString(hand.Pingky.JointsPosition[3]);
                data.PingkyPositionJT2Left = PopulateFingerPositionDataToString(hand.Pingky.JointsPosition[2]);
                data.PingkyPositionJT1Left = PopulateFingerPositionDataToString(hand.Pingky.JointsPosition[1]);
                data.PingkyPositionBaseLeft = PopulateFingerPositionDataToString(hand.Pingky.JointsPosition[0]);
                //Rotation
                data.PingkyRotationTipLeft = PopulateFingerRotationDataToString(hand.Pingky.JointsOrientation[3]);
                data.PingkyRotationJT2Left = PopulateFingerRotationDataToString(hand.Pingky.JointsOrientation[2]);
                data.PingkyRotationJT1Left = PopulateFingerRotationDataToString(hand.Pingky.JointsOrientation[1]);
                data.PingkyRotationBaseLeft = PopulateFingerRotationDataToString(hand.Pingky.JointsOrientation[0]);

                if (flexsion)
                {
                    //Flexion
                    flexionString = data.PingkyFlexsionLeft + ",";
                }

                if (position)
                {
                    //Position
                    positionString += data.PingkyPositionTipLeft + ",";
                    positionString += data.PingkyPositionJT2Left + ",";
                    positionString += data.PingkyPositionJT1Left + ",";
                    positionString += data.PingkyPositionBaseLeft + ",";
                }

                if (rotation)
                {
                    //Rotation
                    rotationString += data.PingkyRotationTipLeft + ",";
                    rotationString += data.PingkyRotationJT2Left + ",";
                    rotationString += data.PingkyRotationJT1Left + ",";
                    rotationString += data.PingkyRotationBaseLeft + ",";
                }

            }
            catch (Exception)
            {

            }
            ComposeDataStreamString(flexsion, position, rotation, flexionString, positionString, rotationString);

        }

        private void ComposeDataStreamString(bool flexsion, bool position, bool rotation, string flexionString, string positionString, string rotationString)
        {
            if (flexsion) dataStreamString += flexionString;

            if (position) dataStreamString += positionString;

            if (rotation) dataStreamString += rotationString;
        }

        private void ShowNullHandDataOnTextbox(bool flexion, bool position, bool rotation)
        {
            string nullData = "";
            if (flexion)
                nullData += "0,";
            if (position)
                nullData += "0,0,0,0,0,0,0,0,0,0,0,0,";
            if (rotation)
                nullData += "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,";
            dataStreamString += nullData;
        }


        private string PopulateFingerPositionDataToString(RS.Point3DF32 finger)
        {
            string result = Math.Round(finger.x, 3).ToString(ci) + "," + Math.Round(finger.y, 3).ToString(ci) + "," + Math.Round(finger.z, 3).ToString(ci);
            return result;
        }

        private string PopulateFingerRotationDataToString(RS.Point4DF32 finger)
        {
            string result = Math.Round(finger.w, 3).ToString(ci) + "," + Math.Round(finger.x, 3).ToString(ci) + "," + Math.Round(finger.y, 3).ToString(ci) + "," + Math.Round(finger.z, 3).ToString(ci);
            return result;
        }
        #endregion
    }

    public class RenderFrameEventArgs : EventArgs
    {
        public int Index { get; set; }
        public RS.Image Image { get; set; }

        public RenderFrameEventArgs(int index, RS.Image image)
        {
            this.Index = index;
            this.Image = image;
        }
    }
}
