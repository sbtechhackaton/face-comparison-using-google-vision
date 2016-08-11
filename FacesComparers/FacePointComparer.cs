using Google.Apis.Vision.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudSamples.FacesComparers
{
    public static class FacePointComparer
    {
        public static double Compare(FaceData beautifulFace, FaceData antoansFace)
        {
            var blqblq = new List<double>();
            foreach (var key in beautifulFace.distanceRatios.Keys)
            {
                if (!antoansFace.distanceRatios.ContainsKey(key))
                {
                    return 0D;
                }

                var perc = Math.Min(beautifulFace.distanceRatios[key], antoansFace.distanceRatios[key]) * 100 / Math.Max(beautifulFace.distanceRatios[key], antoansFace.distanceRatios[key]);

                ApplyPercNTimes(blqblq, perc);
            }

            var sumPerc = blqblq.Sum() / blqblq.Count;

            if (sumPerc < 85 && sumPerc > 80)
            {
                sumPerc = sumPerc - 5;
            }
            else if (sumPerc < 80 && sumPerc > 60)
            {
                sumPerc = sumPerc - 30;
            }
            else if (sumPerc < 60)
            {
                sumPerc = sumPerc - 40;
            }

            Console.WriteLine("The two faces compared to each other: " + string.Concat(sumPerc, "% alike"));
            return sumPerc;
        }

        private static void ApplyPercNTimes(List<double> blqblq, double perc)
        {
            if (perc > 90)
            {
                blqblq.Add(perc);

                return;
            }

            var times = Math.Pow(100 - (int)perc, 3);

            for (var i = 0; i < times; i += 1)
            {
                blqblq.Add(perc);
            }
        }
    }
}
