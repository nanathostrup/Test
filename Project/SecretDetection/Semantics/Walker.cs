using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Project.SecretDetection.Semantics{
    class Walker : CSharpSyntaxWalker
    {
        //Måske lav til dictionary så vi er sikker på at de hænger sammen på den rigtige måde
        public List<string> StringArgs = new List<string>(); //All string argument inputs to GetEnvVar e.g. x = GetEnvVar(y), this list is for the y's - Maybe find a better way to do this than a global variable
        public List<string> InitializedArgs = new List<string>(); //What the environment variable is initialized as e.g. x = GetEnvVar(y), this list is for the x's 
        public Dictionary<string, string> EnvironmentVariableMap = new();
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

                
                //First parent whta is variable declarator,
                    //Thich child is the name that it is declared as
                
                var initializedas = invocation //Now we want what the variable name is initilialized as (so we can tract the variable with dataflow analysis)
                    .FirstAncestorOrSelf<VariableDeclaratorSyntax>() //Not declaraTION - then we get the entire line which is too much for getting the name its initialized as
                    .Identifier.Text;

                // InitializedArgs.Add(initializedas); //Hvorfor adder jeg til mine global lister på to måder? Genovervej på tidspunkt

                if (arglist.Any())
                {
                    // StringArgs.AddRange(arglist); // add to global variable. This could probably be done better, but works for now
                
                    // StringArgs.AddRange(arglist);
                    foreach (var arg in arglist)
                    {
                        EnvironmentVariableMap[arg] = initializedas;
                    }
                }
            }
            base.VisitInvocationExpression(invocation);

        }
    }
}