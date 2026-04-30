using System;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Project.SecretDetection.SecretsAnalysis{
    public class PasswordDetector : SecretDetector
    {
        public float score = 0.0F;
        public override float detect(string secret)
        {
            score = 0.0F;
            string filepath = Directory.GetCurrentDirectory(); //non-hardcoded
            string mostCommonPasswords = filepath+ @"\SecretDetection\SecretsAnalysis\CommonPasswords\10k-most-common.txt";            

            using (StreamReader reader = new StreamReader(mostCommonPasswords))
            {
                string contents = reader.ReadToEnd();
                if (contents.Contains(secret))
                {
                    return score += 60000.0F;
                }
            }

            return score;
        }    
    }
}