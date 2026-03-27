using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Project.SecretDetection.Semantics{
    class DataFlowAnalyzer
    {
        public List<SyntaxToken> results;
        //get a dataflow analysis on variables that HAVE to be identification tokens - meaning each of the detectors must find them prior to dataflow analysis
        public void dataflowAnalysis(List<SyntaxTree> trees, SyntaxToken idToken)//List<SyntaxToken> idTokens) //bottom up approach
        {
            //For each block - find idTokens
                //if idToken is among found idTokens
                    //save idTokens and go again until there are no new idTokens to be added
            for (int i = 0; i < trees.Count; i++) //fot testing purposes, only weatherservices.cs being analyzed
            {
                SyntaxNode root = trees[i].GetRoot();
                var matchingTokens = root.DescendantTokens()
                        .Where(t => t.IsKind(SyntaxKind.IdentifierToken) &&
                                    t.Text == idToken.Text)
                        .ToList();
                foreach(var match in matchingTokens)
                {
                    Console.WriteLine("found matching token: " + match);
                    howIsItUsed(match.Parent); //we need a node not a token for this, so Parent
                }
            }
        }
        public void howIsItUsed(SyntaxNode idToken)
        {
            //is it in an invocationmethod?
                //send on to how to save the new identificationTokens to global list
            
            if (idToken == null) return; //stop klods?

            switch (idToken)
            {
                case VariableDeclaratorSyntax variableDeclarator:
                    Console.WriteLine($"Token {idToken} is part of an assignment: {variableDeclarator}");
                    break;
                case InvocationExpressionSyntax invocation:
                    Console.WriteLine($"Token {idToken} is part of an invocationExpression: {invocation}");
                    break;
                case AssignmentExpressionSyntax assignment:
                    Console.WriteLine($"Token {idToken} is part of an assignment: {assignment}");
                    break;
                case ParameterSyntax parameter:
                    Console.WriteLine($"Token {idToken} is part of a method parameter: {parameter.Identifier.Text}");
                    break;
                case MemberAccessExpressionSyntax memberAccess:
                    Console.WriteLine($"Token {idToken} is part of a member access: {memberAccess}");
                    memberAccessHandler();
                    break;
                default:
                    Console.WriteLine("Wompidi womp");
                    howIsItUsed(idToken.Parent); //Bliver faaaarliiigg - skal stop klods på
                    break;
            }
        }
        public void memberAccessHandler()
        {
            Console.WriteLine("Heeey girl");
        }
        public void invocation()
        {
            //Find the correct idTokens in invocationmethod to add to list
            //for all unique new idTokens, add to global list
            //else return list
        }
    }
}

            //For each tree
                //get all idTokens
                //locate idToke
                    //Find ud af hvad den er initaliseret som
                    //lav switchcase på hvordan den er initialiseret
                        //|invocation
                        //|variabledeclerator
                        //...
                    //fid ud af hvor den bruges
                        //for hvert brug
                            //find ud af hvilke input den har
                            //find ud af hvordan indputne er defineret
            // for (int i = 0; i < trees.Count; i++) //fot testing purposes, only weatherservices.cs being analyzed
            // {
            //     Console.WriteLine("Conducting dataflow analysis on file nr {0}", i);
            //     var compilation = CSharpCompilation.Create("MyAnalysis")
            //             .AddSyntaxTrees(trees[i])
            //             .AddReferences(
            //                 MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
            //             );

            //     var semanticModel = compilation.GetSemanticModel(trees[i]); //make semantic model using compilation and syntax tree
            //     SyntaxNode root = trees[i].GetRoot();   
                    
            //     // var methods = root.DescendantNodes() 
            //     //     .OfType<MethodDeclarationSyntax>()
            //     //     .ToList();
            //         // .First(); //good for testing
            //     var matchingTokens = root.DescendantTokens()
            //             .Where(t => t.IsKind(SyntaxKind.IdentifierToken) &&
            //                         t.Text == idToken.Text)
            //             .ToList();
            //     foreach (var match in matchingTokens)
            //     {
            //         Console.WriteLine("found match: " + match);
            //     }
                
            //     Console.WriteLine("");





                //find the idToken
//                 foreach(var method in methods)
//                 {
//                     var matchingTokens = method.DescendantTokens()
//                         .Where(t => t.IsKind(SyntaxKind.IdentifierToken) &&
//                                     t.Text == idToken.Text)
//                         .ToList();
//                     foreach (var match in matchingTokens)
//                     {
//                         Console.WriteLine("Found: " +  match);
//                         //is match in UsedVariables in dataflow? No?
//                             //find variableDeclaratos idToken
//                             //print for now, but send through this loop again recursively.
//                         var dataFlow = semanticModel.AnalyzeDataFlow(method.Body);
//                         if (dataFlow.Succeeded)
//                         {
//                             Console.WriteLine("dataflow analysis succeeded");
//                             foreach (var used in dataFlow.ReadInside)
//                             {
//                                 Console.WriteLine("varibale found in VariablesUsed: " + used);
//                                 // var usages = method.DescendantNodes() 
// //                                 //     .OfType<VariableDeclaratorSyntax>()
// //                                 //     // .Select(t => t.Identifier)
// //                                 //     .ToList();

// //                                 // foreach (var use in usages)
// //                                 // {
// //                                 //     Console.WriteLine("usage of variable: " + use.Parent);//.Parent.Parent.Parent);
// //                                 //     var idTokens = use.DescendantTokens()
// //                                 //         .Where(t => t.IsKind(SyntaxKind.IdentifierToken))
// //                                 //         .ToList();
// //                                 //     foreach(var id in idTokens)
// //                                 //     {
// //                                 //         Console.WriteLine("id tokens inside this variable declaration: " + id);
// //                                 //     }
// //                                 // }   
//                             }
//                         }
//                     }
                    
                // }
        //     }
        // }
    // }
// }