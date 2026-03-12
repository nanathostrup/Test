using System;
using System.ComponentModel;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

class AST
{
    public List<SyntaxTree> createASTs(string filePath){
        
        List<SyntaxTree> trees = new List<SyntaxTree>(); 
        var files = Directory.EnumerateFiles(filePath, "*", SearchOption.AllDirectories) //Get a hold of all relevant files in the directory provided
                .Where(f =>
                    // exclude obj folder
                    !f.Contains(@"\obj\") 
                    // skip bin folder
                    && !f.Contains(@"\bin\")
                    // skip csproj file
                    && !f.Contains(@".csproj")
                );

        Console.WriteLine("Files being processed into an AST:");//to see what files are being processed, just to make my life easier
            foreach (string file in files)
            {
                Console.WriteLine("     " + file); 
                string code = File.ReadAllText(file);
                SyntaxTree tree = CSharpSyntaxTree.ParseText(code); // parse code to AST
                trees.Add(tree);
            }
        return trees;

        }
}