using AlgoControlLibrary.AlgoBaseFactory;
using OpenCvSharp;
using SmartMore.ViMo;
using SMLogControlLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoControlLibrary.VimoAlgo
{
    public class VimoSegmentManager:VimoManager
    {
        IAlgo algo=null;
        public VimoSegmentManager() {

            algo = new VimoFactory().CreateVimo();
        }


        #region override
        public override EnumReturnVal Init(AlgoInitInput algoinput)
        {
            try
            {
                return algo.Init(algoinput);               
            }
            catch (Exception ex)
            {
                SMLogWindow.OutLog($"{ex.ToString()}", Color.Green);
                return EnumReturnVal.Return_Fail;
            }
        }

        public override EnumReturnVal Run(AlgoRunInput RunInput, out AlgoRunOutput rsps)
        {
            //SMLogWindow.OutLog($"init:start", Color.Green);
            AlgoRunOutput _rsps = new AlgoRunOutput();
            try
            {
               if(EnumReturnVal.Return_OK== algo.Run(RunInput, out ResponseList<SegmentationResponse> algorunOutput))
                {
                    _rsps.mask = RunInput.SourceImg.Clone();
                    ConvertImageData(algorunOutput, RunInput, _rsps);
                }

                rsps = _rsps;
                return EnumReturnVal.Return_OK;
            }
            catch (Exception ex)
            {
                rsps = null;
                return EnumReturnVal.Return_Fail;
            }
        }

        public override EnumReturnVal Free()
        {
            try
            {
                algo.Free();
                return EnumReturnVal.Return_OK;
            }
            catch
            {

                return EnumReturnVal.Return_Fail;
            }
        }
        #endregion


        #region Method

        private void ConvertImageData(ResponseList<SegmentationResponse> rsp, AlgoRunInput RunInput, AlgoRunOutput Runoutput)
        {
            if (rsp == null)
                return;

            foreach (var item in rsp)
            {
                foreach (var kv in item.Item2.LabelMap)
                {
                    using (var labels = new Mat())
                    using (var stats = new Mat())
                    using (var centroids = new Mat())
                    {
                        var count = Cv2.ConnectedComponentsWithStats(item.Item2.Mask.Equals(kv.Value), labels, stats, centroids);
                        for (int i = 1; i < count; i++) // 0 is background, ignore
                        {
                            var area = stats.At<int>(i, (int)ConnectedComponentsTypes.Area);

                            if (!RunInput.DicDefect.ContainsKey(kv.Key)) {

                                SMLogWindow.OutLog($"key:{kv.Key}:不存在",Color.Green,loglevel: SMLogControlLibrary.LogLevel.Error);
                                continue;
                            }

                            if ((RunInput.DicDefect[kv.Key].Minval <= area) && (area <= RunInput.DicDefect[kv.Key].Maxval)) //面积过滤
                            {
                                //if (!result.ContainsKey(ModuleType.Segmentation))
                                //result[ModuleType.Segmentation] = new Data();
                                //result[ModuleType.Segmentation].dataDetails.Add(new SegDataDetail() { area = area, label = kv.Key });
                                //defectList.Add($"{kv.Key}area: {area}\r\n")
                                ArrayList arrlistDic = new ArrayList();
                                if (!Runoutput.Dicdefect.ContainsKey(kv.Key))
                                {

                                    arrlistDic.Add(area);
                                    Runoutput.Dicdefect.Add(kv.Key, arrlistDic); 
                                }
                                else
                                {
                                    arrlistDic = Runoutput.Dicdefect[kv.Key];
                                    arrlistDic.Add(area);
                                    Runoutput.Dicdefect[kv.Key] = arrlistDic;
                                }

                                if (RunInput.ShowMask)
                                {
                                    Cv2.FindContours(labels.Equals(i), out var contours, out var hierarcy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
                                    var color = new Scalar(0, 0, 255);
                                    Cv2.DrawContours(Runoutput.mask, contours, -1, color, 2);

                                    ArrayList arrlist = new ArrayList();
                                    foreach (var ctr in contours) // print area to image
                                    {

                                        double dlen = Cv2.ArcLength(ctr, true);

                                        Debug.WriteLine($"dlen:{dlen}");

                                        //Rect bbox = Cv2.BoundingRect(ctr);
                                        var minRect = Cv2.MinAreaRect(ctr);

                                        arrlist.Add(minRect);

                                        //Debug.WriteLine($"dBoundinglen:{Cv2.ArcLength(minRect.Points(), true)}");
                                        //Debug.WriteLine($"dBoundingWidth:{minRect.Size.Width}");
                                        //Debug.WriteLine($"dBoundingHeight:{minRect.Size.Height}");

                                        //Cv2.DrawContours(handleimage, arrminRect.Select(x => x.Points().Select(x => (Point)x)), -1, color, 2);

                                        //Cv2.PutText(handleimage, $"{kv.Key}, area = {area}", new OpenCvSharp.Point(bbox.X, bbox.Y), HersheyFonts.HersheySimplex, 1, color, 2);
                                        //Cv2.PutText(handleimage, $"{kv.Key},area={area}", new OpenCvSharp.Point(bbox.X, bbox.Y), HersheyFonts.HersheySimplex, 1, color, 2);
                                        Cv2.PutText(Runoutput.mask, $"{kv.Key},area={area}", minRect.BoundingRect().TopLeft, HersheyFonts.HersheySimplex, 1, color, 2);
                                    }

                                    Runoutput.mask.DrawContours((arrlist.ToArray()).Select(x => ((RotatedRect)x).Points().Select(x1 => (OpenCvSharp.Point)x1)), -1, new OpenCvSharp.Scalar(0, 255, 255), 2);

                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

    }
}
