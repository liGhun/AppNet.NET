using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model.Annotations
{
    public class GeoLocation
    {
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public decimal altitude { get; set; }
        public decimal horizontal_accuracy { get; set; }
        public decimal vertical_accuracy { get; set; }
    }
}
