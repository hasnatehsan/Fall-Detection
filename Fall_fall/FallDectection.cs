using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fall_fall
{
    public class FallDectection
    {
        public double Fall(dynamic values)
        {
            var ax = values[0];
            var ay = values[1];
            var az = values[2];

            var TotalAcc = Math.Sqrt(Math.Pow(ax, 2) + Math.Pow(ay, 2) + Math.Pow(az, 2));
            var AccMinimise = TotalAcc / 9.8;

            return AccMinimise;

        }
    }
}
