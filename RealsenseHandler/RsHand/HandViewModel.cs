using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealsenseHandler.RsHand
{
    public class HandViewModel
    {

        #region Right Thumb Properties
        public string ThumbFlexsionRight
        {
            get
            {
                return _thumbFlexsionRight;
            }
            set
            {
                _thumbFlexsionRight = value;
                NotifyPropertyChanged("ThumbFlexsionRight");
            }
        }
        private string _thumbFlexsionRight = "0";
        public string ThumbPositionTipRight
        {
            get
            {
                return _thumbPositionTipRight;
            }
            set
            {
                _thumbPositionTipRight = value;
                NotifyPropertyChanged("ThumbPositionTipRight");
            }
        }
        private string _thumbPositionTipRight = "0";
        public string ThumbPositionJT2Right
        {
            get
            {
                return _thumbPositionJT2Right;
            }
            set
            {
                _thumbPositionJT2Right = value;
                NotifyPropertyChanged("ThumbPositionJT2Right");
            }
        }
        private string _thumbPositionJT2Right = "0";
        public string ThumbPositionJT1Right
        {
            get
            {
                return _thumbPositionJT1Right;
            }
            set
            {
                _thumbPositionJT1Right = value;
                NotifyPropertyChanged("ThumbPositionJT1Right");
            }
        }
        private string _thumbPositionJT1Right = "0";
        public string ThumbPositionBaseRight
        {
            get
            {
                return _thumbPositionBaseRight;
            }
            set
            {
                _thumbPositionBaseRight = value;
                NotifyPropertyChanged("ThumbPositionBaseRight");
            }
        }
        private string _thumbPositionBaseRight = "0";

        public string ThumbPositionImageTipRight
        {
            get
            {
                return _thumbPositionImageTipRight;
            }
            set
            {
                _thumbPositionImageTipRight = value;
                NotifyPropertyChanged("ThumbPositionImageTipRight");
            }
        }
        private string _thumbPositionImageTipRight = "0";
        public string ThumbPositionImageJT2Right
        {
            get
            {
                return _thumbPositionImageJT2Right;
            }
            set
            {
                _thumbPositionImageJT2Right = value;
                NotifyPropertyChanged("ThumbPositionImageJT2Right");
            }
        }
        private string _thumbPositionImageJT2Right = "0";
        public string ThumbPositionImageJT1Right
        {
            get
            {
                return _thumbPositionImageJT1Right;
            }
            set
            {
                _thumbPositionImageJT1Right = value;
                NotifyPropertyChanged("ThumbPositionImageJT1Right");
            }
        }
        private string _thumbPositionImageJT1Right = "0";
        public string ThumbPositionImageBaseRight
        {
            get
            {
                return _thumbPositionImageBaseRight;
            }
            set
            {
                _thumbPositionImageBaseRight = value;
                NotifyPropertyChanged("ThumbPositionImageBaseRight");
            }
        }
        private string _thumbPositionImageBaseRight = "0";

        public string ThumbRotationTipRight
        {
            get
            {
                return _thumbRotationTipRight;
            }
            set
            {
                _thumbRotationTipRight = value;
                NotifyPropertyChanged("ThumbRotationTipRight");
            }
        }
        private string _thumbRotationTipRight = "0";
        public string ThumbRotationJT2Right
        {
            get
            {
                return _thumbRotationJT2Right;
            }
            set
            {
                _thumbRotationJT2Right = value;
                NotifyPropertyChanged("ThumbRotationJT2Right");
            }
        }
        private string _thumbRotationJT2Right = "0";
        public string ThumbRotationJT1Right
        {
            get
            {
                return _thumbRotationJT1Right;
            }
            set
            {
                _thumbRotationJT1Right = value;
                NotifyPropertyChanged("ThumbRotationJT1Right");
            }
        }
        private string _thumbRotationJT1Right = "0";
        public string ThumbRotationBaseRight
        {
            get
            {
                return _thumbRotationBaseRight;
            }
            set
            {
                _thumbRotationBaseRight = value;
                NotifyPropertyChanged("ThumbRotationBaseRight");
            }
        }
        private string _thumbRotationBaseRight = "0";
        #endregion

        #region Right Index Properties
        public string IndexFlexsionRight
        {
            get
            {
                return _indexFlexsionRight;
            }
            set
            {
                _indexFlexsionRight = value;
                NotifyPropertyChanged("IndexFlexsionRight");
            }
        }
        private string _indexFlexsionRight = "0";
        public string IndexPositionTipRight
        {
            get
            {
                return _indexPositionTipRight;
            }
            set
            {
                _indexPositionTipRight = value;
                NotifyPropertyChanged("IndexPositionTipRight");
            }
        }
        private string _indexPositionTipRight = "0";
        public string IndexPositionJT2Right
        {
            get
            {
                return _indexPositionJT2Right;
            }
            set
            {
                _indexPositionJT2Right = value;
                NotifyPropertyChanged("IndexPositionJT2Right");
            }
        }
        private string _indexPositionJT2Right = "0";
        public string IndexPositionJT1Right
        {
            get
            {
                return _indexPositionJT1Right;
            }
            set
            {
                _indexPositionJT1Right = value;
                NotifyPropertyChanged("IndexPositionJT1Right");
            }
        }
        private string _indexPositionJT1Right = "0";
        public string IndexPositionBaseRight
        {
            get
            {
                return _indexPositionBaseRight;
            }
            set
            {
                _indexPositionBaseRight = value;
                NotifyPropertyChanged("IndexPositionBaseRight");
            }
        }
        private string _indexPositionBaseRight = "0";

        public string IndexPositionImageTipRight
        {
            get
            {
                return _indexPositionImageTipRight;
            }
            set
            {
                _indexPositionImageTipRight = value;
                NotifyPropertyChanged("IndexPositionImageTipRight");
            }
        }
        private string _indexPositionImageTipRight = "0";
        public string IndexPositionImageJT2Right
        {
            get
            {
                return _indexPositionImageJT2Right;
            }
            set
            {
                _indexPositionImageJT2Right = value;
                NotifyPropertyChanged("IndexPositionImageJT2Right");
            }
        }
        private string _indexPositionImageJT2Right = "0";
        public string IndexPositionImageJT1Right
        {
            get
            {
                return _indexPositionImageJT1Right;
            }
            set
            {
                _indexPositionImageJT1Right = value;
                NotifyPropertyChanged("IndexPositionImageJT1Right");
            }
        }
        private string _indexPositionImageJT1Right = "0";
        public string IndexPositionImageBaseRight
        {
            get
            {
                return _indexPositionImageBaseRight;
            }
            set
            {
                _indexPositionImageBaseRight = value;
                NotifyPropertyChanged("IndexPositionImageBaseRight");
            }
        }
        private string _indexPositionImageBaseRight = "0";

        public string IndexRotationTipRight
        {
            get
            {
                return _indexRotationTipRight;
            }
            set
            {
                _indexRotationTipRight = value;
                NotifyPropertyChanged("IndexRotationTipRight");
            }
        }
        private string _indexRotationTipRight = "0";
        public string IndexRotationJT2Right
        {
            get
            {
                return _indexRotationJT2Right;
            }
            set
            {
                _indexRotationJT2Right = value;
                NotifyPropertyChanged("IndexRotationJT2Right");
            }
        }
        private string _indexRotationJT2Right = "0";
        public string IndexRotationJT1Right
        {
            get
            {
                return _indexRotationJT1Right;
            }
            set
            {
                _indexRotationJT1Right = value;
                NotifyPropertyChanged("IndexRotationJT1Right");
            }
        }
        private string _indexRotationJT1Right = "0";
        public string IndexRotationBaseRight
        {
            get
            {
                return _indexRotationBaseRight;
            }
            set
            {
                _indexRotationBaseRight = value;
                NotifyPropertyChanged("IndexRotationBaseRight");
            }
        }
        private string _indexRotationBaseRight = "0";
        #endregion

        #region Right Middle Properties
        public string MiddleFlexsionRight
        {
            get
            {
                return _middleFlexsionRight;
            }
            set
            {
                _middleFlexsionRight = value;
                NotifyPropertyChanged("MiddleFlexsionRight");
            }
        }
        private string _middleFlexsionRight = "0";
        public string MiddlePositionTipRight
        {
            get
            {
                return _middlePositionTipRight;
            }
            set
            {
                _middlePositionTipRight = value;
                NotifyPropertyChanged("MiddlePositionTipRight");
            }
        }
        private string _middlePositionTipRight = "0";
        public string MiddlePositionJT2Right
        {
            get
            {
                return _middlePositionJT2Right;
            }
            set
            {
                _middlePositionJT2Right = value;
                NotifyPropertyChanged("MiddlePositionJT2Right");
            }
        }
        private string _middlePositionJT2Right = "0";
        public string MiddlePositionJT1Right
        {
            get
            {
                return _middlePositionJT1Right;
            }
            set
            {
                _middlePositionJT1Right = value;
                NotifyPropertyChanged("MiddlePositionJT1Right");
            }
        }
        private string _middlePositionJT1Right = "0";
        public string MiddlePositionBaseRight
        {
            get
            {
                return _middlePositionBaseRight;
            }
            set
            {
                _middlePositionBaseRight = value;
                NotifyPropertyChanged("MiddlePositionBaseRight");
            }
        }
        private string _middlePositionBaseRight = "0";

        public string MiddlePositionImageTipRight
        {
            get
            {
                return _middlePositionImageTipRight;
            }
            set
            {
                _middlePositionImageTipRight = value;
                NotifyPropertyChanged("MiddlePositionImageTipRight");
            }
        }
        private string _middlePositionImageTipRight = "0";
        public string MiddlePositionImageJT2Right
        {
            get
            {
                return _middlePositionImageJT2Right;
            }
            set
            {
                _middlePositionImageJT2Right = value;
                NotifyPropertyChanged("MiddlePositionImageJT2Right");
            }
        }
        private string _middlePositionImageJT2Right = "0";
        public string MiddlePositionImageJT1Right
        {
            get
            {
                return _middlePositionImageJT1Right;
            }
            set
            {
                _middlePositionImageJT1Right = value;
                NotifyPropertyChanged("MiddlePositionImageJT1Right");
            }
        }
        private string _middlePositionImageJT1Right = "0";
        public string MiddlePositionImageBaseRight
        {
            get
            {
                return _middlePositionImageBaseRight;
            }
            set
            {
                _middlePositionImageBaseRight = value;
                NotifyPropertyChanged("MiddlePositionImageBaseRight");
            }
        }
        private string _middlePositionImageBaseRight = "0";
        public string MiddleRotationTipRight
        {
            get
            {
                return _middleRotationTipRight;
            }
            set
            {
                _middleRotationTipRight = value;
                NotifyPropertyChanged("MiddleRotationTipRight");
            }
        }
        private string _middleRotationTipRight = "0";
        public string MiddleRotationJT2Right
        {
            get
            {
                return _middleRotationJT2Right;
            }
            set
            {
                _middleRotationJT2Right = value;
                NotifyPropertyChanged("MiddleRotationJT2Right");
            }
        }
        private string _middleRotationJT2Right = "0";
        public string MiddleRotationJT1Right
        {
            get
            {
                return _middleRotationJT1Right;
            }
            set
            {
                _middleRotationJT1Right = value;
                NotifyPropertyChanged("MiddleRotationJT1Right");
            }
        }
        private string _middleRotationJT1Right = "0";
        public string MiddleRotationBaseRight
        {
            get
            {
                return _middleRotationBaseRight;
            }
            set
            {
                _middleRotationBaseRight = value;
                NotifyPropertyChanged("MiddleRotationBaseRight");
            }
        }
        private string _middleRotationBaseRight = "0";
        #endregion

        #region Right Ring Properties
        public string RingFlexsionRight
        {
            get
            {
                return _ringFlexsionRight;
            }
            set
            {
                _ringFlexsionRight = value;
                NotifyPropertyChanged("RingFlexsionRight");
            }
        }
        private string _ringFlexsionRight = "0";
        public string RingPositionTipRight
        {
            get
            {
                return _ringPositionTipRight;
            }
            set
            {
                _ringPositionTipRight = value;
                NotifyPropertyChanged("RingPositionTipRight");
            }
        }
        private string _ringPositionTipRight = "0";
        public string RingPositionJT2Right
        {
            get
            {
                return _ringPositionJT2Right;
            }
            set
            {
                _ringPositionJT2Right = value;
                NotifyPropertyChanged("RingPositionJT2Right");
            }
        }
        private string _ringPositionJT2Right = "0";
        public string RingPositionJT1Right
        {
            get
            {
                return _ringPositionJT1Right;
            }
            set
            {
                _ringPositionJT1Right = value;
                NotifyPropertyChanged("RingPositionJT1Right");
            }
        }
        private string _ringPositionJT1Right = "0";
        public string RingPositionBaseRight
        {
            get
            {
                return _ringPositionBaseRight;
            }
            set
            {
                _ringPositionBaseRight = value;
                NotifyPropertyChanged("RingPositionBaseRight");
            }
        }
        private string _ringPositionBaseRight = "0";

        public string RingPositionImageTipRight
        {
            get
            {
                return _ringPositionImageTipRight;
            }
            set
            {
                _ringPositionImageTipRight = value;
                NotifyPropertyChanged("RingPositionImageTipRight");
            }
        }
        private string _ringPositionImageTipRight = "0";
        public string RingPositionImageJT2Right
        {
            get
            {
                return _ringPositionImageJT2Right;
            }
            set
            {
                _ringPositionImageJT2Right = value;
                NotifyPropertyChanged("RingPositionImageJT2Right");
            }
        }
        private string _ringPositionImageJT2Right = "0";
        public string RingPositionImageJT1Right
        {
            get
            {
                return _ringPositionImageJT1Right;
            }
            set
            {
                _ringPositionImageJT1Right = value;
                NotifyPropertyChanged("RingPositionImageJT1Right");
            }
        }
        private string _ringPositionImageJT1Right = "0";
        public string RingPositionImageBaseRight
        {
            get
            {
                return _ringPositionImageBaseRight;
            }
            set
            {
                _ringPositionImageBaseRight = value;
                NotifyPropertyChanged("RingPositionImageBaseRight");
            }
        }
        private string _ringPositionImageBaseRight = "0";

        public string RingRotationTipRight
        {
            get
            {
                return _ringRotationTipRight;
            }
            set
            {
                _ringRotationTipRight = value;
                NotifyPropertyChanged("RingRotationTipRight");
            }
        }
        private string _ringRotationTipRight = "0";
        public string RingRotationJT2Right
        {
            get
            {
                return _ringRotationJT2Right;
            }
            set
            {
                _ringRotationJT2Right = value;
                NotifyPropertyChanged("RingRotationJT2Right");
            }
        }
        private string _ringRotationJT2Right = "0";
        public string RingRotationJT1Right
        {
            get
            {
                return _ringRotationJT1Right;
            }
            set
            {
                _ringRotationJT1Right = value;
                NotifyPropertyChanged("RingRotationJT1Right");
            }
        }
        private string _ringRotationJT1Right = "0";
        public string RingRotationBaseRight
        {
            get
            {
                return _ringRotationBaseRight;
            }
            set
            {
                _ringRotationBaseRight = value;
                NotifyPropertyChanged("RingRotationBaseRight");
            }
        }
        private string _ringRotationBaseRight = "0";
        #endregion

        #region Right Pingky Properties
        public string PingkyFlexsionRight
        {
            get
            {
                return _pingkyFlexsionRight;
            }
            set
            {
                _pingkyFlexsionRight = value;
                NotifyPropertyChanged("PingkyFlexsionRight");
            }
        }
        private string _pingkyFlexsionRight = "0";
        public string PingkyPositionTipRight
        {
            get
            {
                return _pingkyPositionTipRight;
            }
            set
            {
                _pingkyPositionTipRight = value;
                NotifyPropertyChanged("PingkyPositionTipRight");
            }
        }
        private string _pingkyPositionTipRight = "0";
        public string PingkyPositionJT2Right
        {
            get
            {
                return _pingkyPositionJT2Right;
            }
            set
            {
                _pingkyPositionJT2Right = value;
                NotifyPropertyChanged("PingkyPositionJT2Right");
            }
        }
        private string _pingkyPositionJT2Right = "0";
        public string PingkyPositionJT1Right
        {
            get
            {
                return _pingkyPositionJT1Right;
            }
            set
            {
                _pingkyPositionJT1Right = value;
                NotifyPropertyChanged("PingkyPositionJT1Right");
            }
        }
        private string _pingkyPositionJT1Right = "0";
        public string PingkyPositionBaseRight
        {
            get
            {
                return _pingkyPositionBaseRight;
            }
            set
            {
                _pingkyPositionBaseRight = value;
                NotifyPropertyChanged("PingkyPositionBaseRight");
            }
        }
        private string _pingkyPositionBaseRight = "0";

        public string PingkyPositionImageTipRight
        {
            get
            {
                return _pingkyPositionImageTipRight;
            }
            set
            {
                _pingkyPositionImageTipRight = value;
                NotifyPropertyChanged("PingkyPositionImageTipRight");
            }
        }
        private string _pingkyPositionImageTipRight = "0";
        public string PingkyPositionImageJT2Right
        {
            get
            {
                return _pingkyPositionImageJT2Right;
            }
            set
            {
                _pingkyPositionImageJT2Right = value;
                NotifyPropertyChanged("PingkyPositionImageJT2Right");
            }
        }
        private string _pingkyPositionImageJT2Right = "0";
        public string PingkyPositionImageJT1Right
        {
            get
            {
                return _pingkyPositionImageJT1Right;
            }
            set
            {
                _pingkyPositionImageJT1Right = value;
                NotifyPropertyChanged("PingkyPositionImageJT1Right");
            }
        }
        private string _pingkyPositionImageJT1Right = "0";
        public string PingkyPositionImageBaseRight
        {
            get
            {
                return _pingkyPositionImageBaseRight;
            }
            set
            {
                _pingkyPositionImageBaseRight = value;
                NotifyPropertyChanged("PingkyPositionImageBaseRight");
            }
        }
        private string _pingkyPositionImageBaseRight = "0";
        public string PingkyRotationTipRight
        {
            get
            {
                return _pingkyRotationTipRight;
            }
            set
            {
                _pingkyRotationTipRight = value;
                NotifyPropertyChanged("PingkyRotationTipRight");
            }
        }
        private string _pingkyRotationTipRight = "0";
        public string PingkyRotationJT2Right
        {
            get
            {
                return _pingkyRotationJT2Right;
            }
            set
            {
                _pingkyRotationJT2Right = value;
                NotifyPropertyChanged("PingkyRotationJT2Right");
            }
        }
        private string _pingkyRotationJT2Right = "0";
        public string PingkyRotationJT1Right
        {
            get
            {
                return _pingkyRotationJT1Right;
            }
            set
            {
                _pingkyRotationJT1Right = value;
                NotifyPropertyChanged("PingkyRotationJT1Right");
            }
        }
        private string _pingkyRotationJT1Right = "0";
        public string PingkyRotationBaseRight
        {
            get
            {
                return _pingkyRotationBaseRight;
            }
            set
            {
                _pingkyRotationBaseRight = value;
                NotifyPropertyChanged("PingkyRotationBaseRight");
            }
        }
        private string _pingkyRotationBaseRight = "0";
        #endregion


        #region Left Thumb Properties
        public string ThumbFlexsionLeft
        {
            get
            {
                return _thumbFlexsionLeft;
            }
            set
            {
                _thumbFlexsionLeft = value;
                NotifyPropertyChanged("ThumbFlexsionLeft");
            }
        }
        private string _thumbFlexsionLeft = "0";
        public string ThumbPositionTipLeft
        {
            get
            {
                return _thumbPositionTipLeft;
            }
            set
            {
                _thumbPositionTipLeft = value;
                NotifyPropertyChanged("ThumbPositionTipLeft");
            }
        }
        private string _thumbPositionTipLeft = "0";
        public string ThumbPositionJT2Left
        {
            get
            {
                return _thumbPositionJT2Left;
            }
            set
            {
                _thumbPositionJT2Left = value;
                NotifyPropertyChanged("ThumbPositionJT2Left");
            }
        }
        private string _thumbPositionJT2Left = "0";
        public string ThumbPositionJT1Left
        {
            get
            {
                return _thumbPositionJT1Left;
            }
            set
            {
                _thumbPositionJT1Left = value;
                NotifyPropertyChanged("ThumbPositionJT1Left");
            }
        }
        private string _thumbPositionJT1Left = "0";
        public string ThumbPositionBaseLeft
        {
            get
            {
                return _thumbPositionBaseLeft;
            }
            set
            {
                _thumbPositionBaseLeft = value;
                NotifyPropertyChanged("ThumbPositionBaseLeft");
            }
        }
        private string _thumbPositionBaseLeft = "0";

        public string ThumbPositionImageTipLeft
        {
            get
            {
                return _thumbPositionImageTipLeft;
            }
            set
            {
                _thumbPositionImageTipLeft = value;
                NotifyPropertyChanged("ThumbPositionImageTipLeft");
            }
        }
        private string _thumbPositionImageTipLeft = "0";
        public string ThumbPositionImageJT2Left
        {
            get
            {
                return _thumbPositionImageJT2Left;
            }
            set
            {
                _thumbPositionImageJT2Left = value;
                NotifyPropertyChanged("ThumbPositionImageJT2Left");
            }
        }
        private string _thumbPositionImageJT2Left = "0";
        public string ThumbPositionImageJT1Left
        {
            get
            {
                return _thumbPositionImageJT1Left;
            }
            set
            {
                _thumbPositionImageJT1Left = value;
                NotifyPropertyChanged("ThumbPositionImageJT1Left");
            }
        }
        private string _thumbPositionImageJT1Left = "0";
        public string ThumbPositionImageBaseLeft
        {
            get
            {
                return _thumbPositionImageBaseLeft;
            }
            set
            {
                _thumbPositionImageBaseLeft = value;
                NotifyPropertyChanged("ThumbPositionImageBaseLeft");
            }
        }
        private string _thumbPositionImageBaseLeft = "0";

        public string ThumbRotationTipLeft
        {
            get
            {
                return _thumbRotationTipLeft;
            }
            set
            {
                _thumbRotationTipLeft = value;
                NotifyPropertyChanged("ThumbRotationTipLeft");
            }
        }
        private string _thumbRotationTipLeft = "0";
        public string ThumbRotationJT2Left
        {
            get
            {
                return _thumbRotationJT2Left;
            }
            set
            {
                _thumbRotationJT2Left = value;
                NotifyPropertyChanged("ThumbRotationJT2Left");
            }
        }
        private string _thumbRotationJT2Left = "0";
        public string ThumbRotationJT1Left
        {
            get
            {
                return _thumbRotationJT1Left;
            }
            set
            {
                _thumbRotationJT1Left = value;
                NotifyPropertyChanged("ThumbRotationJT1Left");
            }
        }
        private string _thumbRotationJT1Left = "0";
        public string ThumbRotationBaseLeft
        {
            get
            {
                return _thumbRotationBaseLeft;
            }
            set
            {
                _thumbRotationBaseLeft = value;
                NotifyPropertyChanged("ThumbRotationBaseLeft");
            }
        }
        private string _thumbRotationBaseLeft = "0";
        #endregion

        #region Left Index Properties
        public string IndexFlexsionLeft
        {
            get
            {
                return _indexFlexsionLeft;
            }
            set
            {
                _indexFlexsionLeft = value;
                NotifyPropertyChanged("IndexFlexsionLeft");
            }
        }
        private string _indexFlexsionLeft = "0";
        public string IndexPositionTipLeft
        {
            get
            {
                return _indexPositionTipLeft;
            }
            set
            {
                _indexPositionTipLeft = value;
                NotifyPropertyChanged("IndexPositionTipLeft");
            }
        }
        private string _indexPositionTipLeft = "0";
        public string IndexPositionJT2Left
        {
            get
            {
                return _indexPositionJT2Left;
            }
            set
            {
                _indexPositionJT2Left = value;
                NotifyPropertyChanged("IndexPositionJT2Left");
            }
        }
        private string _indexPositionJT2Left = "0";
        public string IndexPositionJT1Left
        {
            get
            {
                return _indexPositionJT1Left;
            }
            set
            {
                _indexPositionJT1Left = value;
                NotifyPropertyChanged("IndexPositionJT1Left");
            }
        }
        private string _indexPositionJT1Left = "0";
        public string IndexPositionBaseLeft
        {
            get
            {
                return _indexPositionBaseLeft;
            }
            set
            {
                _indexPositionBaseLeft = value;
                NotifyPropertyChanged("IndexPositionBaseLeft");
            }
        }
        private string _indexPositionBaseLeft = "0";

        public string IndexPositionImageTipLeft
        {
            get
            {
                return _indexPositionImageTipLeft;
            }
            set
            {
                _indexPositionImageTipLeft = value;
                NotifyPropertyChanged("IndexPositionImageTipLeft");
            }
        }
        private string _indexPositionImageTipLeft = "0";
        public string IndexPositionImageJT2Left
        {
            get
            {
                return _indexPositionImageJT2Left;
            }
            set
            {
                _indexPositionImageJT2Left = value;
                NotifyPropertyChanged("IndexPositionImageJT2Left");
            }
        }
        private string _indexPositionImageJT2Left = "0";
        public string IndexPositionImageJT1Left
        {
            get
            {
                return _indexPositionImageJT1Left;
            }
            set
            {
                _indexPositionImageJT1Left = value;
                NotifyPropertyChanged("IndexPositionImageJT1Left");
            }
        }
        private string _indexPositionImageJT1Left = "0";
        public string IndexPositionImageBaseLeft
        {
            get
            {
                return _indexPositionImageBaseLeft;
            }
            set
            {
                _indexPositionImageBaseLeft = value;
                NotifyPropertyChanged("IndexPositionImageBaseLeft");
            }
        }
        private string _indexPositionImageBaseLeft = "0";

        public string IndexRotationTipLeft
        {
            get
            {
                return _indexRotationTipLeft;
            }
            set
            {
                _indexRotationTipLeft = value;
                NotifyPropertyChanged("IndexRotationTipLeft");
            }
        }
        private string _indexRotationTipLeft = "0";
        public string IndexRotationJT2Left
        {
            get
            {
                return _indexRotationJT2Left;
            }
            set
            {
                _indexRotationJT2Left = value;
                NotifyPropertyChanged("IndexRotationJT2Left");
            }
        }
        private string _indexRotationJT2Left = "0";
        public string IndexRotationJT1Left
        {
            get
            {
                return _indexRotationJT1Left;
            }
            set
            {
                _indexRotationJT1Left = value;
                NotifyPropertyChanged("IndexRotationJT1Left");
            }
        }
        private string _indexRotationJT1Left = "0";
        public string IndexRotationBaseLeft
        {
            get
            {
                return _indexRotationBaseLeft;
            }
            set
            {
                _indexRotationBaseLeft = value;
                NotifyPropertyChanged("IndexRotationBaseLeft");
            }
        }
        private string _indexRotationBaseLeft = "0";
        #endregion

        #region Left Middle Properties
        public string MiddleFlexsionLeft
        {
            get
            {
                return _middleFlexsionLeft;
            }
            set
            {
                _middleFlexsionLeft = value;
                NotifyPropertyChanged("MiddleFlexsionLeft");
            }
        }
        private string _middleFlexsionLeft = "0";
        public string MiddlePositionTipLeft
        {
            get
            {
                return _middlePositionTipLeft;
            }
            set
            {
                _middlePositionTipLeft = value;
                NotifyPropertyChanged("MiddlePositionTipLeft");
            }
        }
        private string _middlePositionTipLeft = "0";
        public string MiddlePositionJT2Left
        {
            get
            {
                return _middlePositionJT2Left;
            }
            set
            {
                _middlePositionJT2Left = value;
                NotifyPropertyChanged("MiddlePositionJT2Left");
            }
        }
        private string _middlePositionJT2Left = "0";
        public string MiddlePositionJT1Left
        {
            get
            {
                return _middlePositionJT1Left;
            }
            set
            {
                _middlePositionJT1Left = value;
                NotifyPropertyChanged("MiddlePositionJT1Left");
            }
        }
        private string _middlePositionJT1Left = "0";
        public string MiddlePositionBaseLeft
        {
            get
            {
                return _middlePositionBaseLeft;
            }
            set
            {
                _middlePositionBaseLeft = value;
                NotifyPropertyChanged("MiddlePositionBaseLeft");
            }
        }
        private string _middlePositionBaseLeft = "0";

        public string MiddlePositionImageTipLeft
        {
            get
            {
                return _middlePositionImageTipLeft;
            }
            set
            {
                _middlePositionImageTipLeft = value;
                NotifyPropertyChanged("MiddlePositionImageTipLeft");
            }
        }
        private string _middlePositionImageTipLeft = "0";
        public string MiddlePositionImageJT2Left
        {
            get
            {
                return _middlePositionImageJT2Left;
            }
            set
            {
                _middlePositionImageJT2Left = value;
                NotifyPropertyChanged("MiddlePositionImageJT2Left");
            }
        }
        private string _middlePositionImageJT2Left = "0";
        public string MiddlePositionImageJT1Left
        {
            get
            {
                return _middlePositionImageJT1Left;
            }
            set
            {
                _middlePositionImageJT1Left = value;
                NotifyPropertyChanged("MiddlePositionImageJT1Left");
            }
        }
        private string _middlePositionImageJT1Left = "0";
        public string MiddlePositionImageBaseLeft
        {
            get
            {
                return _middlePositionImageBaseLeft;
            }
            set
            {
                _middlePositionImageBaseLeft = value;
                NotifyPropertyChanged("MiddlePositionImageBaseLeft");
            }
        }
        private string _middlePositionImageBaseLeft = "0";

        public string MiddleRotationTipLeft
        {
            get
            {
                return _middleRotationTipLeft;
            }
            set
            {
                _middleRotationTipLeft = value;
                NotifyPropertyChanged("MiddleRotationTipLeft");
            }
        }
        private string _middleRotationTipLeft = "0";
        public string MiddleRotationJT2Left
        {
            get
            {
                return _middleRotationJT2Left;
            }
            set
            {
                _middleRotationJT2Left = value;
                NotifyPropertyChanged("MiddleRotationJT2Left");
            }
        }
        private string _middleRotationJT2Left = "0";
        public string MiddleRotationJT1Left
        {
            get
            {
                return _middleRotationJT1Left;
            }
            set
            {
                _middleRotationJT1Left = value;
                NotifyPropertyChanged("MiddleRotationJT1Left");
            }
        }
        private string _middleRotationJT1Left = "0";
        public string MiddleRotationBaseLeft
        {
            get
            {
                return _middleRotationBaseLeft;
            }
            set
            {
                _middleRotationBaseLeft = value;
                NotifyPropertyChanged("MiddleRotationBaseLeft");
            }
        }
        private string _middleRotationBaseLeft = "0";
        #endregion

        #region Left Ring Properties
        public string RingFlexsionLeft
        {
            get
            {
                return _ringFlexsionLeft;
            }
            set
            {
                _ringFlexsionLeft = value;
                NotifyPropertyChanged("RingFlexsionLeft");
            }
        }
        private string _ringFlexsionLeft = "0";
        public string RingPositionTipLeft
        {
            get
            {
                return _ringPositionTipLeft;
            }
            set
            {
                _ringPositionTipLeft = value;
                NotifyPropertyChanged("RingPositionTipLeft");
            }
        }
        private string _ringPositionTipLeft = "0";
        public string RingPositionJT2Left
        {
            get
            {
                return _ringPositionJT2Left;
            }
            set
            {
                _ringPositionJT2Left = value;
                NotifyPropertyChanged("RingPositionJT2Left");
            }
        }
        private string _ringPositionJT2Left = "0";
        public string RingPositionJT1Left
        {
            get
            {
                return _ringPositionJT1Left;
            }
            set
            {
                _ringPositionJT1Left = value;
                NotifyPropertyChanged("RingPositionJT1Left");
            }
        }
        private string _ringPositionJT1Left = "0";
        public string RingPositionBaseLeft
        {
            get
            {
                return _ringPositionBaseLeft;
            }
            set
            {
                _ringPositionBaseLeft = value;
                NotifyPropertyChanged("RingPositionBaseLeft");
            }
        }
        private string _ringPositionBaseLeft = "0";

        public string RingPositionImageTipLeft
        {
            get
            {
                return _ringPositionImageTipLeft;
            }
            set
            {
                _ringPositionImageTipLeft = value;
                NotifyPropertyChanged("RingPositionImageTipLeft");
            }
        }
        private string _ringPositionImageTipLeft = "0";
        public string RingPositionImageJT2Left
        {
            get
            {
                return _ringPositionImageJT2Left;
            }
            set
            {
                _ringPositionImageJT2Left = value;
                NotifyPropertyChanged("RingPositionImageJT2Left");
            }
        }
        private string _ringPositionImageJT2Left = "0";
        public string RingPositionImageJT1Left
        {
            get
            {
                return _ringPositionImageJT1Left;
            }
            set
            {
                _ringPositionImageJT1Left = value;
                NotifyPropertyChanged("RingPositionImageJT1Left");
            }
        }
        private string _ringPositionImageJT1Left = "0";
        public string RingPositionImageBaseLeft
        {
            get
            {
                return _ringPositionImageBaseLeft;
            }
            set
            {
                _ringPositionImageBaseLeft = value;
                NotifyPropertyChanged("RingPositionImageBaseLeft");
            }
        }
        private string _ringPositionImageBaseLeft = "0";

        public string RingRotationTipLeft
        {
            get
            {
                return _ringRotationTipLeft;
            }
            set
            {
                _ringRotationTipLeft = value;
                NotifyPropertyChanged("RingRotationTipLeft");
            }
        }
        private string _ringRotationTipLeft = "0";
        public string RingRotationJT2Left
        {
            get
            {
                return _ringRotationJT2Left;
            }
            set
            {
                _ringRotationJT2Left = value;
                NotifyPropertyChanged("RingRotationJT2Left");
            }
        }
        private string _ringRotationJT2Left = "0";
        public string RingRotationJT1Left
        {
            get
            {
                return _ringRotationJT1Left;
            }
            set
            {
                _ringRotationJT1Left = value;
                NotifyPropertyChanged("RingRotationJT1Left");
            }
        }
        private string _ringRotationJT1Left = "0";
        public string RingRotationBaseLeft
        {
            get
            {
                return _ringRotationBaseLeft;
            }
            set
            {
                _ringRotationBaseLeft = value;
                NotifyPropertyChanged("RingRotationBaseLeft");
            }
        }
        private string _ringRotationBaseLeft = "0";
        #endregion

        #region Left Pingky Properties
        public string PingkyFlexsionLeft
        {
            get
            {
                return _pingkyFlexsionLeft;
            }
            set
            {
                _pingkyFlexsionLeft = value;
                NotifyPropertyChanged("PingkyFlexsionLeft");
            }
        }
        private string _pingkyFlexsionLeft = "0";
        public string PingkyPositionTipLeft
        {
            get
            {
                return _pingkyPositionTipLeft;
            }
            set
            {
                _pingkyPositionTipLeft = value;
                NotifyPropertyChanged("PingkyPositionTipLeft");
            }
        }
        private string _pingkyPositionTipLeft = "0";
        public string PingkyPositionJT2Left
        {
            get
            {
                return _pingkyPositionJT2Left;
            }
            set
            {
                _pingkyPositionJT2Left = value;
                NotifyPropertyChanged("PingkyPositionJT2Left");
            }
        }
        private string _pingkyPositionJT2Left = "0";
        public string PingkyPositionJT1Left
        {
            get
            {
                return _pingkyPositionJT1Left;
            }
            set
            {
                _pingkyPositionJT1Left = value;
                NotifyPropertyChanged("PingkyPositionJT1Left");
            }
        }
        private string _pingkyPositionJT1Left = "0";
        public string PingkyPositionBaseLeft
        {
            get
            {
                return _pingkyPositionBaseLeft;
            }
            set
            {
                _pingkyPositionBaseLeft = value;
                NotifyPropertyChanged("PingkyPositionBaseLeft");
            }
        }
        private string _pingkyPositionBaseLeft = "0";

        public string PingkyPositionImageTipLeft
        {
            get
            {
                return _pingkyPositionImageTipLeft;
            }
            set
            {
                _pingkyPositionImageTipLeft = value;
                NotifyPropertyChanged("PingkyPositionImageTipLeft");
            }
        }
        private string _pingkyPositionImageTipLeft = "0";
        public string PingkyPositionImageJT2Left
        {
            get
            {
                return _pingkyPositionImageJT2Left;
            }
            set
            {
                _pingkyPositionImageJT2Left = value;
                NotifyPropertyChanged("PingkyPositionImageJT2Left");
            }
        }
        private string _pingkyPositionImageJT2Left = "0";
        public string PingkyPositionImageJT1Left
        {
            get
            {
                return _pingkyPositionImageJT1Left;
            }
            set
            {
                _pingkyPositionImageJT1Left = value;
                NotifyPropertyChanged("PingkyPositionImageJT1Left");
            }
        }
        private string _pingkyPositionImageJT1Left = "0";
        public string PingkyPositionImageBaseLeft
        {
            get
            {
                return _pingkyPositionImageBaseLeft;
            }
            set
            {
                _pingkyPositionImageBaseLeft = value;
                NotifyPropertyChanged("PingkyPositionImageBaseLeft");
            }
        }
        private string _pingkyPositionImageBaseLeft = "0";

        public string PingkyRotationTipLeft
        {
            get
            {
                return _pingkyRotationTipLeft;
            }
            set
            {
                _pingkyRotationTipLeft = value;
                NotifyPropertyChanged("PingkyRotationTipLeft");
            }
        }
        private string _pingkyRotationTipLeft = "0";
        public string PingkyRotationJT2Left
        {
            get
            {
                return _pingkyRotationJT2Left;
            }
            set
            {
                _pingkyRotationJT2Left = value;
                NotifyPropertyChanged("PingkyRotationJT2Left");
            }
        }
        private string _pingkyRotationJT2Left = "0";
        public string PingkyRotationJT1Left
        {
            get
            {
                return _pingkyRotationJT1Left;
            }
            set
            {
                _pingkyRotationJT1Left = value;
                NotifyPropertyChanged("PingkyRotationJT1Left");
            }
        }
        private string _pingkyRotationJT1Left = "0";
        public string PingkyRotationBaseLeft
        {
            get
            {
                return _pingkyRotationBaseLeft;
            }
            set
            {
                _pingkyRotationBaseLeft = value;
                NotifyPropertyChanged("PingkyRotationBaseLeft");
            }
        }
        private string _pingkyRotationBaseLeft = "0";
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class HandPreprocessedModel
    {

    }
}
