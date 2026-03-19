//Scoring logic for the environment checkusing System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project.SecretDetection.SecretsAnalysis;

namespace Project.SecretDetection.EnvironmentChecking{
    class EnvScorer //Abstract class til scoring så det kan udvides senere?
    {
        //Class does analysis on the secrets found in the envChecker, and based on the fact that the potential secret is from the env file,
        //are given a score of how critical the "secret" is based on this fact.

        //run different analysis on the strings that are found
        //get score from analysis
        //sum up the different scores based on the fact its in the env

        public void giveScore(List<EnvChecker.EnvironmentVariable> environmentVariables)
        {
            //The different analysis methods to be done on the environmentVariables
            var entropyDetector = new EntropyDetector();
            var hexDetector = new HexDetector();
            var base64Detector = new Base64Detector(); 

                for (int i = 0; i < environmentVariables.Count; i++)
                {
                    
                    int entVal = entropyDetector.detect(environmentVariables[i].secret); //If entVal = 1 har stringen en entropi på over 3, hvilket er en bound JEG har sat, men kan ændres
                    int hexVal = hexDetector.detect(environmentVariables[i].secret);
                    int base64 = base64Detector.detect(environmentVariables[i].secret);
                    Console.WriteLine("Entropy scoring for {0}: {1}", environmentVariables[i].secret, entVal);
                    Console.WriteLine("Hex scoring for {0}: {1}", environmentVariables[i].secret, hexVal);
                    Console.WriteLine("Base64 scoring for {0}: {1}", environmentVariables[i].secret, base64);

                    int tempScore = entVal + hexVal + base64;

                    if (tempScore > 1)
                    {
                        var temp = environmentVariables[i];
                        temp.score = tempScore;
                        environmentVariables[i] = temp;
                    }
                    //Ellers er scoren initialiseret til at være 0, som vi beholder i dette tilfælde

                    Console.WriteLine("Final score for this secret: " + environmentVariables[i].score);
                    Console.WriteLine("");
                }




        //     foreach(var envVar in environmentVariables) //analyse af hver string
        //     {
        //         int entVal = entropyDetector.detect(envVar.secret); //If entVal = 1 har stringen en entropi på over 3, hvilket er en bound JEG har sat, men kan ændres
        //         int hexVal = hexDetector.detect(envVar.secret);
        //         int base64 = base64Detector.detect(envVar.secret);
        //         Console.WriteLine("Entropy scoring for {0}: {1}", envVar.secret, entVal);
        //         Console.WriteLine("Hex scoring for {0}: {1}", envVar.secret, hexVal);
        //         Console.WriteLine("Base64 scoring for {0}: {1}", envVar.secret, base64);

        //         int tempScore = entVal + hexVal + base64;

        //         if (tempScore > 1)
        //         {
        //             envVar.score = tempScore;
        //         }

        //         //Does it look like hex and base64? - up the sum
        //         //Does it look like a word? - dowm the sum
        //         //What is the length of the secret? - not high - sum down
        //         //Does it look like a specific API key? - up the sum by a LOT
        //         //Does it look like a key volt? - down the sum
                
        //     }
        }

    }
}