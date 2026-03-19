using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Project.SecretDetection.EnvironmentChecking{
    class EnvChecker
    {
        public struct EnvironmentVariable
            {
                public int index { get; set;}
                public string envfile { get; set;}
                public string secret { get; set;}
                public int score { get; set;}
                public EnvironmentVariable(int index, string envfile, string secret, int score)
                {
                    this.index = index;
                    this.envfile = envfile;
                    this.secret = secret;
                    this.score = score;
                } 
            }

        public List<EnvironmentVariable> getUnusedEnvVariables(List<string> StringArgs, string filePath) //OPTIMIZE!!!
        {
            //For each stringargument 
                //is it present in the envfiles?
                    //if so - extract the suffix in the file
            
            List<EnvironmentVariable> unusedEnvironmentVariables = new List<EnvironmentVariable>();
            bool used = false;

            var EnvFiles = Directory.EnumerateFiles(filePath, "*", SearchOption.AllDirectories)
                .Where(f =>
                    f.Contains(@".env")
                );
            Console.WriteLine("Checking these files for unused env variables in .env files found in directory"); //Just to make life easier we print what we check 
            foreach (var envfile in EnvFiles)
            {
                Console.WriteLine("     " + envfile);
            }

            foreach (var envfile in EnvFiles)
            {
                int i = 0; //create an index counter so we can track the location in the env file and can write this to the report
                foreach (var line in File.ReadLines(envfile))
                {
                    i++; //update the index counter every time we move line :)
                    used = false;
                    
                    foreach (var str in StringArgs)
                    {
                        if (line.Contains(str))
                        {
                            used = true;
                        }
                    }
                    if (!used)
                    {
                        // unusedEnvironmentVariables.Add(line); //Hele linjen in envfilen
                        string extractedStr = extractString(line); //Kun selve valuen
                        int locationIndex = i;//Lokationen i env filen
                        int score = 0; //Den skal initialiseres her, og opdateres i analysen af stringen                      
                        var envVar = new EnvironmentVariable(locationIndex, envfile, extractedStr, score);
                        unusedEnvironmentVariables.Add(envVar);
                    }
                }
            }
            return unusedEnvironmentVariables;
        }


        public List<EnvironmentVariable> getUsedEnvVariables(List<string> StringArgs, string filePath) //OPTIMIZE!!!
        {
            List<EnvironmentVariable> usedEnvironmentVariables = new List<EnvironmentVariable>();

            var EnvFiles = Directory.EnumerateFiles(filePath, "*", SearchOption.AllDirectories)
                .Where(f =>
                    f.Contains(@".env")
                );
            Console.WriteLine("Checking these files for used env variables in .env files found in directory"); //Just to make life easier we print what we check 
            foreach (var envfile in EnvFiles)
            {
                Console.WriteLine("     " + envfile);
            }

            foreach (var envfile in EnvFiles)
            {
                int i = 0; //create an index counter so we can track the location in the env file and can write this to the report
                foreach (var line in File.ReadLines(envfile))
                {
                    i++; //update the index counter every time we move line :)
                    foreach (var str in StringArgs)
                    {
                        if (line.Contains(str))
                        {
                            // usedEnvironmentVariables.Add(line); //Hele linjen in envfilen
                            string extractedStr = extractString(line); //Kun selve valuen
                            int locationIndex = i; //Lokationen i env filen - burde måske være sin egen funtion men det her er så meget nemmere:)))
                            int score = 0; //Den skal initialiseres her, og opdateres i analysen af stringen                      
                            var envVar = new EnvironmentVariable(locationIndex, envfile, extractedStr, score);
                            usedEnvironmentVariables.Add(envVar);
                        }
                    }
                }
            }
            return usedEnvironmentVariables;
        }
        
        public string extractString(string envVariables) // Giver dig din string uden = og uden mellemrummet. Hvis ikke "= " passer så får man bare alt.
        {
            try
            {
                string extractedString = envVariables.Split('=')[1];
                if (extractedString.StartsWith(' '))
                {
                    string newExtractedString = extractedString.Split(' ')[1];
                    return newExtractedString;
                }
                else
                {
                    return extractedString;
                }
            }
            catch
            {
                return envVariables;
            }
        }
    }
}