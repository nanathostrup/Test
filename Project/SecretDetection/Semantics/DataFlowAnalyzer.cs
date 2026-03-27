using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Project.SecretDetection.Semantics{
    class DataFlowAnalyzer
    {
        public List<SyntaxToken> results = new List<SyntaxToken>();
        //get a dataflow analysis on variables that HAVE to be identification tokens - meaning each of the detectors must find them prior to dataflow analysis
        public List<SyntaxToken> dataflowInit(List<SyntaxTree> trees, List<SyntaxToken> idTokens)
        {
            results.AddRange(idTokens);
            return dataflowAnalysis(trees, idTokens);
        }
        public List<SyntaxToken> dataflowAnalysis(List<SyntaxTree> trees, List<SyntaxToken> idTokens)
        {
            var newTokens = new List<SyntaxToken>();

            foreach (var idToken in idTokens.ToList()) // <-- make a copy to safely iterate
            {
                for (int i = 0; i < trees.Count; i++)
                {
                    SyntaxNode root = trees[i].GetRoot();
                    var matchingTokens = root.DescendantTokens()
                        .Where(t => t.IsKind(SyntaxKind.IdentifierToken) &&
                                    t.Text == idToken.Text)
                        .ToList();

                    foreach (var match in matchingTokens)
                    {
                        Console.WriteLine("found matching token: " + match);
                        var additionalTokens = howIsItUsed(trees, idTokens, match.Parent);

                        // collect new tokens
                        foreach (var token in additionalTokens)
                        {
                            if (!idTokens.Contains(token) && !newTokens.Contains(token))
                                newTokens.Add(token);
                        }
                    }
                }
            }

            // merge new tokens into the main list after iteration
            if (newTokens.Count > 0)
            {
                idTokens.AddRange(newTokens);

                // recursively analyze the new tokens
                dataflowAnalysis(trees, newTokens);
            }
            return idTokens;
        }
        // public void dataflowAnalysis(List<SyntaxTree> trees, List<SyntaxToken> idTokens)//List<SyntaxToken> idTokens) //bottom up approach
        // {
        //     //For each block - find idTokens
        //         //if idToken is among found idTokens
        //             //save idTokens and go again until there are no new idTokens to be added
        //     foreach (var idToken in idTokens){ //REKURSION PÅ ET TIDSPUNKT!!!!
        //         for (int i = 0; i < trees.Count; i++) //fot testing purposes, only weatherservices.cs being analyzed
        //         {
        //             SyntaxNode root = trees[i].GetRoot();
        //             var matchingTokens = root.DescendantTokens()
        //                     .Where(t => t.IsKind(SyntaxKind.IdentifierToken) &&
        //                                 t.Text == idToken.Text)
        //                     .ToList();
        //             foreach(var match in matchingTokens)
        //             {
        //                 Console.WriteLine("found matching token: " + match);
        //                 howIsItUsed(trees, idTokens, match.Parent); //we need a node not a token for this, so Parent
        //             }
        //         }
        //     }
        // }
        public List<SyntaxToken> howIsItUsed(List<SyntaxTree> trees, List<SyntaxToken> idTokens, SyntaxNode node)
        {
            //is it in an invocationmethod?
                //send on to how to save the new identificationTokens to global list
            
            // if (node == null) return; //stop klods?

            switch (node)
            {
                case VariableDeclaratorSyntax variableDeclarator:
                    Console.WriteLine($"Token {node} is part of an assignment: {variableDeclarator}");
                    return idTokens;
                    break;
                case InvocationExpressionSyntax invocation:
                    Console.WriteLine($"Token {node} is part of an invocationExpression: {invocation}");
                    return idTokens;
                    break;
                case AssignmentExpressionSyntax assignment:
                    Console.WriteLine($"Token {node} is part of an assignment: {assignment}");
                    return idTokens;
                    break;
                case ParameterSyntax parameter:
                    Console.WriteLine($"Token {node} is part of a method parameter: {parameter.Identifier.Text}");
                    return idTokens;
                    break;
                case MemberAccessExpressionSyntax memberAccess:
                    Console.WriteLine($"Token {node} is part of a member access: {memberAccess}");
                    return memberAccessHandler(trees, idTokens, node);
                    break;
                default:
                    Console.WriteLine("Wompidi womp");
                    return howIsItUsed(trees, idTokens, node.Parent); //Bliver faaaarliiigg - skal stop klods på
                    break;
            }
        }
        public List<SyntaxToken> memberAccessHandler(List<SyntaxTree> trees, List<SyntaxToken> idTokens, SyntaxNode node)
        {
            Console.WriteLine("Heeey girl");
            bool parentIsInvocation = node.Parent is InvocationExpressionSyntax;
            if (parentIsInvocation)
            {
                Console.WriteLine("I KNEW IT. Need handling of other cases in memberaccessHandler");
                return invocationHandler(trees, idTokens, node.Parent);
            }
            //HANDLE OTHER CASES OF THIS INSTANCE
            return idTokens;
        }
        public List<SyntaxToken> invocationHandler(List<SyntaxTree> trees, List<SyntaxToken> idTokens, SyntaxNode node)
        {
            //Find the correct idTokens in invocationmethod to add to list
            //for all unique new idTokens, add to global list
            //else return list

            var newIdTokens = node.DescendantTokens()
                .Where(t => t.IsKind(SyntaxKind.IdentifierToken))
                .ToList();
            if(newIdTokens != null){
                foreach(var newIdToken in newIdTokens)
                {
                    // Console.WriteLine(idToken);
                    if (!idTokens.Contains(newIdToken))
                    {
                        idTokens.Add(newIdToken);
                    }
                }
            }
            return idTokens;
            
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