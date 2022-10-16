using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTGTest
{
    internal class Data
    {
        public Data(float altitude, float ias, float vs)
        {
            this.altitude = altitude;
            this.ias = ias;
            this.vs = vs;
        }

        public float altitude { get; set; }
        public float ias { get; set; }
        public float vs { get; set; }
    }
}
