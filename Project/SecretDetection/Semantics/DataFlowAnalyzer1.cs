// using System;
// using System.ComponentModel;
// using System.IO;
// using System.Xml.Serialization;
// using Microsoft.CodeAnalysis;
// using Microsoft.CodeAnalysis.CSharp;
// using Microsoft.CodeAnalysis.CSharp.Syntax;

// namespace Project.SecretDetection.Semantics{
//     class DataFlowAnalyzer1
//     {
//         public void dataFlowAnalysis1(List<SyntaxTree> trees, string lookFor)
//         {
//             //For each tree
//                 //Get the identifierTokens -> egen method
//                 //For each method lav dataflow analyse
//                     //Get readIns
//                     //match på 

//             ///////////////////////////////Top down approach^^ Bottom up approach__

//             //For each tree
//                 //Get all id tokens
//                 //if id tokens.Contains(lookFor) -- Httpclient her
//                     //print for now, men ellers trace somehow
//                     //Find ud af hvad den er initaliseret som
//                     //fid ud af hvor den bruges
//                         //for hvert brug
//                             //find ud af hvilke input den har
//                             //find ud af hvordan indputne er defineret
                    
            
//             foreach (var tree in trees)
//             {
//                 Console.WriteLine("New tree");
//                 SyntaxNode root = tree.GetRoot();
//                 var idTokens = root.DescendantNodes()
//                     .OfType<IdentifierNameSyntax>()
//                     .Where(t => !t.Ancestors().OfType<UsingDirectiveSyntax>().Any())
//                     .Select(t => t.Identifier.Text)
//                     .ToList();
                
//                 var idTokensSyntaxTokens = root.DescendantNodes()
//                     .OfType<IdentifierNameSyntax>()
//                     .Where(t => !t.Ancestors().OfType<UsingDirectiveSyntax>().Any())
//                     .Select(t => t.Identifier
//                     .Where(tt => t =  ))
//                     .ToList();

//                 if (idTokens.Contains(lookFor))
//                 {
//                     Console.WriteLine(":))))" + lookFor);
//                     // send on to where this leads to
//                     // var initializedAs = whereIsItInitialized()
//                     //dataFlowAnalysis on initializedAs

//                 }

//                 Console.WriteLine("");
//             }
//         }

//         public void whereIsItInitialized(SyntaxToken token)
//         {
//             ;
//             //get the variable declaration = HttpClient
//                 //get the variable declarator
//         }



//         // public void dataFlowAnalysis(List<SyntaxTree> trees, string secret, int counter)//, List<string> alreadyVisitedArea)
//         // {
//         //     //Dataflow analysis on secret in each method
//         //         //What is the secret initialized as? = initas
//         //         //Dataflow analysis on initas
//         //     if (counter == 1) // ground work for recursion at some point
//         //     {
//         //         return;
//         //     }
//         //     for (int i = 2; i < trees.Count; i++) //fot testing purposes, only weatherservices.cs being analyzed
//         //     {
//         //         Console.WriteLine("File nr {0} being conducted a dataflow analysis on", i);
//         //         var compilation = CSharpCompilation.Create("MyAnalysis")
//         //                 .AddSyntaxTrees(trees[i])
//         //                 .AddReferences(
//         //                     MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
//         //                 );

//         //         var semanticModel = compilation.GetSemanticModel(trees[i]); //make semantic model using compilation and syntax tree
//         //         SyntaxNode root = trees[i].GetRoot();   
                    
//         //         var method = root.DescendantNodes() //find the first method declaration to do the analysis on -- sheit for hvad hvis der er flere metoder i et programm?????
//         //             .OfType<MethodDeclarationSyntax>()
//         //             // .ToList();
//         //             .First();
            
//         //         var identifiers = method.Body.DescendantNodes()
//         //             .OfType<IdentifierNameSyntax>();
                
//          //         var dataFlow = semanticModel.AnalyzeDataFlow(method.Body); //do the actual dataflow analysis on current tree in current method
//         //         if (dataFlow.Succeeded)
//         //         {
//         //             Console.WriteLine("Data flow analysis succeeded");
//         //             var readInside = dataFlow.UsedLocalFunctions;
//         //             // Console.WriteLine("Length: " + readInside.Length);
//         //             if (readInside.Length > 0){
//         //                 foreach (var readinside in readInside)
//         //                 {
//         //                     // var symbol = semanticModel.GetSymbolInfo(readinside).Symbol;
//         //                     // if (symbol != null && symbol.Name != null && symbol.Name.EndsWith(secret))
//         //                     // {
//         //                     //     Console.WriteLine("YAY");
//         //                     //      LAV EN DATAFLOW ANALYSE HER OG AKKUMULER HVAD ALLE INSTANCES SOM DEN SER SECRET RYGER UD TIL
//         //                     // }
//         //                     // else
//         //                     // {
//         //                         Console.WriteLine("");
//                                 // Console.WriteLine("variable found: " + readinside);
//                                 //invocation expressions kan kalde direkte på en variabel.
//                                     //Kig på argument/identifiername/identifiertoken og compare til secert --- KIG PÅ HÅNDTERING BAGEFTER
                                                            
//                                 //find first parent which is var dec
//                                 //get all idtokens inside var dec
//                                 //foreach id token 
//                                     //if id token == current readInside value = counter++
//                                     //dataflowAnalysis(trees, idtoken, counter)

//                                 // var usages = method.DescendantNodes() 
//                                 //     .OfType<VariableDeclaratorSyntax>()
//                                 //     // .Select(t => t.Identifier)
//                                 //     .ToList();

//                                 // foreach (var use in usages)
//                                 // {
//                                 //     Console.WriteLine("usage of variable: " + use.Parent);//.Parent.Parent.Parent);
//                                 //     var idTokens = use.DescendantTokens()
//                                 //         .Where(t => t.IsKind(SyntaxKind.IdentifierToken))
//                                 //         .ToList();
//                                 //     foreach(var id in idTokens)
//                                 //     {
//                                 //         Console.WriteLine("id tokens inside this variable declaration: " + id);
//                                 //     }
//                                 // }   
//                             // }

//                         // }
//                     // }
//                     // var symbol = semanticModel.GetSymbolInfo(readInside).Symbol;
//                     // symbol.Name.EndsWith(secret);
//                     // if (readInside.Contains(secret))
//                     // {
//                     //     Console.WriteLine("YES!");
//                     // }
//                     // else
//                     // {
//                         // foreach ri in readInside
//                             //what variables are inside their decleration?
//                             //dataFlowAnalysis(trees, insideVar)
//                         // foreach
//                     // }
//                 // }
//                 // foreach (var id in identifiers)
//                 // {
//                 //     var symbol = semanticModel.GetSymbolInfo(id).Symbol;

//                 //     if (symbol is IFieldSymbol)
//                 //     {
//                 //         Console.WriteLine("Field used: " + symbol.Name);
//                 //     }
//                 // }

               

// // IDE FRA CHATTEN:
//                 // foreach (var identifier in identifiers){
//                 //     if (semanticModel == null || identifier == null)
//                 //     {
//                 //         Console.WriteLine("Ooopps");
//                 //         return;
   
//                 //     }
//                 //     var symbol = semanticModel.GetSymbolInfo(identifier).Symbol;
//                     // Console.WriteLine("We're on this one: "+ symbol);
                                        
//                     // if (symbol != null && symbol.Name != null && symbol.Name.EndsWith(secret)) //Ends with to take into considderation prefixes, but SHOULD be EXACTLY this secret at the end of declaration (e.g. class fields, global variables startign with class.secret)
//                     // {
//                     //     switch (symbol)
//                     //     {
//                     //         case ILocalSymbol local:
//                     //             // local variable → use dataflow
//                     //             Console.WriteLine("stymbol {0} is a local variable", symbol);
//                     //             break;

//                     //         case IFieldSymbol field:
//                     //             Console.WriteLine("stymbol {0} is a field variable", symbol);
//                     //             //find ud af hvor dens "parent" bliver brugt og kald metoden igen
//                     //                 //send evt. counter med der resetter når man finder en ny parent,
//                     //                     // og sæt en guard på som detecter en for høj guard - indikerer man har kørt i cirkel med den samme "parent"



//                     //             // class/global variable → go to declaration
//                     //             // var whereDidItGo = method.DescendantNodes().OfType<IdentifierNameSyntax>()
//                     //             //     .ToList();

//                     //             // foreach(var wherego in whereDidItGo)
//                     //             // {
//                     //             //     if(semanticModel.GetSymbolInfo(wherego).Symbol.Name.EndsWith(secret))
//                     //             //     {
//                     //             //         // symbol.Name.EndsWith(secret)
//                     //             //         Console.WriteLine(":))))");
//                     //             //     }
//                     //             //     Console.WriteLine("Where did it go? " + wherego);
//                     //             // }
//                     //             // Console.WriteLine(":");
//                     //             // var usages = method.DescendantTokens()
//                     //             //     .Where(t => t.IsKind(SyntaxKind.IdentifierToken))
//                     //             //     .Where(t => semanticModel.GetSymbolInfo(t).Symbol.Equals(symbol))
//                     //             //     // {
//                     //             //     //         var sym = semanticModel.GetSymbolInfo(t).Symbol;
//                     //             //     //         return sym != null && sym.Equals(symbol);
//                     //             //     //     })
//                     //             //     .ToList();
//                     //             // var usages = method.DescendantTokens()
//                     //             //     .Where(t => t.IsKind(SyntaxKind.IdentifierToken))
//                     //             //     .Where(t =>
//                     //             //     {
//                     //             //         var node = t.Parent as IdentifierNameSyntax;
//                     //             //         if (node == null)
//                     //             //             return false;

//                     //             //         var sym = semanticModel.GetSymbolInfo(node).Symbol;
//                     //             //         return sym != null && SymbolEqualityComparer.Default.Equals(sym, symbol);
//                     //             //     })
//                     //             //     .ToList();

//                     //             // var whereDidItGo = usages.Parent
//                     //             //     .Where(t => t.FirstAncestorOrSelf<VariableDeclaratorSyntax>())
//                     //             //     .DescendantNodes()
//                     //             //     .Where(t => t.OfType<IdentiferNameSyntax>())
//                     //             //     .FirstOrDefault();

//                     //             // var whereDidItGo = usages
//                     //             //     .Select(t => t.Parent) // move from token → node
//                     //             //     .Where(node => node != null)
//                     //             //     .Select(node => node.FirstAncestorOrSelf<VariableDeclaratorSyntax>())
//                     //             //     .DescendantNodes().OfType<IdentifierNameSyntax>()
//                     //             //     .FirstOrDefault();
                                
//                     //             // Console.WriteLine(whereDidItGo);
//                     //             // foreach (var use in usages)
//                     //             // {
//                     //             //     Console.WriteLine(use);
//                     //             // }


//                     //             // if(symbol ! in read inside dataflow analyse) -- Så vil vi gerne finde hvor den er instantieret, og så kan man bare køre der ud af:
//                     //             // var usages = method.DescendantNodes()
//                     //             //         .OfType<IdentifierNameSyntax>()
//                     //             //         .Where(x => //semanticModel.GetSymbolInfo(x).Symbol.Equals(symbol))
//                     //             //         {
//                     //             //             var sym = semanticModel.GetSymbolInfo(x).Symbol;
//                     //             //             return sym != null && sym.Equals(symbol);
//                     //             //         }
//                     //             //         )
//                     //             //         .ToList();
//                     //             // // var firstUsage = usages.FirstOrDefault();

//                     //             // var whereDidItGo = usages
//                     //             //     .Select(t => t.FirstAncestorOrSelf<IdentifierNameSyntax>().Identifier)
//                     //             //     .ToList();
                                    
                    
//                     //             // // Console.WriteLine(whereDidItGo);
//                     //             // foreach (var wig in whereDidItGo)
//                     //             // {
//                     //             //     Console.WriteLine("first use declared variable: " + wig);
//                     //             // }


//                     //             break;


//                     //         case IParameterSymbol param:
//                     //             Console.WriteLine("stymbol {0} is a what on earth this is variable", symbol);
//                     //             // method parameter → trace call sites (optional)
//                     //             break;

//                     //         case IPropertySymbol prop:
//                     //             Console.WriteLine("stymbol {0} is a what on earth this is variable", symbol);
//                     //             // property → analyze getter/setter
//                     //             break;
//                     //     }
//                     // }
//                 // }


//                 // For all variables that are declared
//                 // Where are they used? 
//                     //are they used in other declared variabe things? Then look for this too


//                 // is secret used in current AST?





//                 // foreach (var method in methods)
//                 // {
//                 //     Console.WriteLine("Starting analysis on a method");
//                 //     var dataFlow = semanticModel.AnalyzeDataFlow(method.Body); //do the actual dataflow analysis on current tree in current method
//                 //     if (dataFlow.Succeeded)
//                 //     {
//                 //         Console.WriteLine("Data flow analysis succeeded");
//                 //         var temp = dataFlow.ReadOutside;
//                 //         Console.WriteLine("Length: " + temp.Length);
//                 //         if (temp.Length > 0){
//                 //             foreach (var t in temp)
//                 //             {
//                 //                 Console.WriteLine("variable found: " + t);
//                 //                 // dataFlow.ReadInside
//                 //                 // dataFlow.WrittenInside
//                 //                 // dataFlow.VariablesDeclared
//                 //                 // dataFlow.CapturedInside
//                 //                 // dataFlow.UsedLocalFunctions

//                 //             }
//                 //         }
//                 //         Console.WriteLine("");
//                 //     }
//                 // }
//             // }
//         // }







//             // try{ // reimplement - catches files that dont have methods for the dataflow analysis
//                 //get the compilation
//                 // var compilation = CSharpCompilation.Create("MyAnalysis")
//                 //     .AddSyntaxTrees(tree)
//                 //     .AddReferences(
//                 //         MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
//                 //     );

//                 // var semanticModel = compilation.GetSemanticModel(tree); //make semantic model using compilation and syntax tree
//                 // SyntaxNode root = tree.GetRoot();   
                    
//                 // var methods = root.DescendantNodes() //find the first method declaration to do the analysis on -- sheit for hvad hvis der er flere metoder i et programm?????
//                 //     .OfType<MethodDeclarationSyntax>()
//                 //     .ToList();
//                 //     // .First();
                
//                 // foreach(var method in methods)
//                 // {
//                 //     // dataFlowAnalysisOnMethod(method);
//                 //     var dataFlow = semanticModel.AnalyzeDataFlow(method.Body); //do the actual dataflow analysis
//                 //     // var dataFlow = semanticModel.AnalyzeDataFlow(nameSpace); //do the actual dataflow analysis
//                 //         //StatementSyntax or an ExpressionSyntax or a ConstructorInitializerSyntax or a PrimaryConstructorBaseTypeSyntax.
//                 //         //MemberDeclarationSyntax

//                 //     if (dataFlow.Succeeded)
//                 //     {
//                 //         //If i find any of the input secrets then see where they are read and save via read inside

//                 //         Console.WriteLine(":)");
//                 //         // Console.WriteLine("Dataflow analysis succeeded :)");
//                 //         var temp = dataFlow.ReadInside;
//                 //         Console.WriteLine("Length: " + temp.Length);
//                 //         if (temp.Length > 0){
//                 //             foreach (var t in temp)
//                 //             {
//                 //                 Console.WriteLine("variable found: " + t);
//                 //             }
//                 //         }
//                 //     }
//                 // }
//                 // Console.WriteLine("----");

//                 // var nameSpace = root.DescendantNodes() //find the first namespace declaration to do the analysis on
//                 //     .OfType<NamespaceDeclarationSyntax>()
//                 //     .First();

//             // }
//             // catch
//             // {
//             //     Console.WriteLine("we catching - because the file does not contain a method or what now we are going to be looking into");
//             // }
//         // }
//         public void dataFlowAnalysisOnMethod(MethodDeclarationSyntax method)
//         {
//             // var dataFlow = semanticModel.AnalyzeDataFlow(method.Body); //do the actual dataflow analysis
//             // // var dataFlow = semanticModel.AnalyzeDataFlow(nameSpace); //do the actual dataflow analysis
//             //     //StatementSyntax or an ExpressionSyntax or a ConstructorInitializerSyntax or a PrimaryConstructorBaseTypeSyntax.
//             //     //MemberDeclarationSyntax

//             //     if (dataFlow.Succeeded)
//             //     {
//             //         Console.WriteLine(":)");
//             //         // Console.WriteLine("Dataflow analysis succeeded :)");
//             //         // Console.WriteLine("Length: " + dataFlow.ReadOutside.Length);
//             //         // if (dataFlow.ReadOutside.Length > 0){
//             //         //     foreach (var location in dataFlow.ReadOutside)
//             //         //     {
//             //         //         Console.WriteLine(":O");
//             //         //         Console.WriteLine(location);
//             //         //     }
//             //         // }
//             //     }
//             //     else
//             //     {
//             //         Console.WriteLine(":(");
//             //     }
//         }

//         //Funktion med input(AST, variabelToTrace)
//             //Go through AST and find all source points/uses for that variable
//             //Return that list - this list will be used in PLACEANALYSIS
    
    
//         // var semanticModel = context.SemanticModel;
//         // var dataFlow = semanticModel.AnalyzeDataFlow(syntax);
//         // if (dataFlow.Succeeded)
//         // {
//         //     // dataFlow.ReadInside
//         //     // dataFlow.WrittenInside
//         //     // dataFlow.VariablesDeclared
//         //     // dataFlow.CapturedInside
//         //     // dataFlow.UsedLocalFunctions
//         //     // etc.
//         // }


//         //CONTROLFLOW ANALYSIS -- KAN VISE HVOR NOGET EXITER I KODE
//         // var semanticModel = context.SemanticModel;
//         // var controlFlow = semanticModel.AnalyzeControlFlow(statementSyntax);
//         // if(controlFlow.Succeeded)
//         // {
//         //     // controlFlow.EntryPoints
//         //     // controlFlow.ExitPoints
//         //     // controlFlow.StartPointIsReachable
//         //     // controlFlow.EndPointIsReachable
//         //     // controlFlow.ReturnStatements
//         // }


    
//     }
// }