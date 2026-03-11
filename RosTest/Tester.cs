// using System;
// using System.IO;
// using Microsoft.CodeAnalysis;
// using Microsoft.CodeAnalysis.CSharp;
// using Microsoft.CodeAnalysis.CSharp.Syntax;

// class Program1
// {
//     static void Main1()
//     {
//         // Path to the C# file you want to analyze
//         // string filePath = @"C:\Users\natd\OneDrive - Netcompany\Desktop\test\WeatherSimple\Services\WeatherServices.cs";
//         string filePath = @"C:\Users\natd\OneDrive - Netcompany\Desktop\test\WeatherSimple\Services\WeatherSimple.env";
//         // string filePath = @"C:\Users\natd\OneDrive - Netcompany\Desktop\test\RosTest\Test.cs";
//         //  string filePath = @"C:\Users\natd\OneDrive - Netcompany\Desktop\test\PasswordAuthentication\pwauth.cs";


//         if (!File.Exists(filePath))
//         {
//             Console.WriteLine("File not found: " + filePath);
//             return;
//         }

//         // Read the code from the file
//         string code = File.ReadAllText(filePath);

//         // Parse the code into a SyntaxTree
//         SyntaxTree tree = CSharpSyntaxTree.ParseText(code);

//         // Get the root node
//         SyntaxNode root = tree.GetRoot();

//         // Print the AST
//         PrintNode(root, 0);


//         //Pure strings uden noget
//         var strings = root
//             .DescendantNodes()
//             .OfType<LiteralExpressionSyntax>()
//             .Where(x => x.IsKind(SyntaxKind.StringLiteralExpression))
//             .Select(x => x.Token.ValueText);
//         // foreach (var s in strings)
//         // {
//         //     Console.WriteLine(s);
//         // }

//         // Strings i mellem ting
//         var texts = root
//             .DescendantNodes()
//             .OfType<InterpolatedStringTextSyntax>()
//             .Select(x => x.TextToken.ValueText);
//         // foreach (var t in texts)
//         // {
//         //     Console.WriteLine(t);
//         // }

//         var identifiertokens = root
//             .DescendantTokens()
//             .Where(t => t.IsKind(SyntaxKind.IdentifierToken))
//             .Select(t => t.ValueText);

//         // foreach (var i in identifiertokens)
//         // {
//         //     Console.WriteLine(i);
//         // }
//     }


//     static void PrintNode(SyntaxNode node, int indent)
//     {
//         var padding = new string(' ', indent * 2);
//         Console.WriteLine($"{padding}{node.Kind()}");

//         // Print tokens with values
//         foreach (var token in node.ChildTokens())
//         {
//             Console.WriteLine($"{padding}  TOKEN {token.Kind()} : {token.ValueText}");
//         }

//         foreach (var child in node.ChildNodes())
//         {
//             PrintNode(child, indent + 1);
//         }
//     }
    


//     // PRINT UDEN TOKEN
//     // static void PrintNode(SyntaxNode node, int indent)
//     // {
//     //     Console.WriteLine($"{new string(' ', indent * 2)}{node.Kind()}");

//     //     foreach (var child in node.ChildNodes())
//     //     {
//     //         PrintNode(child, indent + 1);
//     //     }
//     // }
// }