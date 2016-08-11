using Google.Apis.Vision.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudSamples.FacesComparers
{
    public class FaceData
    {
        private float baseDistance { get; set; }
        private List<string> baseLandmarkTypes { get; set; }
        private List<Landmark> baseLandmarks { get; set; }

        public Dictionary<string, double> distanceRatios { get; set; }

        public FaceData(IList<Landmark> landmarks)
        {
            distanceRatios = new Dictionary<string, double>();
            baseLandmarkTypes = new List<string>() { "LEFT_EAR_TRAGION", "RIGHT_EAR_TRAGION", "LEFT_EYEBROW_UPPER_MIDPOINT", "RIGHT_EYEBROW_UPPER_MIDPOINT", "CHIN_GNATHION" };
            baseLandmarks = ParseBaseFaceLandmarks(landmarks);
            baseDistance = CalculateBaseDistance(landmarks);

            CalculateDistanceRatios(landmarks);
        }

        private List<Landmark> ParseBaseFaceLandmarks(IList<Landmark> landmarks)
        {
            return landmarks.Where(l => baseLandmarkTypes.Contains(l.Type)).ToList();
        }

        private float CalculateBaseDistance(IList<Landmark> landmarks)
        {
            var leftEarPosition = landmarks.FirstOrDefault(l => l.Type.Equals("LEFT_EAR_TRAGION"));
            var rightEarPosition = landmarks.FirstOrDefault(l => l.Type.Equals("RIGHT_EAR_TRAGION"));

            if (!rightEarPosition.Position.X.HasValue || !leftEarPosition.Position.X.HasValue)
            {
                throw new ArgumentException();
            }

            return rightEarPosition.Position.X.Value - leftEarPosition.Position.X.Value;
        }

        private void CalculateDistanceRatios(IList<Landmark> landmarks)
        {
            foreach (var landmark in landmarks)
            {
                foreach (var baseLandmark in baseLandmarks)
                {
                    if(landmark.Type.Equals(baseLandmark.Type))
                    {
                        continue;
                    }

                    if(distanceRatios.ContainsKey(string.Concat(baseLandmark.Type, landmark.Type)))
                    {
                        continue;
                    }

                    distanceRatios.Add(string.Concat(landmark.Type, baseLandmark.Type), CalculatePointsDistanceRatio(landmark.Position, baseLandmark.Position));
                }
            }
        }

        private double CalculatePointsDistanceRatio(Position pointOne, Position pointTwo)
        {
            var deltaX = Math.Pow((double)(pointOne.X - pointTwo.X), 2);
            var deltaY = Math.Pow((double)(pointTwo.Y - pointOne.Y), 2);
            return Math.Sqrt(deltaX + deltaY) / baseDistance;
        }
    }
}
