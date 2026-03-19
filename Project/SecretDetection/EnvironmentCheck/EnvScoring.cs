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
            var apiKeyDetector = new APIKeyDetector();
            int secretLengthBoud = 15; //its weird if a string is longer than this

                for (int i = 0; i < environmentVariables.Count; i++)
                {
                    
                    int entVal = entropyDetector.detect(environmentVariables[i].secret); //If entVal = 1 har stringen en entropi på over 3, hvilket er en bound JEG har sat, men kan ændres
                    int hexVal = hexDetector.detect(environmentVariables[i].secret);
                    int base64Val = base64Detector.detect(environmentVariables[i].secret);
                    int apiVal = apiKeyDetector.detect(environmentVariables[i].secret);
                    // Console.WriteLine("Entropy scoring for {0}: {1}", environmentVariables[i].secret, entVal);
                    // Console.WriteLine("Hex scoring for {0}: {1}", environmentVariables[i].secret, hexVal);
                    // Console.WriteLine("Base64 scoring for {0}: {1}", environmentVariables[i].secret, base64Val);
                    // Console.WriteLine("API Key scoring for {0}: {1}", environmentVariables[i].secret, apiVal);


                    //Does it look like hex and base64? - up the sum
                    //Does it look like a word? - dowm the sum -- genanvendte passwords så som "password" "p4ssword", 
                        //Black list, compromised passwords
                    //What is the length of the secret? - not high - sum down
                    //Does it look like a specific API key? - up the sum by a LOT
                    //Does it look like a key volt? - down the sum
                    //IF looks like hex && ! looks like word = score +++++++

                    
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

                    if(apiVal == 1) //critical
                    {
                        var temp = environmentVariables[i];
                        temp.score +=100000;
                        temp.comment = temp.comment + "This string looks like " + apiKeyDetector.apiType +". " ;
                        environmentVariables[i] = temp; 
                    }
                    // if(secret looks like a password) //critical
                    //     {
                    //         var temp = environmentVariables[i];
                    //         temp.score +=100000;
                    //         environmentVariables[i] = temp; 
                    //     } //her skal p4ssword i .env filen eks kunne findes
                    // if (secret looks like a word) //no detection needed
                    //     {
                    //          return without a detection og slut her.
                    //     }
                    if (base64Val == 1) //medium detection
                    {
                        var temp = environmentVariables[i];
                        temp.score +=5000;
                        temp.comment = temp.comment + "This string looks like it is base64 encoded. ";
                        environmentVariables[i] = temp; 
                    }
                    if (hexVal == 1) //&& Does not look like a word
                    {
                        if (entVal == 1) // medium detection
                        {
                            var temp = environmentVariables[i];
                            temp.score +=5000;
                            temp.comment = temp.comment + "This string looks like hex and has high entropy. ";
                            environmentVariables[i] = temp; 
                        }
                        //return with no detection
                    }
                    if(entVal == 1)
                    {
                        if (environmentVariables[i].secret.Length > secretLengthBoud) //mild detection
                        {
                            var temp = environmentVariables[i];
                            temp.score +=20;
                            temp.comment = temp.comment + "This string is long and has high entropy. ";
                            environmentVariables[i] = temp;
                        }
                    }
                    //return no value

                    Console.WriteLine("Final score for secret {0}: {1}",environmentVariables[i].secret, environmentVariables[i].score);
                    Console.WriteLine("Comments on detection: {0}", environmentVariables[i].comment);
                }
        }
    }
}