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
        //get a dataflow analysis on variables that HAVE to be identification tokens - meaning each of the detectors must find them prior to dataflow analysis
        public void dataflowAnalysis(List<SyntaxTree> trees, List<SyntaxToken> idTokens) //bottom up approach
        {
            for (int i = 0; i < trees.Count; i++) //fot testing purposes, only weatherservices.cs being analyzed
            {
                Console.WriteLine("Conducting dataflow analysis on file nr {0}", i);
                var compilation = CSharpCompilation.Create("MyAnalysis")
                        .AddSyntaxTrees(trees[i])
                        .AddReferences(
                            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
                        );

                var semanticModel = compilation.GetSemanticModel(trees[i]); //make semantic model using compilation and syntax tree
                SyntaxNode root = trees[i].GetRoot();   
                    
                var methods = root.DescendantNodes() //find the first method declaration to do the analysis on -- sheit for hvad hvis der er flere metoder i et programm?????
                    .OfType<MethodDeclarationSyntax>()
                    .ToList();
                    // .First(); //good for testing

                foreach (var method in methods){           
                    var dataFlow = semanticModel.AnalyzeDataFlow(method.Body); //do the actual dataflow analysis on current tree in current method
                    if (dataFlow.Succeeded)
                    {
                        Console.WriteLine("Data flow analysis succeeded");
                        var temp = dataFlow.VariablesDeclared;
                        Console.WriteLine("Length: " + temp.Length);
                        foreach(var t in temp)
                        {
                            Console.WriteLine("Variables declared: " + t);
                        }
                        foreach (var id in idTokens)
                        {
                            var symbol = semanticModel.GetSymbolInfo(id.Parent).Symbol;


                            if (!temp.Contains(symbol, SymbolEqualityComparer.Default))
                            {
                                Console.WriteLine(":( " + id);
                            }
                            else
                            {
                                Console.WriteLine(":) " + id);
                            }
                        }             
                    // }


                    //     if (readInside.Length > 0){;}
                        // }
                    }
                }
                Console.WriteLine("");
            }
        }
    }
}