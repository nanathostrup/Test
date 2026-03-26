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
        public int score = 0;
        public string apiType = "NONE";
        public override int detect(string secret)
        {
            score = 0;
            if (doesItLookLikeAPIKey(secret))
            {
                score += 40000;
                // return 1;
            }
            return score;
            // return 0;
        }

        public bool doesItLookLikeAPIKey(string secret) 
        {
            apiType = "NONE";
            var jwt = new JWTDetector();
            bool looskLikeJWT = jwt.isItAPI(secret);
            if (looskLikeJWT)
            {
                apiType = "a JWT secret token";
                return true;
            }
            return false;
        } 
    }
}