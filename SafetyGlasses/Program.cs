﻿using System;
using System.IO;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;

namespace SafetyGlasses
{
    class Program
    {

        private static MemoryStream testImage;

        static void Main(string[] args)
        {

            string localPath = @"C:\Projects\SafetyGlasses\SafetyGlasses\SafetyGlasses\Images\";
            string endPoint = "https://southcentralus.api.cognitive.microsoft.com/";
            Guid projectId = new Guid("");
            string predictionKey = "";
            string trainingKey = "";

            CustomVisionTrainingClient trainingApi = new CustomVisionTrainingClient()
            {
                ApiKey = trainingKey,
                Endpoint = endPoint
            };

            var project = trainingApi.GetProject(projectId);
            Console.WriteLine($"Found project: {project.Description}");

            CustomVisionPredictionClient endpoint = new CustomVisionPredictionClient()
            {
                ApiKey = predictionKey,
                Endpoint = endPoint
            };

            Console.WriteLine("Loading image...");
            testImage = new MemoryStream(File.ReadAllBytes($"{localPath}agx.jpg"));

            Console.WriteLine("Making a prediction:");
            var result = endpoint.PredictImage(project.Id, testImage);

            foreach (var c in result.Predictions)
            {
                Console.WriteLine($"\t{c.TagName}: {c.Probability:P1}");
            }

            
            Console.ReadKey();
        }

    }
}
