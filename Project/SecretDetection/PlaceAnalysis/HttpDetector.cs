using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project.SecretDetection.Semantics;

namespace Project.SecretDetection.PlaceAnalysis{
    public class HttpDetector : PlaceDetector
    {
        public float weight = 0.0F;
        public override float getWeight(List<SyntaxTree> trees)
        {
            var dataflow = new DataFlowAnalyzer();
            // List<SyntaxToken> initAs = whatIsVarInitializedAs(trees, "HttpClient"); //hardcoded because we want to know what a developer has called a predefined HttpClient
                                                        //  could also have been hardcoded into the function below
            
            var initAs = SyntaxFactory.Identifier("_httpClient");//test variable
            dataflow.dataflowAnalysis(trees, initAs);

            return weight;
        }
        
        //Where is the http client initalized? Where do we need to look in the dataflow analysis?
        public List<SyntaxToken> whatIsVarInitializedAs(List<SyntaxTree> trees, string lookFor)
        {
            //Find HttpClient i AST som identificationToken
            //Find hvad den er initialized som?
            
            var results = new List<SyntaxToken>();
            foreach (var tree in trees)
            {
                // Console.WriteLine("New Tree");
                SyntaxNode root = tree.GetRoot(); //get root
                
                //Find where the Identification Tokens are located in AST
                //Save the SyntaxNodes where we find what we are looking for - saved as nodes so its easier for me to work with after this step
                var idTokensSyntaxNodes = root.DescendantTokens()
                    .Where(t => t.IsKind(SyntaxKind.IdentifierToken) &&
                                t.ValueText == lookFor)
                    .Select(t => t.Parent)
                    .Where(n => n != null)
                    .ToList();
                
                // Find out what the Identification Tokens in AST are initialized as
                var initAs = idTokensSyntaxNodes
                    .Select(n => n.FirstAncestorOrSelf<VariableDeclaratorSyntax>())
                    .Where(v => v != null)
                    .Select(v => v.Identifier)
                    .ToList();                    

                foreach (var token in initAs)
                {
                    Console.WriteLine("What is it initialized as?: " + token);
                }
                if (initAs.Any())
                {
                    results.AddRange(initAs);
                }
            }
            return results;

        }
    }
}