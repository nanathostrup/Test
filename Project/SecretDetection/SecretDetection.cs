using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project.SecretDetection.Semantics;
using Project.SecretDetection.SecretsAnalysis;
using Project.SecretDetection.EnvironmentChecking;
using Project.SecretDetection.PlaceAnalysis;
using System.Linq.Expressions;


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

            // for(int i = 0; i < walker.InitializedArgs.Count; i++) //skal være walker.StringArgs, da der er blevet overridet, så der kan ikke returneres disse values der hvor de findes:( -- kan finde ud af fix på tidspunkt
            // {
            //     Console.WriteLine("GetEnvironmentVariable() inputs found: " + walker.StringArgs[i]); //Test at vi får de rigtige ud
            //     Console.WriteLine("GetEnvironmentVariable() initialized as: " + walker.InitializedArgs[i]);
            //     Console.WriteLine("");
            // }
            foreach (var kvp in walker.EnvironmentVariableMap)
            {
                Console.WriteLine("GetEnvironmentVariable() input: " + kvp.Key);
                Console.WriteLine("Initialized as: " + kvp.Value);
                Console.WriteLine("");
            }


            Console.WriteLine("");
            Console.WriteLine(" ================================= ENV CHECK ================================= ");
            var envChecker =  new EnvChecker();
            List<EnvChecker.EnvironmentVariable> unusedEnvironmentVariables = new List<EnvChecker.EnvironmentVariable>();
            List<EnvChecker.EnvironmentVariable> usedEnvironmentVariables = new List<EnvChecker.EnvironmentVariable>();
            unusedEnvironmentVariables = envChecker.getUnusedEnvVariables(walker.EnvironmentVariableMap, filePath); //ikke nogen initialized args her, netop fordi de ikke er brugt og dermed ikke initializeret.
            usedEnvironmentVariables = envChecker.getUsedEnvVariables(walker.EnvironmentVariableMap, filePath);
            // unusedEnvironmentVariables = envChecker.getUnusedEnvVariables(walker.StringArgs, filePath); //ikke nogen initialized args her, netop fordi de ikke er brugt og dermed ikke initializeret.
            // usedEnvironmentVariables = envChecker.getUsedEnvVariables(walker.StringArgs, walker.InitializedArgs, filePath);
            
            Console.WriteLine("");
            foreach(var unusedEnvironmentVariable in unusedEnvironmentVariables)
            {
                Console.WriteLine("UNUSED ENVIRONMENT VARIABLE {0} FOUND IN FILE: {1}, ON LINE {2}. SECRET: {3}", 
                unusedEnvironmentVariable.name, unusedEnvironmentVariable.envfile, unusedEnvironmentVariable.index, unusedEnvironmentVariable.secret);
            }
            foreach(var usedEnvironmentVariable in usedEnvironmentVariables)
            {
                Console.WriteLine("USED ENVIRONMENT VARIABLE {0} FOUND IN FILE: {1}, ON LINE {2}. SECRET: {3}", 
                usedEnvironmentVariable.name, usedEnvironmentVariable.envfile, usedEnvironmentVariable.index, usedEnvironmentVariable.secret);
            }
            
            Console.WriteLine("");
            Console.WriteLine(" ============================ DATA FLOW ANALYSIS ============================= ");
            var httpDetector = new HttpDetector(); // Skal rykkes ind i envchecker når done fordi det er sådan man får en weight ligesom med secret scoring
            foreach(var used in usedEnvironmentVariables)
            {
                float weight = httpDetector.getWeight(trees, used.name);
                Console.WriteLine("The weight of http location detection: {0}, for variable {1}", weight, used.name);    
            }
            
            Console.WriteLine("MISSING: Finish dataflow analysis, only partly done"); 

            

            Console.WriteLine("");
            Console.WriteLine(" ================================= SCORING =================================== ");
            // var entropyDetector = new EntropyDetector();
            // var hexDetector = new HexDetector();
            // var base64Detector = new Base64Detector();

            // var apikeyDetector = new APIKeyDetector();
            // apikeyDetector.detect("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0.KMUFsIDTnFmyG3nMiGM6H9FNFUROf3wh7SmqJp-QV30");

            var envScorer = new EnvScorer();
            envScorer.giveScore(usedEnvironmentVariables, trees);
            Console.WriteLine("");
            envScorer.giveScore(unusedEnvironmentVariables, trees);
            envScorer.giveWeight(usedEnvironmentVariables, trees);


            // // // Console.WriteLine(" ############ TEST ############ ");
            // // // List<string> randomwords = new List<string>() {"oranges", "google", "traffic light", "cykel", "random", "ApiKeys", "Cryptography", "durumrulle", "laptopskærm", "entropy", "ARGGHHHHHH", "pneumonoultramicroscopicsilicovolcanoconiosis", "Antidisestablishmentarianism", "kat", "Champ", "Titin", "Aegilops", "Champichamp", "DemonChild", "Bæstet"};
            // // // foreach(string rand in randomwords)
            // // // {
            // // //     int val = base64Detector.detect(rand);
            // // //     Console.WriteLine("Measured base64 for string {0}: {1}", rand, val);
            // // // }
            // // // Console.WriteLine(" ############ TEST ############ ");
            // // // List<string> secretsIsh = new List<string>() {"ea413b8c6e9657e69c24ca2b83e6d895", "password", "api_XweVmYIoqSCHxVOb4Q6C1zMFs0O92zPu", "AKIAIOSFODNN73XAMPL3", "wJalrXUtnFEMI/K7MDENG/bPxRfiCY3XAMPL3K3Y", "IQoJb3JpZ2luX2VjEFAaCXVzLXdlc3QtMiJIMEYCIQDFAK3", "AIzaSyDaGmWKa4JsXZ-HjGw7ISLn_3namBGewQe", "sk_live_C8YfyXfzocnRZNE36yzd7Pg3Wl0aqCad", "sk_live_skfikf5682lfjas896dsndhfuek9hy654", "pk_t3st_4RxUQ9rE2xn8vIbplcQlCLQN", "sk_live_3hmB4s6o0a62C7vrsK00sBJPb3z4CzY9GSEz1dfMtloMec9LpD949IbDPwbeW", "ghp_abcdefghijklmnopqrstuvwxyz123456", "github_pat_11ABCDEF1234567890FAK3", "glpat-abcdefghijklmnopqrstuvwxyz", "123456789:AAFAK3-telegram-bot-token-3xampl3", "your_auth_token_32charslong", "SG.fak3_sendgrid_api_k3y_3xampl3", "SuperLongJWTSigningSecretK3y123456789", "WOlJeDRXzIDR9N0xXrQjIOYNoMYrlEvMz3HF91RTy", "YXZhaWxhYmxldGlyZWRldmVudHRhbGVzcmVndWxhcnByb2R1Y2VlbGV2ZW5zdGFydGM", "ec820703bf716f1bf64a2e54199395ed"};
            // // // foreach(string str in secretsIsh)
            // // // {
            // // //     int val = base64Detector.detect(str);
            // // //     Console.WriteLine("Measured entropy for string {0}: {1}", str, val);
            // // // }



            // Console.WriteLine("");
            // Console.WriteLine(" ================================== REPORT =================================== ");            
            // string logpath = Path.Combine(Directory.GetCurrentDirectory(), "Report.txt"); //Create the output log
            // logpath = Path.GetFullPath(logpath);

            // using (StreamWriter writer = new StreamWriter(logpath, append: false)) // append: false to overwrite, true to append to existing file, tak til chat:)
            // {
            //     foreach (var usedEnvVar in usedEnvironmentVariables)
            //     {
            //         writer.WriteLine("An environment variable is used in file {0} on line {1} and has a score of {2}. {3}", usedEnvVar.envfile, usedEnvVar.index, usedEnvVar.score, usedEnvVar.comment);
            //     }
            //     foreach (var unusedEnvVar in unusedEnvironmentVariables)
            //     {
            //         writer.WriteLine("An unused environment variable is detected in file {0} on line {1} and has a score of {2}. {3}", unusedEnvVar.envfile, unusedEnvVar.index, unusedEnvVar.score, unusedEnvVar.comment);
            //     }
            // }
            // Console.WriteLine("A report has been made in {0} \n", logpath);

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