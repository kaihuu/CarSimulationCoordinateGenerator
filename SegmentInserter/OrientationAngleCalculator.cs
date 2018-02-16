using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;

namespace CarSimulatorCoodinate2
{

        static class OrientationAngleCalculator
        {
            public static double CalcOrientationAngle(GeoCoordinate startCoordinate, GeoCoordinate endCoordinate)
            {
                double startLattitude = MathUtil.ConvertDegreeToRadian(startCoordinate.Latitude);
                double startLongitude = MathUtil.ConvertDegreeToRadian(startCoordinate.Longitude);
                double endLattitude = MathUtil.ConvertDegreeToRadian(endCoordinate.Latitude);
                double endLongitude = MathUtil.ConvertDegreeToRadian(endCoordinate.Longitude);

                double x = Math.Cos(endLattitude) * Math.Sin(endLongitude - startLongitude);
                double y = Math.Cos(startLattitude) * Math.Sin(endLattitude) - Math.Sin(startLattitude) * Math.Cos(endLongitude - startLongitude) * Math.Cos(endLattitude);
                double heading = MathUtil.ConvertRadianToDegree(Math.Atan2(x, y));
                return heading;
            }
        }
    
}
