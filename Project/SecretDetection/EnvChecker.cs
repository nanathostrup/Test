using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Project.SecretDetection{
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
            //RETURN A LIST OF ENVIRONMENT VARIABLES
        public List<EnvironmentVariable> getUnusedEnvVariables(List<string> StringArgs, string filePath) //OPTIMIZE!!!
        {
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
                        // unusedEnvironmentVariables.Add(extractedStr);
                        
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
                            // envVar = new Env
                            // usedEnvironmentVariables.Add(extractedStr);

                            var envVar = new EnvironmentVariable(locationIndex, envfile, extractedStr);
                            usedEnvironmentVariables.Add(envVar);
                        }
                    }
                }
            }
            return usedEnvironmentVariables;
        }
        
        public string extractString(string envVariables)
        {
            try
            {
                string extractedString = envVariables.Split('=')[1];
                if (extractedString.StartsWith(' '))
                {
                    string newExtractedString = extractedString.Split(' ')[1];
                    // Console.WriteLine("EXTRACTED STRING:{0}", newExtractedString);
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
        

        
        public List<string> extractEnvValue(List<string> StringArgs, string filePath) //OPTMISER!
        {
            //For each stringargument 
                //is it present in the envfiles?
                    //if so - extract the suffix in the file

            var EnvFiles = Directory.EnumerateFiles(filePath, "*", SearchOption.AllDirectories)
                .Where(f =>
                    f.Contains(@".env")
                );

            Console.WriteLine("Env files found in directory"); //Just to make life easier we print what we check 
            foreach (var envfile in EnvFiles)
            {
                Console.WriteLine("     " + envfile);
            }

            //OPTIMIZE!!!!!!!!!!
            List<string> results = new List<string>();
            List<string> temp = new List<string>();

            foreach (var envfile in EnvFiles) //Can be optimized.
            {
                foreach (var stringArg in StringArgs) //!!
                {
                    var result = (from line in File.ReadLines(envfile)
                        where line.Contains(stringArg)
                        select line.Split('=')[1]).ToList(); //Retrieve only the acutal value of the argument, not the name of the argument (which is what the string argument is e.g. MY_APIKEY=abcdefg - MY_APIKEY is name and abcdefg is the value.)

                    if (result.Any())
                    {
                        Console.WriteLine("The String Argument \""+stringArg+ "\" was found in " + envfile);
                        results.AddRange(result);
                    }
                }
            }
            foreach(var result in results) //Små åndssvagt og kun til at fjerne mellemrummet efter = i e.g. MyAPIKEY = 123, så får vi "123" istedet for " 123".
            {
                if (result.StartsWith(' '))
                {
                    string newstr = result.Remove(0,1);
                    temp.Add(newstr);            
                }
                else
                {
                    temp.Add(result);
                }
            }
            return temp;
        }

        public Dictionary<int, string> extractLeakLocation(List<string> StringArgs, string filePath) //
        {
            var EnvFiles = Directory.EnumerateFiles(filePath, "*", SearchOption.AllDirectories)
                .Where(f =>
                    f.Contains(@".env")
                );

            Console.WriteLine("Env files found in directory"); //Just to make life easier we print what we check 
            foreach (var envfile in EnvFiles)
            {
                Console.WriteLine("     " + envfile);
            }
            Dictionary<int, string> dict = new Dictionary<int, string>();
            List <int> locationIndexes = new List<int>();
            foreach (var envfile in EnvFiles) //Can VERY MUCH be optimized.
            {
                foreach (var stringArg in StringArgs)
                {
                    string[] arrLine = File.ReadAllLines(envfile);
                    // int i = 0;
                    for (int i = 0; i< arrLine.Length; i++)
                    {
                        if (arrLine[i].Contains(stringArg))
                        {
                            locationIndexes.Add(i);
                            dict.Add(i+1, envfile);
                        }
                    }
                }
            }
            return dict;

        }
        // public int extractLocation(string envfile, string str)
        // {
        //     string[] arrLine = File.ReadAllLines(envfile);
        //     for (int i = 0; i< arrLine.Length; i++)
        //     {
        //         try{
        //         if (arrLine[i].Contains(str))
        //         {
        //             return i+1;
        //         }
        //         // else return 0; // DET HER ER EN ERROR --  MAYBE THROW AN EXCEPTION?
        //         }
        //         catch
        //         {
        //             Console.WriteLine("wtf");
        //         }
        //     }


                
            // foreach (var sætning in envfile)
            // {
            //     if (sætning==line)
            // }

        // }

    }
}