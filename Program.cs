﻿/*
 * Copyright (c) 2015 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */
// [START all]
// [START import_libraries]

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Vision.v1;
using Google.Apis.Vision.v1.Data;
using GoogleCloudSamples.FacesComparers;
using System;
using System.Collections.Generic;
using System.Linq;

// [END import_libraries]

namespace GoogleCloudSamples
{
    public class LabelDetectionSample
    {
        const string usage = @"Usage:LabelDetectionSample <path_to_image>";
        // [START authenticate]
        /// <summary>
        /// Creates an authorized Cloud Vision client service using Application 
        /// Default Credentials.
        /// </summary>
        /// <returns>an authorized Cloud Vision client.</returns>
        public VisionService CreateAuthorizedClient()
        {
            GoogleCredential credential =
                GoogleCredential.GetApplicationDefaultAsync().Result;
            // Inject the Cloud Vision scopes
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    VisionService.Scope.CloudPlatform
                });
            }
            return new VisionService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                GZipEnabled = false
            });
        }
        // [END authenticate]

        // [START detect_labels]
        /// <summary>
        /// Detect labels for an image using the Cloud Vision API.
        /// </summary>
        /// <param name="vision">an authorized Cloud Vision client.</param>
        /// <param name="imagePath">the path where the image is stored.</param>
        /// <returns>a list of labels detected by the Vision API for the image.
        /// </returns>
        public IList<AnnotateImageResponse> DetectLabels(
            VisionService vision, string imagePath)
        {
            Console.WriteLine("Detecting Labels...");
            // Convert image to Base64 encoded for JSON ASCII text based request   
            byte[] imageArray = System.IO.File.ReadAllBytes(imagePath);
            string imageContent = Convert.ToBase64String(imageArray);
            // Post label detection request to the Vision API
            // [START construct_request]
            var responses = vision.Images.Annotate(
                new BatchAnnotateImagesRequest()
                {
                    Requests = new[] {
                    new AnnotateImageRequest() {
                        Features = new [] { new Feature() { Type =
                          "FACE_DETECTION"}},
                        Image = new Image() { Content = imageContent }
                    }
               }
                }).Execute();
            return responses.Responses;
            // [END construct_request]
        }
        // [END detect_labels]

        // [START run_application]
        private static void Main(string[] args)
        {
            LabelDetectionSample sample = new LabelDetectionSample();
            //if (args.Length == 0)
            //{
            //    Console.WriteLine(usage);
            //    return;
            //}
<<<<<<< HEAD
            string faceOne = @"..\..\bin\Debug\antoan1.jpg";
            string faceTwo = @"..\..\bin\Debug\antoan1.jpg";
=======
            imagePath =  @"../../imgs/jolie.jpg";
>>>>>>> 1ca43e5da540fcc59ba41eb9cf9e1b26da6e43b3
            // Create a new Cloud Vision client authorized via Application 
            // Default Credentials
            VisionService vision = sample.CreateAuthorizedClient();
            // Use the client to get label annotations for the given image
            // [START parse_response]
            IList<AnnotateImageResponse> faceOneResult = sample.DetectLabels(
                vision, faceOne);
            IList<AnnotateImageResponse> faceTwoResult = sample.DetectLabels(
                vision, faceTwo);
            
            var firstFaceData = new FaceData(faceOneResult.FirstOrDefault().FaceAnnotations.FirstOrDefault().Landmarks);
            var secondFaceData = new FaceData(faceTwoResult.FirstOrDefault().FaceAnnotations.FirstOrDefault().Landmarks);

            FacePointComparer.Compare(firstFaceData, secondFaceData);

            // Check if label annotations were found
            if (faceOneResult != null)
            {
                Console.WriteLine("Labels for image: " + faceOneResult);
                // Loop through and output label annotations for the image
                foreach (var response in faceOneResult)
                {
                    foreach (var facial in response.FaceAnnotations)
                    {
                    }
                }
            }
            else
            {
                Console.WriteLine("No labels found.");
            }
            // [END parse_response]
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
        // [END run_application]
    }
}
// [END all]
