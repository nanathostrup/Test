using System;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Project.SecretDetection.Secrets{
    class SecretsVerifier
    { 

        //This function is a translation of the function here: https://thesagardahal.medium.com/understanding-shannon-entropy-measuring-randomness-for-secure-code-auditing-4b3c5697a7f9
        //Exact implementation is in python - see python/entrpy.py for their full code implementation
        //Tyyyyv stjålet :)
        public double ShannonEntropy(string str)
        {
            if (str == null)
            {
                return 0.0F; //Niks du
            }

            //Count the frequency of each character in the string
            var freq = new Dictionary<char, int>();
            foreach (char ch in str)
            {
                freq[ch] = freq.GetValueOrDefault(ch, 0) + 1;
            }

            double entropy = 0.0;
            double totalLength = str.Length;

            //Calculate the probability of each character and sum the entropy terms
            foreach (double count in freq.Values)
            {
                double probability = count/totalLength;
                entropy -= probability * Math.Log2(probability);
            }

            return entropy;
        }
        public bool isItHex(string str)
        {
            List<char> validChars = new List<char>() {'a','b','c','d','e','f','0','1','2','3','4','5','6','7','8','9'}; //Could probably be done smarter
            //Check if each char in str is from 0-9 or a-f --MANGLER A-F??
            foreach (char ch in str)
            {
                if (!validChars.Contains(ch))
                {
                    return false;
                }
            }
            return true; //none of the chars were caught as invalid, so must be valid. Jeniousity
        }
        public bool isItBase64(string str)
        {
            try
            {
                byte[] data = Convert.FromBase64String(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}