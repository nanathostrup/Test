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
        public List<SyntaxToken> dataflowAnalysis(List<SyntaxTree> trees, List<SyntaxToken> idTokens)//, int counter)
        {
            //we look into all id tokens -- possible that more are added
            //then for each id token we look through all trees
            //We need to find the idtoken in the tree
            //then check how its used
            //Based on how its used its handled in switch
            Console.WriteLine("");
            Console.WriteLine("DataFlowAnalyzer entered");
            foreach(var id in idTokens)
            {
                Console.WriteLine(id);
            }

            List<SyntaxToken> foundInTrees = getIdTokenInTree(trees, idTokens);
            List<SyntaxToken> newFinds = new List<SyntaxToken>();
            foreach(var id in foundInTrees) //Not sure I want it handled like this
            {
                newFinds.AddRange(howIsVariableUsed(trees, foundInTrees, id.Parent));
            }
            // List<SyntaxToken> newFinds = howIsVariableUsed(trees, foundInTrees);
            newFinds.AddRange(idTokens);
            List<SyntaxToken> newNewFinds = newFinds.Distinct().ToList();

            var onlyInFirst = newNewFinds.Except(idTokens);
            var onlyInSecond = idTokens.Except(newNewFinds);
            bool areEqual = !onlyInFirst.Any() && !onlyInSecond.Any(); //Filter out the repeat tokens

            foreach(var n in newFinds)
            {
                Console.WriteLine(n);
            }

            if(areEqual) // Checker om de to lister er ens
            {
                Console.WriteLine("new=id");
                return idTokens; //Stop klods
            }
            else
            {
                Console.WriteLine("new!=id");
                return dataflowAnalysis(trees, newNewFinds); //rekursivt kald for at analysere den nye liste af id tokens
            }
        }
        public List<SyntaxToken> getIdTokenInTree(List<SyntaxTree> trees, List<SyntaxToken> idTokens) // RETHINK THiS METHOD
        {
            List<SyntaxToken> foundInTree = new List<SyntaxToken>();
            foreach (var idToken in idTokens){
                for (int i = 0; i < trees.Count; i++)
                    {
                        SyntaxNode root = trees[i].GetRoot();
                        var matchingTokens = root.DescendantTokens()
                            .Where(t => t.IsKind(SyntaxKind.IdentifierToken) &&
                                        t.Text == idToken.Text)
                            .ToList();
                        
                        foundInTree.AddRange(matchingTokens);
                    }
            }
            return foundInTree;
        }
        
        public List<SyntaxToken> howIsVariableUsed(List<SyntaxTree> trees, List<SyntaxToken> idTokens, SyntaxNode node)
        {
            //is variable part of an invocationmethod?
                //send on to how to save the new identificationTokens according to method handling
            // is variable part of ...
            switch (node)
            {
                case MemberAccessExpressionSyntax memberAccess:
                    Console.WriteLine($"Token {node} is part of a member access: {memberAccess}");
                    return memberAccessHandler(trees, idTokens, node);
                case InvocationExpressionSyntax invocation:
                    Console.WriteLine($"Token {node} is part of a member access: {invocation}");
                    return invocationHandler(trees, idTokens, node);
                case VariableDeclaratorSyntax variableDeclarator:
                    Console.WriteLine($"Token {node} is part of a member access: {variableDeclarator}");
                    return variableDeclaratorHandler(trees, idTokens, node);
                case AssignmentExpressionSyntax assignment:
                    return new List<SyntaxToken>(); 
                case ParameterSyntax parameter:
                    return new List<SyntaxToken>(); 
                default:
                    Console.WriteLine($"NOPE: Token {node} is not part of any switch case");
                    if (node.Parent != null){
                        return howIsVariableUsed(trees, idTokens, node.Parent); //Not sure if this should be done like this?
                    }
                    return new List<SyntaxToken>();
            }
        }

        public List<SyntaxToken> memberAccessHandler(List<SyntaxTree> trees, List<SyntaxToken> idTokens, SyntaxNode node)
        {
            bool parentIsInvocation = node.Parent is InvocationExpressionSyntax;
            if (parentIsInvocation)
            {
                return invocationHandler(trees, idTokens, node.Parent);
            }
            //HANDLE OTHER CASES OF THIS INSTANCE
            return idTokens;
        }
        public List<SyntaxToken> invocationHandler(List<SyntaxTree> trees, List<SyntaxToken> idTokens, SyntaxNode node)
        {
            //find the idtokens that are not already in idTokens (get only new instances)
            //return that list

            var newIdTokens = node.DescendantTokens()
                .Where(t => t.IsKind(SyntaxKind.IdentifierToken) && !idTokens.Contains(t))
                .ToList();

            return newIdTokens;
                        //Lavet rekursions venlig med en ny liste der bliver returneret, hvis man tilføjer til idTokens, og returnerer den er den ikke rekursions venlig altså den vil ikke kunne se der er sket noget nyt forrest i rekursionen.
        }
        
        public List<SyntaxToken> variableDeclaratorHandler(List<SyntaxTree> trees, List<SyntaxToken> idTokens, SyntaxNode node)
        {
            var newIdTokens = node.DescendantTokens()
                .Where(t => t.IsKind(SyntaxKind.IdentifierToken) && !idTokens.Contains(t)) // && t.ValueText != "city") //city is outcommented for now - to make debugging easier
                .ToList();

            foreach(var nidt in newIdTokens)
            {
                Console.WriteLine(nidt);
            }
            return newIdTokens;
        }
    }
}