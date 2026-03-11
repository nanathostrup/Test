using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

class Program
{  
    public static void Main(String[] args)
    {
        // foreach file
        //     make AST
        
        // Foreach AST
        //     if something accesses env file
        //         check that variable in cs file in env file
        //             is it a string litteral?
       
        string filePath = @"C:\Users\natd\OneDrive - Netcompany\Desktop\test\WeatherSimple";

        var files = Directory.EnumerateFiles(filePath, "*", SearchOption.AllDirectories)
            .Where(f =>
                // exclude obj folder
                !f.Contains(@"\obj\") 
                // skip bin folder
                && !f.Contains(@"\bin\")
                // skip csproj file
                && !f.Contains(@".csproj")
            );


        //Convert each file to an AST
        List<SyntaxTree> trees = new List<SyntaxTree>();
        Console.WriteLine("Files being processed into an AST:");
        foreach (string file in files)
        {
            Console.WriteLine("     *" + file);
            string code = File.ReadAllText(file);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code); // parse code to AST
            trees.Add(tree);
        }
       
        //Travrese trees and look for environment calls
        List<string> PossibleDetections = new List<string>();
        foreach (SyntaxTree tree in trees)
        {
            WalkAST(tree, PossibleDetections);  //SHIT METODE MED 4 FOR LOOPS BUT GIVES ME RIGHT RESULTS
        }

        //For printing each AST
        // foreach (SyntaxTree tree in trees)
        // {
            // Console.WriteLine(" ================================= NEW SYNTAX TREE ================================= ");
            // Get the root of the tree
            // SyntaxNode root = tree.GetRoot();
            // PrintNode(root, 0);
        // }
    }


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

    // TODO: OPTIMIZE!
    static List<string> WalkAST(SyntaxTree tree, List<string> )
    {
        // if invocation expression
            // go to identifier token node
            // if idtoken == GetEnvironmentToken
                // get invocationmethod's argument list childs child - string litteral
                //Print that child child
            //Check the current AST for invocation expressions
            SyntaxNode root = tree.GetRoot();
            // Get all invocation expressions
            var invocations = root
                .DescendantNodes()
                .OfType<InvocationExpressionSyntax>();
            
            foreach (var invocation in invocations)
                {
                    //Extracting the string litteral from decendants
                    // Console.WriteLine("Invocation Expression: " + invocation);   

                    //Extract places where we have an Environment Variable call
                    var identifiertokens = invocation
                        .DescendantTokens()
                        .Where(t => t.IsKind(SyntaxKind.IdentifierToken))
                        .Select(t => t.ValueText);
                    foreach(var identifiertoken in identifiertokens)
                    {
                        if (identifiertoken == "GetEnvironmentVariable")
                        {
                            Console.WriteLine("Invocation Expression, Identifier Token: " + identifiertoken);
                            var stringliterals = invocation
                                .DescendantNodes()
                                .OfType<LiteralExpressionSyntax>()
                                .Where(l => l.IsKind(SyntaxKind.StringLiteralExpression))
                                .Select(l => l.Token.ValueText);
                            
                            foreach (var stringliteral in stringliterals)
                            {
                                Console.WriteLine("Child string litteral: " + stringliteral);
                                PossibleDetections.Add(stringliteral);
                            }
                        }
                    }
                }
            return PossibleDetections;
    }

    // void Walk(SyntaxNode node)
    //     {
    //         Console.WriteLine(node.Kind());
    //         foreach (var child in node.ChildNodes())
    //         {
    //             if (child == "GetEnvitonmentToken")
    //             {
    //                 Console.WriteLine(":)");
    //             }
    //             else
    //             {
    //                 Walk(child);                    
    //             }
    //         }
    //     }


// 
//     public void inorderTraversal(TreeNode root) {
//         if (root != null) {
//             inorderTraversal(root.left);
//             Console.WriteLine(root.data + " ");
//             inorderTraversal(root.right);
//         }
// }



}