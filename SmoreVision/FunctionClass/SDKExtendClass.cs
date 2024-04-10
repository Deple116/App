using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmoreVision.FunctionClass
{
    public class SDKExtendClass
    {
        /// <summary>
        /// 检测算法
        /// </summary>
        public static class IDetectionResponse
        {

        }

        /// <summary>
        /// 分类算法
        /// </summary>
        public static class IClassificationResponse
        {

        }

        /// <summary>
        /// OCR算法
        /// </summary>
        public static class IOcrResponse
        {
            private const int ERROR_OK = 0;
            private const int ERROR_FAILED = -1;
            /// <summary>
            /// 字符的初始输出坐标集合
            /// </summary>
            public static List<Point2f> m_TextPoint = new List<Point2f>();
            /// <summary>
            /// 字符进行排序后的坐标集合
            /// </summary>
            public static List<Point2f> m_TextPointSorted = new List<Point2f>();
            /// <summary>
            /// 字符进行排序后的坐标集合
            /// </summary>
            public static List<Point2f> m_Line3PointSorted = new List<Point2f>();
            /// <summary>
            /// 以坐标为键值将字符内容存入字典
            /// </summary>
            public static Dictionary<Point2f, string> m_TextDic = new Dictionary<Point2f, string>();
            /// <summary>
            /// 输出渲染图坐标集合
            /// </summary>
            public static IList<IList<OpenCvSharp.Point>> ListPoints = new List<IList<OpenCvSharp.Point>>();
            /// <summary>
            /// 输出渲染图字符集合
            /// </summary>
            public  static IList<string> TextList = new List<string>();

            /// <summary>
            /// 初始化所有集合
            /// </summary>
            /// <returns></returns>
            public static int InitCollection()
            {
                try
                {
                    m_TextPoint.Clear();
                    m_TextPointSorted.Clear();
                    m_TextDic.Clear();
                    return ERROR_OK;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }

            /// <summary>
            /// 按照输出字符的某个坐标位置对输出字符进行从左到右，从上到下的顺序进行输出
            /// </summary>
            /// <param name="_pointsList"></param>
            /// <returns></returns>
            public static int PointXYSort(List<Point2f> _pointsList,ref string _ocrResult, ref string _Line3Result)
            {
                try
                {
                    int LineSpacing = 30;
                    int line = 0;
                    _ocrResult = string.Empty;
                    m_TextPointSorted.Clear();
                    m_Line3PointSorted.Clear();
                    List<Point2f> YSortedPointList = new List<Point2f>();
                    List<Point2f> RowPointList = new List<Point2f>();
                    List<Point2f> SortedPointList = new List<Point2f>();
                    List<Point2f> ThirdPointList = new List<Point2f>();
                    YSortedPointList = _pointsList.OrderBy(o => o.Y).ToList();

                    for (int i = 0; i < YSortedPointList.Count - 1; i++)
                    {
                        if (Math.Abs(YSortedPointList[i].Y - YSortedPointList[i + 1].Y) < LineSpacing)
                        {
                            RowPointList.Add(YSortedPointList[i]);
                            if (YSortedPointList.Count - 2 == i)
                            {
                                RowPointList.Add(YSortedPointList[i + 1]);
                                RowPointList = RowPointList.OrderBy(o => o.X).ToList();
                                SortedPointList = SortedPointList.Concat(RowPointList).ToList();
                                RowPointList.Clear();
                            }
                        }
                        else
                        {
                            line++;
                            if (0 == i)
                            {
                                SortedPointList.Add(YSortedPointList[i]);
                                continue;
                            }
                            RowPointList.Add(YSortedPointList[i]);
                            RowPointList = RowPointList.OrderBy(o => o.X).ToList();
                            SortedPointList = SortedPointList.Concat(RowPointList).ToList();
                            if (line == 3)
                            {
                                ThirdPointList = ThirdPointList.Concat(RowPointList).ToList();
                            }
                            RowPointList.Clear();
                            if (YSortedPointList.Count - 2 == i)
                            {
                                SortedPointList.Add(YSortedPointList[i + 1]);
                            }
                        }
                    }

                    foreach (var point in SortedPointList)
                    {
                        m_TextPointSorted.Add(point);
                    }

                    //按照排序后在字典中取出字符
                    foreach (var item in m_TextPointSorted)
                    {
                        Console.Write(m_TextDic[item]);
                        _ocrResult += m_TextDic[item];
                    }


                    foreach (var point in ThirdPointList)
                    {
                        m_Line3PointSorted.Add(point);
                    }

                    foreach (var item in m_Line3PointSorted)
                    {
                        Console.Write(m_TextDic[item]);
                        _Line3Result += m_TextDic[item];
                    }

                    _ocrResult = _ocrResult.Insert(_ocrResult.Length - 2, " ");

                    return ERROR_OK;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }

            /// <summary>
            /// 输出Bitmap格式的渲染图
            /// </summary>
            /// <param name="mat"></param>
            /// <returns></returns>
            public static Bitmap VisualizeBitmap(Mat mat)
            {
                for (int i = 0; i < ListPoints.Count(); i++)
                {
                    Cv2.PutText(mat, TextList[i], ListPoints[i][1], HersheyFonts.HersheyComplex, 1, Scalar.Green, 2, LineTypes.Link8);
                    Cv2.Rectangle(mat, ListPoints[i][1], ListPoints[i][3], Scalar.Red);
                }
                Bitmap bitmap = new Bitmap(mat.Cols, mat.Rows, (int)mat.Step(), PixelFormat.Format24bppRgb, mat.Data);
                return bitmap;
            }


            /// <summary>
            /// 输出Mat格式的渲染图
            /// </summary>
            /// <param name="mat"></param>
            /// <returns></returns>
            public static Mat VisualizeMat(Mat mat, Scalar _color,string _ocrResult)
            {
                Mat laMat = mat.Clone();
                //if (laMat.Channels() == 1) Cv2.CvtColor(laMat, laMat, ColorConversionCodes.GRAY2RGB);
    
                //OpenCvSharp.Point point = new OpenCvSharp.Point(100,100);
                //Cv2.PutText(laMat, _ocrResult, point, HersheyFonts.HersheyComplex,3, _color, 2, LineTypes.Link8);
                return laMat;
            }

            /// <summary>
            /// 字符串坐标转成OpenCvSharp.Point格式
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static OpenCvSharp.Point GetPoint(string str)
            {
                IList<string> numbericList = new List<string>();
                MatchCollection ms = Regex.Matches(str, @"\d+");
                foreach (Match m in ms)
                {
                    numbericList.Add(m.Value);
                }
                OpenCvSharp.Point point = new OpenCvSharp.Point(Convert.ToInt32(numbericList[0]), Convert.ToInt32(numbericList[1]));
                return point;
            }
        }

        /// <summary>
        /// 分割算法
        /// </summary>
        public static class ISegmentationResponse
        {

        }
    }
}
