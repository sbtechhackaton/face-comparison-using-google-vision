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
                Console.WriteLine(string.Concat(key, ": ", perc, "%"));
                blqblq.Add(perc);
            }
            var sumPerc = blqblq.Sum() / blqblq.Count;
            Console.WriteLine(string.Concat(sumPerc, "%"));
            return sumPerc;
        }
    }
}
