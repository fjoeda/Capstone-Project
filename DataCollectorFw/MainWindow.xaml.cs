using DataCollectorFw.ViewModel;
using Microsoft.Win32;
using RealsenseHandler;
using RealsenseHandler.RsHand;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataCollectorFw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CultureInfo ci = new CultureInfo("en-US");
        RealsenseManager rm;
        ViewModel.HandViewModel data;
        private bool flexionChecked, positionChecked, rotationChecked;
        public MainWindow()
        {
            
            InitializeComponent();
            rm = new RealsenseManager();
            rm.HandDataChanged += HandDataChanged;
            rm.Init();
            data = new ViewModel.HandViewModel();
            DataContext = data;
        }

        private void HandDataChanged(Hand rightHand, Hand leftHand)
        {
            try
            {
                if (rightHand != null && rightHand.IsTracked)
                {
                    Dispatcher.BeginInvoke(new PopulateHandData(PopulateRightThumbData), new object[] { rightHand, flexionChecked, positionChecked, rotationChecked });
                    Dispatcher.BeginInvoke(new PopulateHandData(PopulateRightIndexData), new object[] { rightHand, flexionChecked, positionChecked, rotationChecked } );
                    Dispatcher.BeginInvoke(new PopulateHandData(PopulateRightMiddleData), new object[] { rightHand, flexionChecked, positionChecked, rotationChecked});
                    Dispatcher.BeginInvoke(new PopulateHandData(PopulateRightRingData), new object[] { rightHand, flexionChecked, positionChecked, rotationChecked } );
                    Dispatcher.BeginInvoke(new PopulateHandData(PopulateRightPingkyData), new object[] { rightHand, flexionChecked, positionChecked, rotationChecked});
                }
                else 
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Dispatcher.BeginInvoke(new PopulateNullDataOnTextBox(ShowNullHandDataOnTextbox), new object[] { flexionChecked, positionChecked, rotationChecked });
                    }
                }

                if (leftHand != null && leftHand.IsTracked)
                {
                    Dispatcher.BeginInvoke(new PopulateHandData(PopulateLeftThumbData), new object[] { leftHand, flexionChecked, positionChecked, rotationChecked });
                    Dispatcher.BeginInvoke(new PopulateHandData(PopulateLeftIndexData), new object[] { leftHand, flexionChecked, positionChecked, rotationChecked });
                    Dispatcher.BeginInvoke(new PopulateHandData(PopulateLeftMiddleData), new object[] { leftHand, flexionChecked, positionChecked, rotationChecked });
                    Dispatcher.BeginInvoke(new PopulateHandData(PopulateLeftRingData), new object[] { leftHand, flexionChecked, positionChecked, rotationChecked });
                    Dispatcher.BeginInvoke(new PopulateHandData(PopulateLeftPingkyData), new object[] { leftHand, flexionChecked, positionChecked, rotationChecked });
                }
                else 
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Dispatcher.BeginInvoke(new PopulateNullDataOnTextBox(ShowNullHandDataOnTextbox),new object[] { flexionChecked, positionChecked, rotationChecked });
                    }
                }
                Dispatcher.BeginInvoke(new PopulateDataOnTextbox(AddNewLineOnTextBox));

            }
            catch (Exception e)
            {

            }
            
        }

        private delegate void PopulateHandData(Hand hand, bool flexsion, bool position, bool rotation);
        private delegate void PopulateNullDataOnTextBox(bool flexsion, bool position, bool rotation);
        private delegate void PopulateDataOnTextbox();

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

            ShowDataOnTextbox(flexsion, position, rotation, flexionString, positionString, rotationString);
            
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
            ShowDataOnTextbox(flexsion, position, rotation, flexionString, positionString, rotationString);
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
            ShowDataOnTextbox(flexsion, position, rotation, flexionString, positionString, rotationString);
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
            ShowDataOnTextbox(flexsion, position, rotation, flexionString, positionString, rotationString);
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
            ShowDataOnTextbox(flexsion, position, rotation, flexionString, positionString, rotationString);
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
            ShowDataOnTextbox(flexsion, position, rotation, flexionString, positionString, rotationString);
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
            ShowDataOnTextbox(flexsion, position, rotation, flexionString, positionString, rotationString);
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
            ShowDataOnTextbox(flexsion, position, rotation, flexionString, positionString, rotationString);
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
            ShowDataOnTextbox(flexsion, position, rotation, flexionString, positionString, rotationString);
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
            ShowDataOnTextbox(flexsion, position, rotation, flexionString, positionString, rotationString);
            
        }


        private void ShowDataOnTextbox(bool flexsion,bool position, bool rotation, string flexionString, string positionString, string rotationString)
        {
            if (flexsion) tb_StreamData.Text += flexionString;

            if (position) tb_StreamData.Text += positionString;

            if (rotation) tb_StreamData.Text += rotationString;
        }

        private void ShowNullHandDataOnTextbox(bool flexion, bool position, bool rotation)
        {
            string nullData = "";
            if(flexion)
                nullData += "0,";
            if(position)
                nullData += "0,0,0,0,0,0,0,0,0,0,0,0,";
            if(rotation)
                nullData += "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,";
            tb_StreamData.Text += nullData;
        }

        private void AddNewLineOnTextBox()
        {
            tb_StreamData.Text += tb_Label.Text+Environment.NewLine;
        }

        

        private void cb_Checked(object sender, RoutedEventArgs e)
        {
            flexionChecked = (bool)cb_Flexion.IsChecked;
            positionChecked = (bool)cb_Position.IsChecked;
            rotationChecked = (bool)cb_Rotation.IsChecked;
        }

        private void ExportToCsv(string path, string header)
        {
            var csv = new StringBuilder();
            CultureInfo ci = new CultureInfo("en-US");
            csv.AppendLine(header);
            csv.AppendLine(tb_StreamData.Text);
            File.WriteAllText(path, csv.ToString());
        }

        private string BuildHeaderForCsvExport(bool flexion, bool position, bool rotation)
        {
            string header = "";
           

            if (position)
            {
                if (flexion) header += "R_Thumb_flexion,";
                header += "R_Thumb_tip_x,R_Thumb_tip_y,R_Thumb_tip_z,R_Thumb_jt2_x,R_Thumb_jt2_y,R_Thumb_jt2_z,R_Thumb_jt1_x,R_Thumb_jt1_y,R_Thumb_jt1_z,R_Thumb_base_x,R_Thumb_base_y,R_Thumb_base_z,";
                if (flexion) header += "R_Index_flexion,";
                header += "R_Index_tip_x,R_Index_tip_y,R_Index_tip_z,R_Index_jt2_x,R_Index_jt2_y,R_Index_jt2_z,R_Index_jt1_x,R_Index_jt1_y,R_Index_jt1_z,R_Index_base_x,R_Index_base_y,R_Index_base_z,";
                if (flexion) header += "R_Middle_flexion,";
                header += "R_Middle_tip_x,R_Middle_tip_y,R_Middle_tip_z,R_Middle_jt2_x,R_Middle_jt2_y,R_Middle_jt2_z,R_Middle_jt1_x,R_Middle_jt1_y,R_Middle_jt1_z,R_Middle_base_x,R_Middle_base_y,R_Middle_base_z,";
                if (flexion) header += "R_Ring_flexion,";
                header += "R_Ring_tip_x,R_Ring_tip_y,R_Ring_tip_z,R_Ring_jt2_x,R_Ring_jt2_y,R_Ring_jt2_z,R_Ring_jt1_x,R_Ring_jt1_y,R_Ring_jt1_z,R_Ring_base_x,R_Ring_base_y,R_Ring_base_z,";
                if (flexion) header += "R_Pingky_flexion,";
                header += "R_Pingky_tip_x,R_Pingky_tip_y,R_Pingky_tip_z,R_Pingky_jt2_x,R_Pingky_jt2_y,R_Pingky_jt2_z,R_Pingky_jt1_x,R_Pingky_jt1_y,R_Pingky_jt1_z,R_Pingky_base_x,R_Pingky_base_y,R_Pingky_base_z,";

                if (flexion) header += "L_Thumb_flexion,";
                header += "L_Thumb_tip_x,L_Thumb_tip_y,L_Thumb_tip_z,L_Thumb_jt2_x,L_Thumb_jt2_y,L_Thumb_jt2_z,L_Thumb_jt1_x,L_Thumb_jt1_y,L_Thumb_jt1_z,L_Thumb_base_x,L_Thumb_base_y,L_Thumb_base_z,";
                if (flexion) header += "L_Index_flexion,";
                header += "L_Index_tip_x,L_Index_tip_y,L_Index_tip_z,L_Index_jt2_x,L_Index_jt2_y,L_Index_jt2_z,L_Index_jt1_x,L_Index_jt1_y,L_Index_jt1_z,L_Index_base_x,L_Index_base_y,L_Index_base_z,";
                if (flexion) header += "L_Middle_flexion,";
                header += "L_Middle_tip_x,L_Middle_tip_y,L_Middle_tip_z,L_Middle_jt2_x,L_Middle_jt2_y,L_Middle_jt2_z,L_Middle_jt1_x,L_Middle_jt1_y,L_Middle_jt1_z,L_Middle_base_x,L_Middle_base_y,L_Middle_base_z,";
                if (flexion) header += "L_Ring_flexion,";
                header += "L_Ring_tip_x,L_Ring_tip_y,L_Ring_tip_z,L_Ring_jt2_x,L_Ring_jt2_y,L_Ring_jt2_z,L_Ring_jt1_x,L_Ring_jt1_y,L_Ring_jt1_z,L_Ring_base_x,L_Ring_base_y,L_Ring_base_z,";
                if (flexion) header += "L_Pingky_flexion,";
                header += "L_Pingky_tip_x,L_Pingky_tip_y,L_Pingky_tip_z,L_Pingky_jt2_x,L_Pingky_jt2_y,L_Pingky_jt2_z,L_Pingky_jt1_x,L_Pingky_jt1_y,L_Pingky_jt1_z,L_Pingky_base_x,L_Pingky_base_y,L_Pingky_base_z,";
            }

            if (rotation)
            {
                if (flexion&&!position) header += "R_Thumb_flexion,";
                header += "R_Rot_Thumb_tip_w,R_Rot_Thumb_tip_x,R_Rot_Thumb_tip_y,R_Rot_Thumb_tip_z,R_Rot_Thumb_jt2_w,R_Rot_Thumb_jt2_x,R_Rot_Thumb_jt2_y,R_Rot_Thumb_jt2_z,R_Rot_Thumb_jt1_w,R_Rot_Thumb_jt1_x,R_Rot_Thumb_jt1_y,R_Rot_Thumb_jt1_z,R_Rot_Thumb_base_w,R_Rot_Thumb_base_x,R_Rot_Thumb_base_y,R_Rot_Thumb_base_z,";
                if (flexion && !position) header += "R_Index_flexion,";
                header += "R_Rot_Index_tip_w,R_Rot_Index_tip_x,R_Rot_Index_tip_y,R_Rot_Index_tip_z,R_Rot_Index_jt2_w,R_Rot_Index_jt2_x,R_Rot_Index_jt2_y,R_Rot_Index_jt2_z,R_Rot_Index_jt1_w,R_Rot_Index_jt1_x,R_Rot_Index_jt1_y,R_Rot_Index_jt1_z,R_Rot_Index_base_w,R_Rot_Index_base_x,R_Rot_Index_base_y,R_Rot_Index_base_z,";
                if (flexion && !position) header += "R_Middle_flexion,";
                header += "R_Rot_Middle_tip_w,R_Rot_Middle_tip_x,R_Rot_Middle_tip_y,R_Rot_Middle_tip_z,R_Rot_Middle_jt2_w,R_Rot_Middle_jt2_x,R_Rot_Middle_jt2_y,R_Rot_Middle_jt2_z,R_Rot_Middle_jt1_w,R_Rot_Middle_jt1_x,R_Rot_Middle_jt1_y,R_Rot_Middle_jt1_z,R_Rot_Middle_base_w,R_Rot_Middle_base_x,R_Rot_Middle_base_y,R_Rot_Middle_base_z,";
                if (flexion && !position) header += "R_Ring_flexion,";
                header += "R_Rot_Ring_tip_w,R_Rot_Ring_tip_x,R_Rot_Ring_tip_y,R_Rot_Ring_tip_z,R_Rot_Ring_jt2_w,R_Rot_Ring_jt2_x,R_Rot_Ring_jt2_y,R_Rot_Ring_jt2_z,R_Rot_Ring_jt1_w,R_Rot_Ring_jt1_x,R_Rot_Ring_jt1_y,R_Rot_Ring_jt1_z,R_Rot_Ring_base_w,R_Rot_Ring_base_x,R_Rot_Ring_base_y,R_Rot_Ring_base_z,";
                if (flexion && !position) header += "R_Pingky_flexion,";
                header += "R_Rot_Pingky_tip_w,R_Rot_Pingky_tip_x,R_Rot_Pingky_tip_y,R_Rot_Pingky_tip_z,R_Rot_Pingky_jt2_w,R_Rot_Pingky_jt2_x,R_Rot_Pingky_jt2_y,R_Rot_Pingky_jt2_z,R_Rot_Pingky_jt1_w,R_Rot_Pingky_jt1_x,R_Rot_Pingky_jt1_y,R_Rot_Pingky_jt1_z,R_Rot_Pingky_base_w,R_Rot_Pingky_base_x,R_Rot_Pingky_base_y,R_Rot_Pingky_base_z,";

                if (flexion && !position) header += "L_Thumb_flexion,";
                header += "L_Rot_Thumb_tip_w,L_Rot_Thumb_tip_x,L_Rot_Thumb_tip_y,L_Rot_Thumb_tip_z,L_Rot_Thumb_jt2_w,L_Rot_Thumb_jt2_x,L_Rot_Thumb_jt2_y,L_Rot_Thumb_jt2_z,L_Rot_Thumb_jt1_w,L_Rot_Thumb_jt1_x,L_Rot_Thumb_jt1_y,L_Rot_Thumb_jt1_z,L_Rot_Thumb_base_w,L_Rot_Thumb_base_x,L_Rot_Thumb_base_y,L_Rot_Thumb_base_z,";
                if (flexion && !position) header += "L_Index_flexion,";
                header += "L_Rot_Index_tip_w,L_Rot_Index_tip_x,L_Rot_Index_tip_y,L_Rot_Index_tip_z,L_Rot_Index_jt2_w,L_Rot_Index_jt2_x,L_Rot_Index_jt2_y,L_Rot_Index_jt2_z,L_Rot_Index_jt1_w,L_Rot_Index_jt1_x,L_Rot_Index_jt1_y,L_Rot_Index_jt1_z,L_Rot_Index_base_w,L_Rot_Index_base_x,L_Rot_Index_base_y,L_Rot_Index_base_z,";
                if (flexion && !position) header += "L_Middle_flexion,";
                header += "L_Rot_Middle_tip_w,L_Rot_Middle_tip_x,L_Rot_Middle_tip_y,L_Rot_Middle_tip_z,L_Rot_Middle_jt2_w,L_Rot_Middle_jt2_x,L_Rot_Middle_jt2_y,L_Rot_Middle_jt2_z,L_Rot_Middle_jt1_w,L_Rot_Middle_jt1_x,L_Rot_Middle_jt1_y,L_Rot_Middle_jt1_z,L_Rot_Middle_base_w,L_Rot_Middle_base_x,L_Rot_Middle_base_y,L_Rot_Middle_base_z,";
                if (flexion && !position) header += "L_Ring_flexion,";
                header += "L_Rot_Ring_tip_w,L_Rot_Ring_tip_x,L_Rot_Ring_tip_y,L_Rot_Ring_tip_z,L_Rot_Ring_jt2_w,L_Rot_Ring_jt2_x,L_Rot_Ring_jt2_y,L_Rot_Ring_jt2_z,L_Rot_Ring_jt1_w,L_Rot_Ring_jt1_x,L_Rot_Ring_jt1_y,L_Rot_Ring_jt1_z,L_Rot_Ring_base_w,L_Rot_Ring_base_x,L_Rot_Ring_base_y,L_Rot_Ring_base_z,";
                if (flexion && !position) header += "L_Pingky_flexion,";
                header += "L_Rot_Pingky_tip_w,L_Rot_Pingky_tip_x,L_Rot_Pingky_tip_y,L_Rot_Pingky_tip_z,L_Rot_Pingky_jt2_w,L_Rot_Pingky_jt2_x,L_Rot_Pingky_jt2_y,L_Rot_Pingky_jt2_z,L_Rot_Pingky_jt1_w,L_Rot_Pingky_jt1_x,L_Rot_Pingky_jt1_y,L_Rot_Pingky_jt1_z,L_Rot_Pingky_base_w,L_Rot_Pingky_base_x,L_Rot_Pingky_base_y,L_Rot_Pingky_base_z,";

            }

            header += "label";

            return header;
        }

        private void btn_ExportData_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "CSV File|*.csv";
            saveFileDialog1.Title = "Save an Csv File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                ExportToCsv(saveFileDialog1.FileName,BuildHeaderForCsvExport(flexionChecked,positionChecked,rotationChecked));
            }
        }

        private string PopulateFingerPositionDataToString(PXCMPoint3DF32 finger)
        {
            string result = Math.Round(finger.x, 3).ToString(ci) + "," + Math.Round(finger.y, 3).ToString(ci) + "," + Math.Round(finger.z, 3).ToString(ci);
            return result;
        }

        private string PopulateFingerRotationDataToString(PXCMPoint4DF32 finger)
        {
            string result = Math.Round(finger.w, 3).ToString(ci) + "," + Math.Round(finger.x, 3).ToString(ci) + "," + Math.Round(finger.y, 3).ToString(ci) + "," + Math.Round(finger.z, 3).ToString(ci);
            return result;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            rm.Dispose();
        }
    }
}
