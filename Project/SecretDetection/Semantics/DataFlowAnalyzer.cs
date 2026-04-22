using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Project.SecretDetection.Semantics{
    class DataFlowAnalyzer
    {
        public List<SyntaxToken> dataflowAnalysis(List<SyntaxTree> trees, List<SyntaxToken> idTokens, int counter)
        {
            //we look into all id tokens -- possible that more are added
            //then for each id token we look through all trees
            //We need to find the idtoken in the tree
            //then check how its used
            //Based on how its used its handled in switch

            if (counter == 1) //stop klods på rekursions dybde
            {
                return idTokens;
            }

            List<SyntaxToken> foundInTree = getIdTokenInTree(trees, idTokens);
            List<SyntaxToken> newFinds = howIsVariableUsed(trees, foundInTree);
            if(newFinds == foundInTree)
            {
                return newFinds; //nothing new to check
            }
            else
            {
                return dataflowAnalysis(trees, idTokens, 1);
            }

            //Kør den videre
            //retuner newFinds
            //sammenlign med input idtokens, hvis der ikke er nogle nye, så returner newFinds
                //Ellers så kører vi dataflow analyse igen med newFinds

            // if(we)
            //     dataflowAnalysis(trees, idTokens+newOne);
            return idTokens;
        }
        public List<SyntaxToken> getIdTokenInTree(List<SyntaxTree> trees, List<SyntaxToken> idTokens) // RETHINK THiS METHOD
        {
            List<SyntaxToken> foundInTree = new List<SyntaxToken>();
            foreach (var idToken in idTokens){ // To.List makes a copy to safely iterate -- Gør rekursivt på et tidspunkt i stedet 
                for (int i = 0; i < trees.Count; i++)
                    {
                        SyntaxNode root = trees[i].GetRoot(); //SKAL VÆRE i
                        var matchingTokens = root.DescendantTokens()
                            .Where(t => t.IsKind(SyntaxKind.IdentifierToken) &&
                                        t.Text == idToken.Text)
                            .ToList();
                        
                        foundInTree.AddRange(matchingTokens);
                    }
            }
            return foundInTree;
        }
        
        public List<SyntaxToken> howIsVariableUsed(List<SyntaxTree> trees, List<SyntaxToken> idTokens)//, SyntaxNode node)
        {
            //is variable part of an invocationmethod?
                //send on to how to save the new identificationTokens according to method handling
            // is variable part of ...
            foreach(SyntaxToken node in idTokens){
                switch (node.Parent)
                {
                    case MemberAccessExpressionSyntax memberAccess:
                        Console.WriteLine($"Token {node} is part of a member access: {memberAccess}");
                        return new List<SyntaxToken>();
                    case InvocationExpressionSyntax invocation:
                        return new List<SyntaxToken>(); 
                    case VariableDeclaratorSyntax variableDeclarator:
                        return new List<SyntaxToken>(); 
                    case AssignmentExpressionSyntax assignment:
                        return new List<SyntaxToken>(); 
                    case ParameterSyntax parameter:
                        return new List<SyntaxToken>(); 
                    default:
                        return new List<SyntaxToken>();
                }
            }
            return new List<SyntaxToken>(); 
        }
    }
}