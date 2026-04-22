//Scoring logic for the environment checkusing System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project.SecretDetection.SecretsAnalysis;
using Project.SecretDetection.PlaceAnalysis;

namespace Project.SecretDetection.EnvironmentChecking{
    class EnvScorer //Abstract class til scoring så det kan udvides senere?
    {

        //Class does analysis on the secrets found in the envChecker, and based on the fact that the potential secret is from the env file,
        //are given a score of how critical the "secret" is based on this fact.

        //run different analysis on the strings that are found
        //get score from analysis
        //sum up the different scores based on the fact its in the env
        public void getScore()
        {
            //Lav en funktion der kalder giveScore og giveWeight. -- DET HER SKAL VÆRE EN ABSTRACT FUNKTION I ABSTRACT CLASS
        }



        //For secret analysis
        public void giveScore(List<EnvChecker.EnvironmentVariable> environmentVariables, List<SyntaxTree> trees)
        {
            int secretLengthBoud = 15; //its weird if a string is longer than this

            //The different secret analysis methods to be done on the environmentVariables
            var entropyDetector = new EntropyDetector();
            var hexDetector = new HexDetector();
            var base64Detector = new Base64Detector(); 
            var apiKeyDetector = new APIKeyDetector();

            for (int i = 0; i < environmentVariables.Count; i++)
            {
                //Does it look like API?
                    //Return kritical score
                //Does it look like pw? --- MANGLER IMPLEMENTATION AF CHECK
                    //Return Critical score
                //Does it look like a word? --- MANGLER IMPLEMENTATION AF CHECK
                    //Return without score
                //Does it look like base64?
                    //Return medium score
                //Does it look like Hex?
                    //Does it have high entropy?
                        //Return mild score
                    //Return no score
                //Does it have high entropy?
                    //Does the length look sus?
                        //Return mild score
                    //Return no score
                //Return no score

                float entVal = entropyDetector.detect(environmentVariables[i].secret); //If entVal = 1 har stringen en entropi på over 3, hvilket er en bound JEG har sat, men kan ændres
                float hexVal = hexDetector.detect(environmentVariables[i].secret);
                float base64Val = base64Detector.detect(environmentVariables[i].secret);
                float apiVal = apiKeyDetector.detect(environmentVariables[i].secret);

                if(apiVal > 0) //critical
                {
                    var temp = environmentVariables[i];
                    temp.score +=apiKeyDetector.score;
                    temp.comment = temp.comment + "This string looks like " + apiKeyDetector.apiType +". " ;
                    environmentVariables[i] = temp; 
                }
                // if(secret looks like a password) //critical
                //     {
                //         var temp = environmentVariables[i];
                //         temp.score +=100000;
                //         environmentVariables[i] = temp; 
                //     } //her skal p4ssword i .env filen eks kunne findes
                // if (secret looks like a word && not password) //no detection needed
                //     {
                //          return without a detection og slut her.
                //     }
                if (base64Val > 0) //medium detection
                {
                    var temp = environmentVariables[i];
                    temp.score += base64Detector.score;
                    temp.comment = temp.comment + "This string looks like it is base64 encoded. ";
                    environmentVariables[i] = temp; 
                }
                if (hexVal > 0) //&& Does not look like a word
                {
                    if (entVal > 0) // medium detection
                    {
                        var temp = environmentVariables[i];
                        temp.score += hexDetector.score;
                        temp.comment = temp.comment + "This string looks like hex and has high entropy. ";
                        environmentVariables[i] = temp; 
                    }
                    //return with no detection
                }
                else if(entVal > 0)
                {
                    if (environmentVariables[i].secret.Length > secretLengthBoud) //mild detection
                                                                //Ryk evt denne bound ind i entropy detector klassen (det er dens ansvar at give en score)
                    {
                        var temp = environmentVariables[i];
                        temp.score += entropyDetector.score;
                        temp.comment = temp.comment + "This string is long and has high entropy. ";
                        environmentVariables[i] = temp;
                    }
                }
            }
        }
        //For dataflow analysis
        public void giveWeight(List<EnvChecker.EnvironmentVariable> environmentVariables, List<SyntaxTree> trees)
        {
            //place detectors
            var httpDetector = new HttpDetector();
            
            for (int i = 0; i < environmentVariables.Count; i++)
            {
                //Det giver ikke mening at lave dataflow analyse på en ubrugt env variabel, og hvis vi ganger en weight med 0 er no go
                if (environmentVariables[i].used)
                    {
                        var temp = environmentVariables[i];
                        float weight = httpDetector.getWeight(trees, environmentVariables[i].name);
                        Console.WriteLine("The weight for the secret " + environmentVariables[i].name + ":"+ weight);
                        temp.score = temp.score * weight;
                        // temp.comment = temp.comment + "This string is being used in http. ";
                        environmentVariables[i] = temp;
                    }
                
                Console.WriteLine("Name: {0}. Final score for secret {1}: {2}", environmentVariables[i].name, environmentVariables[i].secret, environmentVariables[i].score);
                Console.WriteLine("Comments on detection: {0}", environmentVariables[i].comment);

            }
        }
    }
}