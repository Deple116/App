using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoreVision.FunctionClass
{

    public class Rootobject
    {
        public string[] defect { get; set; }
        public object[] convex_circle { get; set; }
        public object[] defect_chars { get; set; }
        public object[] oil_hole_chars { get; set; }
        public Normal_Chars[] normal_chars { get; set; }
        public Oil_Holes[] oil_holes { get; set; }
        public bool is_ok { get; set; }
        public string infer_state { get; set; }
    }

    public class Normal_Chars
    {
        public string label { get; set; }
        public Rect rect { get; set; }
        public int[] rect_center { get; set; }
    }

    public class Rect
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class Oil_Holes
    {
        public string label { get; set; }
        public Rect1 rect { get; set; }
        public int[] rect_center { get; set; }
    }

    public class Rect1
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }


}
