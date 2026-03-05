using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

class Program
{
    static void Main()
    {
        // Path to the C# file you want to analyze
        // string filePath = @"C:\Users\natd\OneDrive - Netcompany\Desktop\test\\WeatherSimple\";
        string filePath = @"C:\Users\natd\OneDrive - Netcompany\Desktop\test\RosTest\Test.cs";


        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found: " + filePath);
            return;
        }

        // Read the code from the file
        string code = File.ReadAllText(filePath);

        // Parse the code into a SyntaxTree
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);

        // Get the root node
        SyntaxNode root = tree.GetRoot();

        // Print the AST
        PrintNode(root, 0);
    }

    static void PrintNode(SyntaxNode node, int indent)
    {
        Console.WriteLine($"{new string(' ', indent * 2)}{node.Kind()}");

        foreach (var child in node.ChildNodes())
        {
            PrintNode(child, indent + 1);
        }
    }
}