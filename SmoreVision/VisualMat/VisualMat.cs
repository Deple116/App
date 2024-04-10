using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoreVision.VisualMat
{
    public class VisualResponse
    {
        public Mat mat;//原图
        public Mat mask_ori;//背景为0的所有缺陷
        public Mat mask;//输出图
        public Dictionary<string, int> LabelMap { get; set; }//所有缺陷名称和阈值
    }

    public class VisualJsonResponse
    {
        public Mat mat;//原图
        public Mat mask;//输出图
        public string res_json;//json文本
    }


    class VisualMat
    {
        //绘制
        public static void VisualizeSegmentationResult(VisualResponse rsp)
        {
            try
            {
                if (rsp.mat.Channels() == 1) Cv2.CvtColor(rsp.mat, rsp.mat, ColorConversionCodes.GRAY2BGR);

                foreach (var kv in rsp.LabelMap)
                {
                    using (Mat labels = new Mat())
                    using (Mat stats = new Mat())
                    using (Mat centroids = new Mat())
                    {
                        var count = Cv2.ConnectedComponentsWithStats(rsp.mask_ori.Equals(kv.Value), labels, stats, centroids);

                        for (int i = 1; i < count; i++) // 0 is background, ignore
                        {
                            var area = stats.At<int>(i, (int)ConnectedComponentsTypes.Area);
                            Cv2.FindContours(labels.Equals(i).ToMat(), out OpenCvSharp.Point[][] contours, out HierarchyIndex[] hierarcy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

                            var color = new Scalar(0, 0, 255);

                            //if (bAI)
                            //{
                            //    for (int j = 0; j < contours.Length; j++)
                            //    {
                            //        for (int t = 0; t < contours[j].Length; t++)
                            //        {
                            //            contours[j][t].X = (int)(contours[j][t].X * 2) + 600;
                            //            contours[j][t].Y = (int)(contours[j][t].Y * 2);
                            //        }

                            //    }
                            //}

                            double left = stats.At<int>(i, (int)ConnectedComponentsTypes.Left) * 2 + 600; //连通域的boundingbox的最左边
                            double top = stats.At<int>(i, (int)ConnectedComponentsTypes.Top) * 2;//连通域的boundingbox的最上边


                            Cv2.DrawContours(rsp.mat, contours, -1, color, 2);

                            foreach (var ctr in contours) // print area to image
                            {
                                var bbox = Cv2.BoundingRect(ctr);
                                Cv2.PutText(rsp.mat, $"{kv.Key}, area = {123}", new OpenCvSharp.Point(bbox.X, bbox.Y), HersheyFonts.HersheySimplex, 1, color, 2);

                                //Cv2.PutText(image, $"{kv.Key}, area = {area}", new Point(100, 100), HersheyFonts.HersheySimplex, 1, color, 2);
                            }
                        }




                    }
                }


            }
            catch (Exception ex)
            {

            }
        }


        //Json绘制
        //public static void VisualizeJsonResult(VisualJsonResponse rsp)
        //{
        //    try
        //    {
        //        Rootobject root = JsonConvert.DeserializeObject<Rootobject>(rsp.res_json);

        //        int ilen = root.convex_circle.Length;
        //        int cx = (int)((JArray)root.convex_circle[0])[0];
        //        int cy = (int)((JArray)root.convex_circle[0])[1];
        //        double cr1 = (double)(root.convex_circle[1]);
        //        double cr2 = (double)(root.convex_circle[2]);



        //        Mat temp = rsp.mat.Clone();
        //        Cv2.Circle(temp, cx, cy, (int)cr1, Scalar.Red, 3);
        //        Cv2.Circle(temp, cx, cy, (int)cr2, Scalar.Purple, 3);

        //        Cv2.ImWrite("20230322_1.bmp", temp.Clone());

        //        ilen = root.oil_holes.Length;
        //        for (int i = 0; i < ilen; i++)
        //        {

        //            Cv2.Rectangle(temp, new OpenCvSharp.Rect((int)root.oil_holes[i].rect.x, (int)root.oil_holes[i].rect.y,
        //                (int)root.oil_holes[i].rect.w, (int)root.oil_holes[i].rect.h), Scalar.Green, 1);
        //            Cv2.PutText(temp, root.oil_holes[i].label, new OpenCvSharp.Point((int)root.oil_holes[i].rect.x, (int)root.oil_holes[i].rect.y),
        //                HersheyFonts.HersheySimplex, 1, Scalar.Red, 3);

        //        }
        //        Cv2.ImWrite("20230322_2.bmp", temp.Clone());

        //        ilen = root.normal_chars.Length;
        //        for (int i = 0; i < ilen; i++)
        //        {
        //            Cv2.Rectangle(temp, new OpenCvSharp.Rect((int)root.normal_chars[i].rect.x, (int)root.normal_chars[i].rect.y,
        //                (int)root.normal_chars[i].rect.w, (int)root.normal_chars[i].rect.h), Scalar.Green, 1);
        //            Cv2.PutText(temp, root.normal_chars[i].label, new OpenCvSharp.Point((int)root.normal_chars[i].rect.x, (int)root.normal_chars[i].rect.y),
        //                HersheyFonts.HersheySimplex, 1, Scalar.Red, 3);

        //        }
        //        Cv2.ImWrite("20230322_3.bmp", temp.Clone());
        //        rsp.mask = temp.Clone();

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }

   
}
