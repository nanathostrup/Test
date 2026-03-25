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
        public void dataFlowAnalysis(List<SyntaxTree> trees, string secret)
        {
            //Dataflow analysis on secret in each method
                //What is the secret initialized as? = initas
                //Dataflow analysis on initas

            for (int i = 0; i < trees.Count; i++)
            {
                Console.WriteLine("File nr {0} being conducted a dataflow analysis on", i);
                var compilation = CSharpCompilation.Create("MyAnalysis")
                        .AddSyntaxTrees(trees[i])
                        .AddReferences(
                            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
                        );

                var semanticModel = compilation.GetSemanticModel(trees[i]); //make semantic model using compilation and syntax tree
                SyntaxNode root = trees[i].GetRoot();   
                    
                var method = root.DescendantNodes() //find the first method declaration to do the analysis on -- sheit for hvad hvis der er flere metoder i et programm?????
                    .OfType<MethodDeclarationSyntax>()
                    // .ToList();
                    .First();

                var identifiers = method.Body.DescendantNodes()
                    .OfType<IdentifierNameSyntax>();

                foreach (var id in identifiers)
                {
                    var symbol = semanticModel.GetSymbolInfo(id).Symbol;

                    if (symbol is IFieldSymbol)
                    {
                        Console.WriteLine("Field used: " + symbol.Name);
                    }
                }


                // For all variables that are declared
                // Where are they used? 
                    //are they used in other declared variabe things? Then look for this too


                // is secret used in current AST?





                // foreach (var method in methods)
                // {
                //     Console.WriteLine("Starting analysis on a method");
                //     var dataFlow = semanticModel.AnalyzeDataFlow(method.Body); //do the actual dataflow analysis on current tree in current method
                //     if (dataFlow.Succeeded)
                //     {
                //         Console.WriteLine("Data flow analysis succeeded");
                //         var temp = dataFlow.ReadOutside;
                //         Console.WriteLine("Length: " + temp.Length);
                //         if (temp.Length > 0){
                //             foreach (var t in temp)
                //             {
                //                 Console.WriteLine("variable found: " + t);
                //                 // dataFlow.ReadInside
                //                 // dataFlow.WrittenInside
                //                 // dataFlow.VariablesDeclared
                //                 // dataFlow.CapturedInside
                //                 // dataFlow.UsedLocalFunctions

                //             }
                //         }
                //         Console.WriteLine("");
                //     }
                // }
            }
        }







            // try{ // reimplement - catches files that dont have methods for the dataflow analysis
                //get the compilation
                // var compilation = CSharpCompilation.Create("MyAnalysis")
                //     .AddSyntaxTrees(tree)
                //     .AddReferences(
                //         MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
                //     );

                // var semanticModel = compilation.GetSemanticModel(tree); //make semantic model using compilation and syntax tree
                // SyntaxNode root = tree.GetRoot();   
                    
                // var methods = root.DescendantNodes() //find the first method declaration to do the analysis on -- sheit for hvad hvis der er flere metoder i et programm?????
                //     .OfType<MethodDeclarationSyntax>()
                //     .ToList();
                //     // .First();
                
                // foreach(var method in methods)
                // {
                //     // dataFlowAnalysisOnMethod(method);
                //     var dataFlow = semanticModel.AnalyzeDataFlow(method.Body); //do the actual dataflow analysis
                //     // var dataFlow = semanticModel.AnalyzeDataFlow(nameSpace); //do the actual dataflow analysis
                //         //StatementSyntax or an ExpressionSyntax or a ConstructorInitializerSyntax or a PrimaryConstructorBaseTypeSyntax.
                //         //MemberDeclarationSyntax

                //     if (dataFlow.Succeeded)
                //     {
                //         //If i find any of the input secrets then see where they are read and save via read inside

                //         Console.WriteLine(":)");
                //         // Console.WriteLine("Dataflow analysis succeeded :)");
                //         var temp = dataFlow.ReadInside;
                //         Console.WriteLine("Length: " + temp.Length);
                //         if (temp.Length > 0){
                //             foreach (var t in temp)
                //             {
                //                 Console.WriteLine("variable found: " + t);
                //             }
                //         }
                //     }
                // }
                // Console.WriteLine("----");

                // var nameSpace = root.DescendantNodes() //find the first namespace declaration to do the analysis on
                //     .OfType<NamespaceDeclarationSyntax>()
                //     .First();

            // }
            // catch
            // {
            //     Console.WriteLine("we catching - because the file does not contain a method or what now we are going to be looking into");
            // }
        // }
        public void dataFlowAnalysisOnMethod(MethodDeclarationSyntax method)
        {
            // var dataFlow = semanticModel.AnalyzeDataFlow(method.Body); //do the actual dataflow analysis
            // // var dataFlow = semanticModel.AnalyzeDataFlow(nameSpace); //do the actual dataflow analysis
            //     //StatementSyntax or an ExpressionSyntax or a ConstructorInitializerSyntax or a PrimaryConstructorBaseTypeSyntax.
            //     //MemberDeclarationSyntax

            //     if (dataFlow.Succeeded)
            //     {
            //         Console.WriteLine(":)");
            //         // Console.WriteLine("Dataflow analysis succeeded :)");
            //         // Console.WriteLine("Length: " + dataFlow.ReadOutside.Length);
            //         // if (dataFlow.ReadOutside.Length > 0){
            //         //     foreach (var location in dataFlow.ReadOutside)
            //         //     {
            //         //         Console.WriteLine(":O");
            //         //         Console.WriteLine(location);
            //         //     }
            //         // }
            //     }
            //     else
            //     {
            //         Console.WriteLine(":(");
            //     }
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