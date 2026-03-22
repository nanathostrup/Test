using System;
using System.ComponentModel;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Project.SecretDetection.Semantics{
    class DataFlowAnalyzer
    {
        public void dataFlowAnalysis(SyntaxTree tree)
        {
            try{
                //get the compilation
                var compilation = CSharpCompilation.Create("MyAnalysis")
                    .AddSyntaxTrees(tree)
                    .AddReferences(
                        MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
                    );

                var semanticModel = compilation.GetSemanticModel(tree); //make semantic model using compilation and syntax tree
                SyntaxNode root = tree.GetRoot();   
                    
                var method = root.DescendantNodes() //make a method - not sure what exactly this does 
                    .OfType<MethodDeclarationSyntax>()
                    .First();
                
                var dataFlow = semanticModel.AnalyzeDataFlow(method.Body); //do the actual dataflow analysis
                
                if (dataFlow.Succeeded)
                {
                    Console.WriteLine("Dataflow analysis succeeded :)");
                    Console.WriteLine("Length: " + dataFlow.ReadOutside.Length);
                    if (dataFlow.ReadOutside.Length > 0){
                        foreach (var location in dataFlow.ReadOutside)
                        {
                            Console.WriteLine(":O");
                            Console.WriteLine(location);
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("we catching - theory is because it is not a .cs file. Find a fix for this");
            }
        }

        //Funktion med input(AST, variabelToTrace)
            //Go through AST and find all source points/uses for that variable
            //Return that list - this list will be used in PLACEANALYSIS
    
    
        // var semanticModel = context.SemanticModel;
        // var dataFlow = semanticModel.AnalyzeDataFlow(syntax);
        // if (dataFlow.Succeeded)
        // {
        //     // dataFlow.ReadInside
        //     // dataFlow.WrittenInside
        //     // dataFlow.VariablesDeclared
        //     // dataFlow.CapturedInside
        //     // dataFlow.UsedLocalFunctions
        //     // etc.
        // }


        //CONTROLFLOW ANALYSIS -- KAN VISE HVOR NOGET EXITER I KODE
        // var semanticModel = context.SemanticModel;
        // var controlFlow = semanticModel.AnalyzeControlFlow(statementSyntax);
        // if(controlFlow.Succeeded)
        // {
        //     // controlFlow.EntryPoints
        //     // controlFlow.ExitPoints
        //     // controlFlow.StartPointIsReachable
        //     // controlFlow.EndPointIsReachable
        //     // controlFlow.ReturnStatements
        // }


    
    }
}