using RealsenseHandler.RsHand;
using System;
using System.Collections.Generic;
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
using RealsenseHandler;
using Intel.RealSense;

namespace DataCollector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RealsenseManager rm;

        #region Right Thumb Properties
        public string ThumbFlexsionRight { get; set; }
        public string ThumbPositionTipRight { get; set; }
        public string ThumbPositionJT2Right { get; set; }
        public string ThumbPositionJT1Right { get; set; }
        public string ThumbPositionBaseRight { get; set; }
        public string ThumbRotationTipRight { get; set; }
        public string ThumbRotationJT2Right { get; set; }
        public string ThumbRotationJT1Right { get; set; }
        public string ThumbRotationBaseRight { get; set; }

        #endregion



        public MainWindow()
        {
            rm = new RealsenseManager();           
            InitializeComponent();
            rm.Init();
            rm.handDataChanged += HandDataChanged;
            DataContext = this;
        }

        private void HandDataChanged(Hand rightHand, Hand leftHand)
        {
            if (rightHand != null)
            {
                PopulateRightThumbData(rightHand);
            }
        }

        public void PopulateRightThumbData(Hand hand)
        {
            ThumbFlexsionRight = hand.Thumb.Foldness.ToString();
            //Position
            ThumbPositionTipRight = PopulateFingerPositionDataToString(hand.Thumb.JointsPosition[3]);
            ThumbPositionJT2Right = PopulateFingerPositionDataToString(hand.Thumb.JointsPosition[2]);
            ThumbPositionJT1Right = PopulateFingerPositionDataToString(hand.Thumb.JointsPosition[1]);
            ThumbPositionBaseRight = PopulateFingerPositionDataToString(hand.Thumb.JointsPosition[0]);
            //Rotation
            ThumbRotationTipRight = PopulateFingerRotationDataToString(hand.Thumb.JointsOrientation[3]);
            ThumbRotationJT2Right = PopulateFingerRotationDataToString(hand.Thumb.JointsOrientation[2]);
            ThumbRotationJT1Right = PopulateFingerRotationDataToString(hand.Thumb.JointsOrientation[1]);
            ThumbRotationBaseRight = PopulateFingerRotationDataToString(hand.Thumb.JointsOrientation[0]);
        }

        private string PopulateFingerPositionDataToString(Point3DF32 finger)
        {
            string result = finger.x.ToString() + ", " + finger.y.ToString() + ", " + finger.z.ToString();
            return result;
        }

        private string PopulateFingerRotationDataToString(Point4DF32 finger)
        {
            string result = finger.w.ToString()+", "+ finger.x.ToString() + ", " + finger.y.ToString() + ", " + finger.z.ToString();
            return result;
        }
    }
}
