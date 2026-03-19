using System;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Project.SecretDetection.SecretsAnalysis.APIKeyVariants{
    public abstract class APIVariant
    {
        public string apiType { get; set;}
        public abstract bool isItAPI(string secret); //used to detect if it is a specific type of API key
    }
}