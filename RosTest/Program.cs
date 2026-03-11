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
            Console.WriteLine("     " + file);
            string code = File.ReadAllText(file);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code); // parse code to AST
            trees.Add(tree);
        }

        var walker = new Walker();
        foreach (SyntaxTree tree in trees)
        {
            //Check the current AST for invocation expressions
            SyntaxNode root = tree.GetRoot();
            walker.Visit(root);
        }
        
        //Test at vi får de rigtige ud
        foreach (var stringarg in walker.StringArgs)
        {
            Console.WriteLine("Input to GetEnvironmentVariable(): " + stringarg);
        }
    
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