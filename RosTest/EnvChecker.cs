using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

class EnvChecker
{
    //This function saves the potential secrets, and for now it is a helper function depending on the needs of the project in a larger scope
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
}