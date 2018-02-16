using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarSimulatorCoodinate2
{
    class MathUtil
    {

        public static double ConvertDegreeToRadian(double degree)
        {
            return degree * Math.PI / 180;
        }
        public static double ConvertRadianToDegree(double radian)
        {
            return radian * 180 / Math.PI;
        }
    }
}
