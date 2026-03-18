using System;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Project.SecretDetection.Secrets{
    class Tester
    { 
        public void EntropyTester()
        {
            var secretsVerifier = new SecretsVerifier();
            Console.WriteLine("");
            Console.WriteLine(" ============================= BOUNDING ENTROPY ============================== ");
            double e = secretsVerifier.ShannonEntropy("ea413b8c6e9657e69c24ca2b83e6d895");
            Console.WriteLine("Entropy of \"ea413b8c6e9657e69c24ca2b83e6d895\":                             " + e);
            double e1 = secretsVerifier.ShannonEntropy("password");
            Console.WriteLine("Entropy of \"password\":                                                     " + e1);
            double e2 = secretsVerifier.ShannonEntropy("api_XweVmYIoqSCHxVOb4Q6C1zMFs0O92zPu");
            Console.WriteLine("Entropy of \"api_XweVmYIoqSCHxVOb4Q6C1zMFs0O92zPu \":                        " + e2);
            double e3 = secretsVerifier.ShannonEntropy("AKIAIOSFODNN73XAMPL3");
            Console.WriteLine("Entropy of \" AKIAIOSFODNN73XAMPL3\":                                        " + e3);
            double e4 = secretsVerifier.ShannonEntropy("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");
            Console.WriteLine("Entropy of \"wJalrXUtnFEMI/K7MDENG/bPxRfiCY3XAMPL3K3Y\":                     " + e4);
            double e5 = secretsVerifier.ShannonEntropy("IQoJb3JpZ2luX2VjEFAaCXVzLXdlc3QtMiJIMEYCIQDFAK3");
            Console.WriteLine("Entropy of \"IQoJb3JpZ2luX2VjEFAaCXVzLXdlc3QtMiJIMEYCIQDFAK3 \":             " + e5);
            double e6 = secretsVerifier.ShannonEntropy("IQoJb3JpZ2luX2VjEDoaCXVzLWVhc3QtMiJIMEYCIQDQh4gelDqno96q39RwiPT5x7K7SyVOSmeDpUMd9SthWAIhAP5tT81Cb+Rb2zN85delmYB4KECmW1uL7Tr36C/M2GaJKr0DCKP//////////wEQARoMNjY2MzU5NzY0NTI4Igyu9F2yAqZN3dG0q9YqkQMVrg/4mCJjDxg0QmplU581Z2P8LGhGfr9vgei6SaONhhfks5Kt9Ikbh61G9UiQ3SXgPLbHjOfTUueaIIcBz1Y3LcW+WajtfsGfB8CqT76lkJLtkvl+1KjSCVn6k+/K/iWgr3Zc1Ej+qT2djTH4x1OWFNS6i6iCtlUy/Z6i3P2fziHGsEmafkH3ict+07dFb3DA2aRnUhnaCHfQDNd/5ub70oILwB4UgtgGNkbM9SE/NxKgPZY9qIktYifqcgfDyYMYHlvY9XEc0UT2jfaQKDYVgMCdsdsW5mkoBYzLRisQhKxjfwaBpkRtdW8dEHFAG04eV4JSAbOSat3bgUwahATGizOdsMz/qhnS9qzShQGgSR6OU6pDDUtuHCGh0sgwrjsZ+bGDfzkw5Sy3JhjQpozfinCsAmDZ1t3nX6llw9OR9B2mdDHCeccsWGwjIvmprs21FtgjDuKGzaAET6HgQAR+pkFUgxBWVmZArtck1ziG21FEN8pFR75rOgxSkQ3yEZeDZkIIZ/aJnABGvbC3Fbq9ATD6ycuKBjqlAaGPeFKzdCR1dBh4sHQVHejXNegWWZV72n4MLyZx2FE9wLUfPGXXW+pYZg4SySvN0Z4OnGoYdlO/pjKvdRa507mSD8N8EhkwgpJMatFobJb0hsz7GY5flutVSkDfBDYkU91vpl7YCJ5rlvuR0I6iWe+K7smYj5hzm16YokWsRQ4EeWHo0peEJuqTZrZt/U4gHVsFpG44V8Yb6iRdZL78E+5xcgjeFw==");
            Console.WriteLine("Entropy of \" IQoJb3JpZ2luX2VjEDoaCXVzLWVhc3QtMiJIMEYCIQDQh4gelDqno96q39RwiPT5x7K7SyVOSmeDpUMd9SthWAIhAP5tT81Cb+Rb2zN85delmYB4KECmW1uL7Tr36C/M2GaJKr0DCKP//////////wEQARoMNjY2MzU5NzY0NTI4Igyu9F2yAqZN3dG0q9YqkQMVrg/4mCJjDxg0QmplU581Z2P8LGhGfr9vgei6SaONhhfks5Kt9Ikbh61G9UiQ3SXgPLbHjOfTUueaIIcBz1Y3LcW+WajtfsGfB8CqT76lkJLtkvl+1KjSCVn6k+/K/iWgr3Zc1Ej+qT2djTH4x1OWFNS6i6iCtlUy/Z6i3P2fziHGsEmafkH3ict+07dFb3DA2aRnUhnaCHfQDNd/5ub70oILwB4UgtgGNkbM9SE/NxKgPZY9qIktYifqcgfDyYMYHlvY9XEc0UT2jfaQKDYVgMCdsdsW5mkoBYzLRisQhKxjfwaBpkRtdW8dEHFAG04eV4JSAbOSat3bgUwahATGizOdsMz/qhnS9qzShQGgSR6OU6pDDUtuHCGh0sgwrjsZ+bGDfzkw5Sy3JhjQpozfinCsAmDZ1t3nX6llw9OR9B2mdDHCeccsWGwjIvmprs21FtgjDuKGzaAET6HgQAR+pkFUgxBWVmZArtck1ziG21FEN8pFR75rOgxSkQ3yEZeDZkIIZ/aJnABGvbC3Fbq9ATD6ycuKBjqlAaGPeFKzdCR1dBh4sHQVHejXNegWWZV72n4MLyZx2FE9wLUfPGXXW+pYZg4SySvN0Z4OnGoYdlO/pjKvdRa507mSD8N8EhkwgpJMatFobJb0hsz7GY5flutVSkDfBDYkU91vpl7YCJ5rlvuR0I6iWe+K7smYj5hzm16YokWsRQ4EeWHo0peEJuqTZrZt/U4gHVsFpG44V8Yb6iRdZL78E+5xcgjeFw==\":            " + e6);
            double e7 = secretsVerifier.ShannonEntropy("AIzaSyDaGmWKa4JsXZ-HjGw7ISLn_3namBGewQe");
            Console.WriteLine("Entropy of \"AIzaSyDaGmWKa4JsXZ-HjGw7ISLn_3namBGewQe\":                      " + e7);
            double e8 = secretsVerifier.ShannonEntropy("sk_live_C8YfyXfzocnRZNE36yzd7Pg3Wl0aqCad");
            Console.WriteLine("Entropy of \"sk_live_C8YfyXfzocnRZNE36yzd7Pg3Wl0aqCad\":                     " + e8);
            double e9 = secretsVerifier.ShannonEntropy("sk_live_skfikf5682lfjas896dsndhfuek9hy654");
            Console.WriteLine("Entropy of \" sk_live_skfikf5682lfjas896dsndhfuek9hy654\":                   " + e9);
            double e10 = secretsVerifier.ShannonEntropy("pk_t3st_4RxUQ9rE2xn8vIbplcQlCLQN");
            Console.WriteLine("Entropy of \" pk_t3st_4RxUQ9rE2xn8vIbplcQlCLQN\":                            " + e10);
            double e11 = secretsVerifier.ShannonEntropy("ghp_abcdefghijklmnopqrstuvwxyz123456");
            Console.WriteLine("Entropy of \"ghp_abcdefghijklmnopqrstuvwxyz123456 \":                        " + e11);
            double e12 = secretsVerifier.ShannonEntropy("github_pat_11ABCDEF1234567890FAK3");
            Console.WriteLine("Entropy of \" github_pat_11ABCDEF1234567890FAK3\":                           " + e12);
            double e13 = secretsVerifier.ShannonEntropy("glpat-abcdefghijklmnopqrstuvwxyz");
            Console.WriteLine("Entropy of \"glpat-abcdefghijklmnopqrstuvwxyz \":                            " + e13);
            double e14 = secretsVerifier.ShannonEntropy("WOlJeDRXzIDR9N0xXrQjIOYNoMYrlEvMz3HF91RTy");
            Console.WriteLine("Entropy of \"WOlJeDRXzIDR9N0xXrQjIOYNoMYrlEvMz3HF91RTy \":                   " + e14);
            double e15 = secretsVerifier.ShannonEntropy("v5PkYJ0atm3iKr9aiWgFJYmpuwhsti48AmdyxKykzsM");
            Console.WriteLine("Entropy of \" v5PkYJ0atm3iKr9aiWgFJYmpuwhsti48AmdyxKykzsM\":                 " + e15);
            double e16 = secretsVerifier.ShannonEntropy("YXZhaWxhYmxldGlyZWRldmVudHRhbGVzcmVndWxhcnByb2R1Y2VlbGV2ZW5zdGFydGM");
            Console.WriteLine("Entropy of \"YXZhaWxhYmxldGlyZWRldmVudHRhbGVzcmVndWxhcnByb2R1Y2VlbGV2ZW5zdGFydGM \":   " + e16);
            double e17 = secretsVerifier.ShannonEntropy("ec820703bf716f1bf64a2e54199395ed");
            Console.WriteLine("Entropy of \"ec820703bf716f1bf64a2e54199395ed\":                             " + e17);
            double e18 = secretsVerifier.ShannonEntropy("copenhagen");
            Console.WriteLine("Entropy of \"copenhagen\":                                                   " + e18);
            double e19 = secretsVerifier.ShannonEntropy("3141592653589793238462643383279");
            Console.WriteLine("Entropy of \"3141592653589793238462643383279\":                              " + e19);
            double e20 = secretsVerifier.ShannonEntropy("sk_live_3hmB4s6o0a62C7vrsK00sBJPb3z4CzY9GSEz1dfMtloMec9LpD949IbDPwbeW");
            Console.WriteLine("Entropy of \"sk_live_3hmB4s6o0a62C7vrsK00sBJPb3z4CzY9GSEz1dfMtloMec9LpD949IbDPwbeW\":  " + e20);       
            
            Console.WriteLine("");
            List<string> randomwords = new List<string>() {"oranges", "google", "traffic light", "cykel", "random", "ApiKeys", "Cryptography", "durumrulle", "laptopskærm", "entropy", "ARGGHHHHHH", "pneumonoultramicroscopicsilicovolcanoconiosis", "Antidisestablishmentarianism", "kat", "Champ", "Titin", "Aegilops", "Champichamp", "DemonChild", "Bæstet"};
            foreach(string str in randomwords)
            {
                double ent = secretsVerifier.ShannonEntropy(str);
                // if (ent>3)
                // {
                    Console.WriteLine(str + ": " + ent + " LOOKS LIKE HEX: " + secretsVerifier.isItHex(str) + " LOOKS LIKE BASE64: " + secretsVerifier.isItBase64(str));
                // }
                // Console.WriteLine(str + ": " + ent);
            }
        }
    }
}