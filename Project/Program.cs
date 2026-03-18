using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project.SecretDetection;

namespace Project{
    class Program
    {
        public static void Main(String[] args)
        {
            string filePath = @"C:\Users\natd\OneDrive - Netcompany\Desktop\test\WeatherSimple";
            var secretDetector = new SecretDetector();
            secretDetector.Detect(filePath);
        }
    }
}