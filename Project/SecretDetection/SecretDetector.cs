using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project.SecretDetection.Semantics;
using Project.SecretDetection.Secrets;

namespace Project.SecretDetection{
    public class SecretDetector
    {
        public void Detect(string filePath)
        {
            // foreach file
            //     make AST
            
            // Foreach AST
            //     if something accesses env file
            //         check that variable in cs file in env file
            //             is it a string litteral?
            
            Console.WriteLine("");
            Console.WriteLine(" ============================= CONVERTING TO AST ============================= ");
            var ast = new AST();
            List<SyntaxTree> trees = ast.createASTs(filePath);

            Console.WriteLine("");
            Console.WriteLine(" ===================== EXTRACT ENVIRONMENT CALL INPUTS ======================= ");
            var walker = new Walker();
            foreach (SyntaxTree tree in trees)
            {
                SyntaxNode root = tree.GetRoot(); //Get the root of the tree
                walker.Visit(root); //Check the current AST for invocation expressions. Walker går gennem træet, og der er blevet lavet sær regel for invocation expressions
            }
            foreach (var stringarg in walker.StringArgs) //skal være walker.StringArgs, da der er blevet overridet, så der kan ikke returneres disse values der hvor de findes:( -- kan finde ud af fix på tidspunkt
            {
                Console.WriteLine("GetEnvironmentVariable() inputs found: " + stringarg); //Test at vi får de rigtige ud
            }
            
            Console.WriteLine("");
            Console.WriteLine(" ================================= ENV CHECK ================================= ");
            List<string> usedEnvironmentVariables = new List<string>();
            List<string> unusedEnvironmentVariables = new List<string>();
            var envChecker =  new EnvChecker();
            unusedEnvironmentVariables = envChecker.getUnusedEnvVariables(walker.StringArgs, filePath);
            Console.WriteLine("");
            usedEnvironmentVariables = envChecker.getUsedEnvVariables(walker.StringArgs, filePath);
            
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Report.txt"); //Create the output log
            path = Path.GetFullPath(path);
            FileStream outputLog = File.Create(path); 
            
            Console.WriteLine("");
            foreach (var use in usedEnvironmentVariables) // Print for at checke
            {
                Console.WriteLine("USED VARIABLE:       {0}", use);
            }

            foreach (var unuse in unusedEnvironmentVariables)
            {
                Console.WriteLine("UNUSED VARIABLE:      {0}", unuse);
            }

            // Console.WriteLine("");
            // Console.WriteLine(" ================================= ENV CHECK ================================= ");
            // Console.WriteLine("TODO: EnvChecker function needs to be prettier");
            // var envChecker = new EnvChecker();
            // List<string> extractedStrings = envChecker.extractEnvValue(walker.StringArgs, filePath);
            // foreach (string extraction in extractedStrings)
            // {
            //     Console.WriteLine("Extracted string: {0}", extraction); //Check resultaterne
            // }

            // Console.WriteLine("");
            // Console.WriteLine("Locations of leaks");
            // Dictionary<int, string> locationIndexes = envChecker.extractLeakLocation(walker.StringArgs, filePath);
            // foreach(var (index, file) in locationIndexes)
            // {
            //     Console.WriteLine("Possible leak detected in {0} on line {1}", file, index);
            // }

            // Console.WriteLine("");
            // Console.WriteLine(" ================================== ENTROPY ================================== ");
            // var secretsVerifier = new SecretsVerifier();
            // foreach (string extraction in extractedStrings)
            // {
            //     double entropy = secretsVerifier.ShannonEntropy(extraction);
            //     Console.WriteLine("Entropy of \""+extraction+ "\": " + entropy + "Looks like hex: " + secretsVerifier.isItHex(extraction) + " Looks like base 64: " + secretsVerifier.isItBase64(extraction));
            // }

            // // For some tests of entropy, hex, base64...
            // var tester = new Tester();
            // tester.EntropyTester();

            //For printing each AST
            // foreach (SyntaxTree tree in trees)
            // {
            //     Console.WriteLine(" ================================= NEW SYNTAX TREE ================================= ");
            //     // Get the root of the tree
            //     SyntaxNode root = tree.GetRoot();
            //     PrintNode(root, 0);
            // }
        }

        //Tyv stjålet fra sooomewhere... dont remember hvor...
        static void PrintNode(SyntaxNode node, int indent)
        {
            var padding = new string(' ', indent * 2);
            Console.WriteLine($"{padding}{node.Kind()}");

            // Print tokens with values
            foreach (var token in node.ChildTokens())
            {
                Console.WriteLine($"{padding}  TOKEN {token.Kind()} : {token.ValueText}");
            }

            foreach (var child in node.ChildNodes())
            {
                PrintNode(child, indent + 1);
            }
        }
    }
}