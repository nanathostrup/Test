using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Project.SecretDetection.Semantics{
    class Walker : CSharpSyntaxWalker
    {
        public List<string> StringArgs = new List<string>(); //Maybe find a better way to do this than a global variable

        public override void VisitInvocationExpression(InvocationExpressionSyntax invocation)
            {
            //Does the invocation have a child that is GetEnvVar?
                //No? Return
                //Yes 
                    //get invocation's argumentList and add the string litteral (which is an argument to GetEnvVar) to a list

            bool GetEnvVar = invocation.DescendantTokens()
                    .Any(t => t.IsKind(SyntaxKind.IdentifierToken) 
                                && t.ValueText == "GetEnvironmentVariable");

            if (GetEnvVar)
            {
                var arglist = invocation.ArgumentList //We want the string litteral that is from the argument list to ensure we have the input for the GetEnvironmentVariable().
                    .Arguments
                    .Select(t => t.Expression)
                    .OfType<LiteralExpressionSyntax>()  
                    .Where(t => t.IsKind(SyntaxKind.StringLiteralExpression))
                    .Select(t => t.Token.ValueText)
                    .ToList();

                if (arglist.Any())
                {
                    StringArgs.AddRange(arglist); // add to global variable. This could probably be done better, but works for now
                }
            }
            base.VisitInvocationExpression(invocation);

        }
    }
}