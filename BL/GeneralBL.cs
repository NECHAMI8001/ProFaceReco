using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;
using System.IO;
using Emgu.CV;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Emgu.CV.Structure;
using Emgu.CV.Face;
using Emgu.CV.Text;
using Emgu.CV.Util;
using Emgu.CV.UI;
using Emgu.CV.ImgHash;

using Emgu.CV.Features2D;
using Emgu.CV.Flann;
using Emgu.CV.Cuda;
using System.Runtime.InteropServices;
using Emgu.CV.CvEnum;
using Emgu.CV.OCR;
//using ClosedXML.Excel;
//using EmgucvDemo.Models;
using Emgu.CV.ML;
using Emgu.CV.ML.MlEnum;
using System.Runtime.InteropServices.WindowsRuntime;
using Emgu.CV.Stitching;
using Emgu.CV.Dnn;
using System.Text.RegularExpressions;

namespace BL.Classes
{
    public class GeneralBL
    {
        VectorOfVectorOfPointF landmarks;
        double Normalization = 0.0;
        List<PointFace> d = new List<PointFace>();
        List<SamePoint> sm = new List<SamePoint>();

        string Distpoint1 = "";
        double disE = 0.0;
        List<Models.StudentTBLModel> listOfStudent_Tbl;
        DBConnection dbCon;
        Models.StudentTBLModel Mstudent = new Models.StudentTBLModel();
        private static Regex r = new Regex(":");
        DateTime dateTime = new DateTime();
        PresenceBL presenceBL = new PresenceBL();
        Models.PresenceTBLModel presence = new PresenceTBLModel();

        //public int LoadDataStudent(string path, int GradeCode)
        //{
        //    return 1;
        //}

        public GeneralBL()
        {
            dbCon = new DBConnection();
            listOfStudent_Tbl = ConvertListToModel(dbCon.GetDbSet<Student_Tbl>().ToList());
        }
        public static List<Models.StudentTBLModel> ConvertListToModel(List<Student_Tbl> li)
        {
            return li.Select(l => ConvertStudentToModel(l)).ToList();
        }
        public static Models.StudentTBLModel ConvertStudentToModel(Student_Tbl a)
        {
            return new Models.StudentTBLModel
            {
                Student_ID = a.Student_ID,
                Student_Name = a.Student_Name,
                Student_Image = a.Student_Image,
                Student_Grade_Code = a.Student_Grade_Code,
                DistancePoint = a.DistancePoint,
                DistPoint = a.DistPoint

            };
        }

        public string landmarkDetection(string imagePath)
        {

            try
            {
                if (imagePath == null)
                {
                    return "";
                }
                string rootDirectory = "C:\\Users\\User\\source\\repos\\FaceRecognition\\BL\\";
                string lbpFacepath = rootDirectory + "data\\lbpcascade_frontalface_improved.xml";
                string modelPath = rootDirectory + "data\\lbfmodel.yaml";
                //גישה מבוססת למידת מכונה מאומן מהרבה תמונות ומשמש לזיהוי עצמים בתמונות אחרות
                CascadeClassifier classifier = new CascadeClassifier(lbpFacepath);
                //נקודות על הפנים ואימון 
                FacemarkLBFParams facemarkLBF = new FacemarkLBFParams();
                FacemarkLBF facemark = new FacemarkLBF(facemarkLBF);

                //  var img = new Bitmap(pictureBox1.Image).ToImage<Bgr, byte>();
                //את נתיב התמונה ממירה לפיקסלים
                var img = new Bitmap(imagePath).ToImage<Bgr, byte>();
                var imgGray = img.Convert<Gray, byte>();
                var faces = classifier.DetectMultiScale(imgGray);

                facemark.LoadModel(modelPath);


                landmarks = new VectorOfVectorOfPointF();
                VectorOfRect rects = new VectorOfRect(faces);
                bool result = facemark.Fit(imgGray, rects, landmarks);
                if (result)
                {
                    for (int i = 0; i < faces.Length; i++)
                    {
                        FaceInvoke.DrawFacemarks(img, landmarks[i], new MCvScalar(0, 0, 255));
                        var p = landmarks[i][33];
                        // d.Add(landmarks[i][i].X);
                        for (int j = 0; j < landmarks[0].Length; j++)
                        {
                            if (j >= 1 && j <= 17)
                            {
                                PointFace p1 = new PointFace();
                                p1.x = landmarks[0][j].X;
                                p1.y = landmarks[0][j].Y;
                                d.Add(p1);

                            }

                        }
                        for (long k = landmarks[0].Length; k > 0; k--)
                        {
                            if (k <= 27 && k >= 18)
                            {


                                int h = Convert.ToInt32(k);
                                PointFace p2 = new PointFace();
                                p2.x = landmarks[0][h].X;
                                p2.y = landmarks[0][h].Y;
                                d.Add(p2);
                            }
                        }
                        disE = n(d);
                        Normalization = disE / 1000;


                        CvInvoke.Circle(img, new Point((int)p.X, (int)p.Y), 5, new MCvScalar(0, 255, 0), -1);
                    }
                }
                //pictureBox1.Image = img.ToBitmap();
                return CalcResultDistance();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "";
        }

        public void PresenceStudent(string imgPath)
        {
            string str = landmarkDetection(imgPath);
            string id = check(str);
            bool isPresence;
            dateTime = GetDateTakenFromImage(imgPath);

            DateTime d = dateTime;

            string StrTime = d.TimeOfDay.ToString();
            TimeSpan HourLesson = TimeSpan.Parse(StrTime);
            //  d.TimeOfDay(imgPath);

            if (id != "")
            {
                isPresence = true;
            }
            else
            {
                isPresence = false;
            }
            presence.Presence_Student_ID = id;
            presence.Presence_TimeBLesson = isPresence;
            presence.Presence_Date = dateTime;
            presence.Presence_Hour = HourLesson;
            presence.Presence_Code = presenceBL.GetCount() + 1;
            presenceBL.InsertPresence(presence);


        }
        PointFace p = new PointFace();
        public double n(List<PointFace> l)
        {
            double distance = 0.0, dist = 0.0;

            PointFace f = l[0];
            for (int i = 0; i < l.Count; i++)
            {
                if (i + 1 != l.Count)
                {
                    distance = Math.Sqrt((Math.Pow(l[i].x - l[i + 1].x, 2) + Math.Pow(l[i].y - l[i + 1].y, 2)));
                    dist += distance;
                }
                else
                {
                    int coun = l.Count - 1;
                    distance = Math.Sqrt((Math.Pow(l[coun].x - f.x, 2) + Math.Pow(l[coun].y - f.y, 2)));
                    dist += distance;
                }

            }

            return dist;

            //int[][] distCalcArr ={

            //    new int[] { 1,18 },
            //    new int[] { 18,37 },
            //    new int[] { 40, 28 },
            //    new int[] {43,28},
            //    new int[] {28,22}


            // };
        }
        public double distanation(int index1, int index2, VectorOfVectorOfPointF land)
        {

            float x1 = landmarks[0][index1].X;
            float y1 = landmarks[0][index1].Y;
            float x2 = landmarks[0][index2].X;
            float y2 = landmarks[0][index2].Y;
            double distance = Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
            return distance;
        }



        public string CalcResultDistance()
        {
            // int w = Convert.ToInt32(disE);

            int[][] distCalcArr ={
#region Points
                new int[] { 1,37 },
                             new int[] { 18,37 },
                             new int[] { 40, 28 },
                             new int[] {43,28},
                             new int[] {28,22},
                             new int[] {28,23},
                             new int[] {32,49},
                             new int[] {36,55},
                             new int[] {34,52},
                             new int[] {49,6},
                             new int[] {55,13},
                             new int[] {17,46},
                             new int[] {22,23},
                             new int[] {22,40},
                             new int[] {23,43},
                             new int[] {36,15},
                             new int[] {32,3},
                             new int[] {1,32},
                             new int[] {17,36},
                             new int[] {27,17},
                             new int[] {18,1},
                             new int[] {60,7},
                             new int[] {59,8},
                             new int[] {58,9},
                             new int[] {57,10},
                             new int[] {56,11}
                             
                          

                             

#endregion
            };



            //StreamReader s = new StreamReader(@"data/pointFace.txt");
            //for (int i=0; i<s. ;i++)
            //{
            //    int x = int.Parse(s.ReadLine());

            //}

            double[] results = new double[26];
            for (int i = 0; i < distCalcArr.Length; i++)
            {
                results[i] = distanation(distCalcArr[i][0], distCalcArr[i][1], landmarks);
                var s = results[i] / Normalization;
                Distpoint1 += s.ToString() + ",";
                // Distpoint1 += results[i].ToString() + ",";
            }
            return Distpoint1;
        }

        public double[] Range()
        {
            double[] t = new double[] { 11.1768 ,
             2.40439,
             5.05715 ,
             5.21224,
             3.72668 ,
             3.79618,
             4.27426,
             6.19737,
             3.69821,
             9.65218,
             9.88852,
             5.59321 ,
             1.83386,
             5.11695 ,
             3.31777,
             9.51118 ,
             15.5166,
             13.4662,
             2.73144,
             7.32891,
             10.6638,
             9.59826 ,
             6.93734,
             5.53679,
             6.94058,
             9.09978 };

            return t;
        }
        public List<double> StringToDouble(string str)
        {
            //הגדרת רשימה לנקודות
            List<double> d = new List<double>();
            //חיתוך המחרוזת לפי ,
            string[] PointsList = str.Split(',');
            //המרת המחרוזות לדאבל
            foreach (string point in PointsList)
            {
                if (point != "")
                    d.Add(double.Parse(point));
            }
            return d;
        }
        public string check(string dist)
        {
            string IDstudent = "";

            //הגדרת רשימה לנקודות
            List<double> d = new List<double>();
            d = StringToDouble(dist);
            List<double> PointPlus = new List<double>();
            List<double> PointMinus = new List<double>();
            double[] tv = new double[26];
            tv = Range();
            for (int i = 0; i < 26; i++)
            {
                PointPlus.Add(d[i] + tv[i]);
                PointMinus.Add(d[i] - tv[i]);
            }
            int mone = 0;
            for (int i = 0; i < listOfStudent_Tbl.Count; i++)
            {

                for (int j = 0; j < tv.Length; j++)
                {
                    List<double> l = new List<double>();
                    //רשימת נקודות מהמסד נתונים
                    l = StringToDouble(listOfStudent_Tbl[i].DistPoint);
                    //  SamePoint samePoint = new SamePoint();

                    if (l[j] >= PointMinus[j] && l[j] <= PointPlus[j])
                    {
                        mone++;
                    }
                }
                SamePoint samePoint = new SamePoint();
                samePoint.StID = listOfStudent_Tbl[i].Student_ID;
                samePoint.CountPoint = mone;

                sm.Add(samePoint);
                mone = 0;

                //if (mone >= 18)
                //{
                //    return listOfStudent_Tbl[i].Student_ID;
                //    break;
                //}

            }
            IDstudent = MaxPoint(sm);
            return IDstudent;
            //if (String.Equals(dist, Mstudent.DistPoint))
            //    return true;

        }
        public string MaxPoint(List<SamePoint> same)
        {
            int max = 0;
            string ID = "";
            for (int i = 0; i < same.Count; i++)
            {
                if (max < same[i].CountPoint)
                {
                    max = same[i].CountPoint;
                    ID = same[i].StID;
                }
            }
            return ID;
        }

        public static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(myImage.PropertyItems.First(a => a.Id == 36867).Id);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }
    }
}
