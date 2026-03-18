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
                public string secret {get; set;}
                public EnvironmentVariable(int index, string envfile, string secret)
                {
                    this.index = index;
                    this.envfile = envfile;
                    this.secret = secret;
                } 
            }

        public List<EnvironmentVariable> getUnusedEnvVariables(List<string> StringArgs, string filePath) //OPTIMIZE!!!
        {
            //For each stringargument 
                //is it present in the envfiles?
                    //if so - extract the suffix in the file
            
            List<EnvironmentVariable> unusedEnvironmentVariables = new List<EnvironmentVariable>();
            // List<string> unusedEnvironmentVariables = new List<string>();
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

            //Maybe redo such that we get more info, e.g. line number and what file the error so it can be added to the end report

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
                        // Console.WriteLine("FOUND ON : line " + locationIndex); 
                        // Console.WriteLine("IN FILE: " + envfile);
                        
                        var envVar = new EnvironmentVariable(locationIndex, envfile, extractedStr);
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

            //Maybe redo such that we get more info, e.g. line number and what file the error so it can be added to the end report

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
                            // Console.WriteLine("FOUND ON : line " + locationIndex); 
                            // Console.WriteLine("IN FILE: " + envfile);

                            var envVar = new EnvironmentVariable(locationIndex, envfile, extractedStr);
                            usedEnvironmentVariables.Add(envVar);
                        }
                    }
                }
            }
            return usedEnvironmentVariables;
        }
        
        public string extractString(string envVariables) // Giver dig din string uden = og uden mellemrummet. Hvis ikke "= " passer så får man bare alt andet.
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
                    // Console.WriteLine("EXTRACTED STRING:{0}", extractedString);
                    return extractedString;
                }
            }
            catch
            {
                // Console.WriteLine("We catching");
                // Console.WriteLine("EXTRACTED STRING:{0}", envVariables);
                return envVariables;
            }
        }
    }
}