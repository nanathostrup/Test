using System;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Project.SecretDetection.SecretsAnalysis.APIKeyVariants{
    public class JWTDetector
    {
        public void isItJWT(string secret)
        {
            Console.WriteLine(secret);
            //Does the string follow the format?
            try // Prøv på at inddele secreten i formaten og se om det følger
            {
                //Header: Usually consists of two things - secifies the token type (JWT), and the signing algorithm
                //Payload: contains the "claims"
                string header = secret.Split('.')[0];
                string payload = secret.Split('.')[1];
                string signature = secret.Split('.')[2]; // Her kan vi allerede se at det måske godt kunne følge en format. Sus goes up
                Console.WriteLine(header);
                Console.WriteLine(payload);
                Console.WriteLine(signature);

                //Sæt en guard ind der kun leder efter 3 punktumer no more no less
                var base64Detector = new Base64Detector(); 
                // .isItBase64



                // Er header base64?
                // Er payload base64?
                // er hashet(encoded header + encoded payload) == signature? --- Måske ikke muligt da det ligner der er kommet en secret med!

                // HMACSHA256(
                //     base64UrlEncode(header) + "." +
                //     base64UrlEncode(payload),
                //     secret) ----- pis



            }
            catch //Eller følger det ikke format og ligner ikke en secret
            {
                Console.WriteLine("we catching");                
            }
        }
    }
}