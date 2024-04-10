using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoreVision.FunctionClass
{
    public class InputRootobject
    {
        public string product_string { get; set; }
        public string product_type { get; set; }
        public int num_oil_holes { get; set; }
        public bool only_check_normal_char { get; set; }
        public float[] convex_radius_scaling_ratio { get; set; }
    }
}
