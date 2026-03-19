using System;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Project.SecretDetection.SecretsAnalysis.APIKeyVariants;


namespace Project.SecretDetection.SecretsAnalysis{
    public class APIKeyDetector: Detector
    {
        public int score;
        public override int detect(string secret)
        {
            doesItLookLikeAPIKey(secret);
            return 0;
        }

        public void doesItLookLikeAPIKey(string secret)
        {
            var jwt = new JWTDetector();
            jwt.isItJWT(secret);
        } 
    }
}