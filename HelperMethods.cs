using Google.Apis.Vision.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudSamples
{
    public static class HelperMethods
    {
        public static double CalculateDistance(Position p1, Position p2)
        {
            var deltaX = Math.Pow((double)(p2.X - p2.X), 2);
            var deltaY = Math.Pow((double)(p2.Y - p2.Y), 2);
            var distance = Math.Sqrt(deltaX + deltaY);
            return distance;
        }
    }
}
