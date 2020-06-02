using Intel.RealSense;
using Intel.RealSense.Hand;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealsenseHandler.RsHand
{
    public class Hand
    {
       
        public Finger Thumb { get; set; }
        public Finger Index { get; set; }
        public Finger Middle { get; set; }
        public Finger Ring { get; set; }
        public Finger Pingky { get; set; }

        public EulerAngle orientation { get; set; }

        public bool IsTracked { get; set; }
        public Hand(IHand ihand)
        {
           
            if (ihand != null)
            {
                
                if (!ihand.HasTrackedJoints())
                    IsTracked = false;
                else
                    IsTracked = true;


                if (IsTracked)
                {
                    PopulateHandFinger(ihand);
                    PopulateHandProperty(ihand);
                }
            }
        }

        private void PopulateHandFinger(IHand hand)
        {
           
            //Thumb
            Thumb = new Finger
            {

                Foldness = hand.FingerData[FingerType.FINGER_THUMB].foldedness,
                JointsPosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_THUMB_BASE].positionWorld, hand.TrackedJoints[JointType.JOINT_THUMB_JT1].positionWorld,
                    hand.TrackedJoints[JointType.JOINT_THUMB_JT2].positionWorld, hand.TrackedJoints[JointType.JOINT_THUMB_TIP].positionWorld,
                    hand.TrackedJoints[JointType.JOINT_CENTER].positionWorld
                },
                JointsImagePosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_THUMB_BASE].positionImage, hand.TrackedJoints[JointType.JOINT_THUMB_JT1].positionImage,
                    hand.TrackedJoints[JointType.JOINT_THUMB_JT2].positionImage, hand.TrackedJoints[JointType.JOINT_THUMB_TIP].positionImage, 
                    hand.TrackedJoints[JointType.JOINT_CENTER].positionImage
                },
                JointsOrientation = new Point4DF32[]
                {
                    hand.TrackedJoints[JointType.JOINT_THUMB_BASE].globalOrientation, hand.TrackedJoints[JointType.JOINT_THUMB_JT1].globalOrientation,
                    hand.TrackedJoints[JointType.JOINT_THUMB_JT2].globalOrientation, hand.TrackedJoints[JointType.JOINT_THUMB_TIP].globalOrientation
                }
            };

            //Index
            Index = new Finger
            {
                Foldness = hand.FingerData[FingerType.FINGER_INDEX].foldedness,
                JointsPosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_INDEX_BASE].positionWorld, hand.TrackedJoints[JointType.JOINT_INDEX_JT1].positionWorld,
                    hand.TrackedJoints[JointType.JOINT_INDEX_JT2].positionWorld, hand.TrackedJoints[JointType.JOINT_INDEX_TIP].positionWorld,
                    hand.TrackedJoints[JointType.JOINT_CENTER].positionWorld
                },
                JointsImagePosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_INDEX_BASE].positionImage, hand.TrackedJoints[JointType.JOINT_INDEX_JT1].positionImage,
                    hand.TrackedJoints[JointType.JOINT_INDEX_JT2].positionImage, hand.TrackedJoints[JointType.JOINT_INDEX_TIP].positionImage,
                    hand.TrackedJoints[JointType.JOINT_CENTER].positionImage
                },
                JointsOrientation = new Point4DF32[]
                {
                    hand.TrackedJoints[JointType.JOINT_INDEX_BASE].globalOrientation, hand.TrackedJoints[JointType.JOINT_INDEX_JT1].globalOrientation,
                    hand.TrackedJoints[JointType.JOINT_INDEX_JT2].globalOrientation, hand.TrackedJoints[JointType.JOINT_INDEX_TIP].globalOrientation
                }
            };

            //Middle
            Middle = new Finger
            {
                Foldness = hand.FingerData[FingerType.FINGER_MIDDLE].foldedness,
                JointsPosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_MIDDLE_BASE].positionWorld, hand.TrackedJoints[JointType.JOINT_MIDDLE_JT1].positionWorld,
                    hand.TrackedJoints[JointType.JOINT_MIDDLE_JT2].positionWorld, hand.TrackedJoints[JointType.JOINT_MIDDLE_TIP].positionWorld, 
                    hand.TrackedJoints[JointType.JOINT_CENTER].positionWorld
                },
                JointsImagePosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_MIDDLE_BASE].positionImage, hand.TrackedJoints[JointType.JOINT_MIDDLE_JT1].positionImage,
                    hand.TrackedJoints[JointType.JOINT_MIDDLE_JT2].positionImage, hand.TrackedJoints[JointType.JOINT_MIDDLE_TIP].positionImage,
                    hand.TrackedJoints[JointType.JOINT_CENTER].positionImage
                },
                JointsOrientation = new Point4DF32[]
                {
                    hand.TrackedJoints[JointType.JOINT_MIDDLE_BASE].globalOrientation, hand.TrackedJoints[JointType.JOINT_MIDDLE_JT1].globalOrientation,
                    hand.TrackedJoints[JointType.JOINT_MIDDLE_JT2].globalOrientation, hand.TrackedJoints[JointType.JOINT_MIDDLE_TIP].globalOrientation
                }
            };

            //Ring
            Ring = new Finger
            {
                Foldness = hand.FingerData[FingerType.FINGER_RING].foldedness,
                JointsPosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_RING_BASE].positionWorld, hand.TrackedJoints[JointType.JOINT_RING_JT1].positionWorld,
                    hand.TrackedJoints[JointType.JOINT_RING_JT2].positionWorld, hand.TrackedJoints[JointType.JOINT_RING_TIP].positionWorld, 
                    hand.TrackedJoints[JointType.JOINT_CENTER].positionWorld
                },
                JointsImagePosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_RING_BASE].positionImage, hand.TrackedJoints[JointType.JOINT_RING_JT1].positionImage,
                    hand.TrackedJoints[JointType.JOINT_RING_JT2].positionImage, hand.TrackedJoints[JointType.JOINT_RING_TIP].positionImage,
                    hand.TrackedJoints[JointType.JOINT_CENTER].positionImage
                },
                JointsOrientation = new Point4DF32[]
                {
                    hand.TrackedJoints[JointType.JOINT_RING_BASE].globalOrientation, hand.TrackedJoints[JointType.JOINT_RING_JT1].globalOrientation,
                    hand.TrackedJoints[JointType.JOINT_RING_JT2].globalOrientation, hand.TrackedJoints[JointType.JOINT_RING_TIP].globalOrientation
                }
            };

            //Pinky
            Pingky = new Finger
            {
                Foldness = hand.FingerData[FingerType.FINGER_PINKY].foldedness,
                JointsPosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_PINKY_BASE].positionWorld, hand.TrackedJoints[JointType.JOINT_PINKY_JT1].positionWorld,
                    hand.TrackedJoints[JointType.JOINT_PINKY_JT2].positionWorld, hand.TrackedJoints[JointType.JOINT_PINKY_TIP].positionWorld, 
                    hand.TrackedJoints[JointType.JOINT_CENTER].positionWorld
                },
                JointsImagePosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_PINKY_BASE].positionImage, hand.TrackedJoints[JointType.JOINT_PINKY_JT1].positionImage,
                    hand.TrackedJoints[JointType.JOINT_PINKY_JT2].positionImage, hand.TrackedJoints[JointType.JOINT_PINKY_TIP].positionImage,
                    hand.TrackedJoints[JointType.JOINT_CENTER].positionImage
                },
                JointsOrientation = new Point4DF32[]
                {
                    hand.TrackedJoints[JointType.JOINT_PINKY_BASE].globalOrientation, hand.TrackedJoints[JointType.JOINT_PINKY_JT1].globalOrientation,
                    hand.TrackedJoints[JointType.JOINT_PINKY_JT2].globalOrientation, hand.TrackedJoints[JointType.JOINT_PINKY_TIP].globalOrientation
                }
            };
        }

        private void PopulateHandProperty(IHand hand)
        {
            orientation = new EulerAngle { Quarternion = hand.PalmOrientation };
        }
    }

    public class Finger
    {
        public int Foldness { get; set; }
        public Point3DF32[] JointsPosition { get; set; } 
        public Point3DF32[] JointsImagePosition { get; set; }
        public Point4DF32[] JointsOrientation { get; set; }
        
    }

    public class EulerAngle
    {
        public double Roll { get 
            {
                return Math.Atan2(2 * ((Quarternion.w * Quarternion.x) + (Quarternion.y * Quarternion.z)), 
                    1 - 2 * ((Quarternion.x * Quarternion.x) + (Quarternion.y * Quarternion.y)));
            } 
        }
        public double Pitch { get 
            {
                return Math.Asin(2 * ((Quarternion.w * Quarternion.y) - (Quarternion.z * Quarternion.x)));
            } 
        }
        public double Yaw { get
            {
                return Math.Atan2(2 * ((Quarternion.w * Quarternion.z) + (Quarternion.x * Quarternion.y)),
                    1 - 2 * ((Quarternion.z * Quarternion.z) + (Quarternion.y * Quarternion.y)));
            }
        }
        public Point4DF32 Quarternion { get; set; }
    }

    public class HandPreprocessed
    {
        FingerPreprocessed Thumb { get; set; }
        FingerPreprocessed Index { get; set; }
        FingerPreprocessed Middle { get; set; }
        FingerPreprocessed Ring { get; set; }
        FingerPreprocessed Pinky { get; set; }

        public HandPreprocessed(Hand hand)
        {
            Thumb = new FingerPreprocessed(hand.Thumb);
            Index = new FingerPreprocessed(hand.Index);
            Middle = new FingerPreprocessed(hand.Middle);
            Ring = new FingerPreprocessed(hand.Ring);
            Pinky = new FingerPreprocessed(hand.Pingky);
        }

        public string GetStream()
        {
            return $"{Thumb.GetStream()}{Index.GetStream()}{Middle.GetStream()}{Ring.GetStream()}{Pinky.GetStream()}";
        }
    }

    public class FingerPreprocessed
    {
        readonly CultureInfo ci = new CultureInfo("en-US");

        public int Foldness { get; set; }
        public Point3DF32 DeltaTipJt { get; set; }
        public Point3DF32 DeltaJtJt { get; set; }
        public Point3DF32 DeltaJtBase { get; set; }
        public Point3DF32 DeltaBaseCenter { get; set; }
        public Point3DF32 DeltaTipJtImage { get; set; }
        public Point3DF32 DeltaJtJtImage { get; set; }
        public Point3DF32 DeltaJtBaseImage { get; set; }
        public Point3DF32 DeltaBaseCenterImage { get; set; }

        public FingerPreprocessed(Finger finger)
        {
            Foldness = finger.Foldness;
            DeltaTipJt = GetDeltaPoint(finger.JointsPosition[0], finger.JointsPosition[1]);
            DeltaJtJt = GetDeltaPoint(finger.JointsPosition[1], finger.JointsPosition[2]);
            DeltaJtBase = GetDeltaPoint(finger.JointsPosition[3], finger.JointsPosition[3]);
            DeltaBaseCenter = GetDeltaPoint(finger.JointsPosition[4], finger.JointsPosition[0]);

            DeltaTipJtImage = GetDeltaPoint(finger.JointsImagePosition[0], finger.JointsImagePosition[1]);
            DeltaJtJtImage = GetDeltaPoint(finger.JointsImagePosition[1], finger.JointsImagePosition[2]);
            DeltaJtBaseImage = GetDeltaPoint(finger.JointsImagePosition[3], finger.JointsImagePosition[3]);
            DeltaBaseCenterImage = GetDeltaPoint(finger.JointsImagePosition[4], finger.JointsImagePosition[0]);
        }

        public string GetStream()
        {
            return $"{Foldness}," +
                $"{ PopulateFingerPositionDataToString(DeltaTipJt)}," +
                $"{ PopulateFingerPositionDataToString(DeltaJtJt)}," +
                $"{ PopulateFingerPositionDataToString(DeltaJtBase)}," +
                $"{ PopulateFingerPositionDataToString(DeltaBaseCenter)}," +
                $"{ PopulateFingerPositionDataToString(DeltaTipJtImage)}," +
                $"{ PopulateFingerPositionDataToString(DeltaJtJtImage)}," +
                $"{ PopulateFingerPositionDataToString(DeltaJtBaseImage)}," +
                $"{ PopulateFingerPositionDataToString(DeltaBaseCenterImage)},";
        }


        private string PopulateFingerPositionDataToString(Point3DF32 finger)
        {
            string result = Math.Round(finger.x, 3).ToString(ci) + "," + Math.Round(finger.y, 3).ToString(ci) + "," + Math.Round(finger.z, 3).ToString(ci);
            return result;
        }
        private Point3DF32 GetDeltaPoint(Point3DF32 point1, Point3DF32 point2)
        {
            return new Point3DF32(point1.x - point2.x, point1.y - point2.y, point1.z - point2.z);
        }

    }

    #region Just in case i need to move again to 2016 R3 SDK
    /*
    //Thumb
            Thumb = new Finger
            {
                
                Foldness = thumbFingerData.foldedness,
                JointsPosition = new PXCMPoint3DF32[] {
                    t.positionWorld, hand.TrackedJoints[JointType.JOINT_THUMB_JT1].positionWorld,
                    hand.TrackedJoints[JointType.JOINT_THUMB_JT2].positionWorld, hand.TrackedJoints[JointType.JOINT_THUMB_TIP].positionWorld
                },
                JointsImagePosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_THUMB_BASE].positionImage, hand.TrackedJoints[JointType.JOINT_THUMB_JT1].positionImage,
                    hand.TrackedJoints[JointType.JOINT_THUMB_JT2].positionImage, hand.TrackedJoints[JointType.JOINT_THUMB_TIP].positionImage
                },
                JointsOrientation = new Point4DF32[]
                {
                    hand.TrackedJoints[JointType.JOINT_THUMB_BASE].globalOrientation, hand.TrackedJoints[JointType.JOINT_THUMB_JT1].globalOrientation,
                    hand.TrackedJoints[JointType.JOINT_THUMB_JT2].globalOrientation, hand.TrackedJoints[JointType.JOINT_THUMB_TIP].globalOrientation
                }
            };

            //Index
            Index = new Finger
            {
                Foldness = hand.FingerData[FingerType.FINGER_INDEX].foldedness,
                JointsPosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_INDEX_BASE].positionWorld, hand.TrackedJoints[JointType.JOINT_INDEX_JT1].positionWorld,
                    hand.TrackedJoints[JointType.JOINT_INDEX_JT2].positionWorld, hand.TrackedJoints[JointType.JOINT_INDEX_TIP].positionWorld
                },
                JointsImagePosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_INDEX_BASE].positionImage, hand.TrackedJoints[JointType.JOINT_INDEX_JT1].positionImage,
                    hand.TrackedJoints[JointType.JOINT_INDEX_JT2].positionImage, hand.TrackedJoints[JointType.JOINT_INDEX_TIP].positionImage
                },
                JointsOrientation = new Point4DF32[]
                {
                    hand.TrackedJoints[JointType.JOINT_INDEX_BASE].globalOrientation, hand.TrackedJoints[JointType.JOINT_INDEX_JT1].globalOrientation,
                    hand.TrackedJoints[JointType.JOINT_INDEX_JT2].globalOrientation, hand.TrackedJoints[JointType.JOINT_INDEX_TIP].globalOrientation
                }
            };

            //Middle
            Middle = new Finger
            {
                Foldness = hand.FingerData[FingerType.FINGER_MIDDLE].foldedness,
                JointsPosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_MIDDLE_BASE].positionWorld, hand.TrackedJoints[JointType.JOINT_MIDDLE_JT1].positionWorld,
                    hand.TrackedJoints[JointType.JOINT_MIDDLE_JT2].positionWorld, hand.TrackedJoints[JointType.JOINT_MIDDLE_TIP].positionWorld
                },
                JointsImagePosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_MIDDLE_BASE].positionImage, hand.TrackedJoints[JointType.JOINT_MIDDLE_JT1].positionImage,
                    hand.TrackedJoints[JointType.JOINT_MIDDLE_JT2].positionImage, hand.TrackedJoints[JointType.JOINT_MIDDLE_TIP].positionImage
                },
                JointsOrientation = new Point4DF32[]
                {
                    hand.TrackedJoints[JointType.JOINT_MIDDLE_BASE].globalOrientation, hand.TrackedJoints[JointType.JOINT_MIDDLE_JT1].globalOrientation,
                    hand.TrackedJoints[JointType.JOINT_MIDDLE_JT2].globalOrientation, hand.TrackedJoints[JointType.JOINT_MIDDLE_TIP].globalOrientation
                }
            };

            //Ring
            Ring = new Finger
            {
                Foldness = hand.FingerData[FingerType.FINGER_RING].foldedness,
                JointsPosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_RING_BASE].positionWorld, hand.TrackedJoints[JointType.JOINT_RING_JT1].positionWorld,
                    hand.TrackedJoints[JointType.JOINT_RING_JT2].positionWorld, hand.TrackedJoints[JointType.JOINT_RING_TIP].positionWorld
                },
                JointsImagePosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_RING_BASE].positionImage, hand.TrackedJoints[JointType.JOINT_RING_JT1].positionImage,
                    hand.TrackedJoints[JointType.JOINT_RING_JT2].positionImage, hand.TrackedJoints[JointType.JOINT_RING_TIP].positionImage
                },
                JointsOrientation = new Point4DF32[]
                {
                    hand.TrackedJoints[JointType.JOINT_RING_BASE].globalOrientation, hand.TrackedJoints[JointType.JOINT_RING_JT1].globalOrientation,
                    hand.TrackedJoints[JointType.JOINT_RING_JT2].globalOrientation, hand.TrackedJoints[JointType.JOINT_RING_TIP].globalOrientation
                }
            };

            //Pinky
            Pingky = new Finger
            {
                Foldness = hand.FingerData[FingerType.FINGER_PINKY].foldedness,
                JointsPosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_PINKY_BASE].positionWorld, hand.TrackedJoints[JointType.JOINT_PINKY_JT1].positionWorld,
                    hand.TrackedJoints[JointType.JOINT_PINKY_JT2].positionWorld, hand.TrackedJoints[JointType.JOINT_PINKY_TIP].positionWorld
                },
                JointsImagePosition = new Point3DF32[] {
                    hand.TrackedJoints[JointType.JOINT_PINKY_BASE].positionImage, hand.TrackedJoints[JointType.JOINT_PINKY_JT1].positionImage,
                    hand.TrackedJoints[JointType.JOINT_PINKY_JT2].positionImage, hand.TrackedJoints[JointType.JOINT_PINKY_TIP].positionImage
                },
                JointsOrientation = new Point4DF32[]
                {
                    hand.TrackedJoints[JointType.JOINT_PINKY_BASE].globalOrientation, hand.TrackedJoints[JointType.JOINT_PINKY_JT1].globalOrientation,
                    hand.TrackedJoints[JointType.JOINT_PINKY_JT2].globalOrientation, hand.TrackedJoints[JointType.JOINT_PINKY_TIP].globalOrientation
                }
            };
    


    */
    #endregion
}
