//LAV EN ABSTRACT CLASS SOM KAN UDVIDES SENERE

// SKAL HAVE EN FIELD = SCORE
//Når man bruger den skal den tilføjes til en samlet score som afgør hvor kritisk en detection er


using System;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Project.SecretDetection.Secrets{
    public abstract class Detector
    {
        public int score {get; set;}
        public abstract int detect(string secret);  //used to detect if the string is a secret
                                                    //should return the score - figure this out
    }
}