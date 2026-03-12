using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

class SecretsVerifier
{ 

    //This function relies HEAVILY on the implementation here: https://thesagardahal.medium.com/understanding-shannon-entropy-measuring-randomness-for-secure-code-auditing-4b3c5697a7f9
    //Exact implementation is in python - see python/entrpy.py for their full code implementation
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


}
