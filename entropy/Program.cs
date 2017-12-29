using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace entropy
{
    class Program
    {
        struct Str
        {
            public string key;
            public string text;
            public double E1;
            public double E2;
        }
        static Dictionary<string, double> countries = new Dictionary<string, double>(); //справочник для хранения переменных
        static Dictionary<string, double> bigrams = new Dictionary<string, double>();
        static void Main(string[] args)
        {

            Cipher v = new Cipher();
            AES aes = new AES();
            string text = (File.ReadAllText("text.txt")).ToLower();
            text = Regex.Replace(text, "[-–.?!»«,:;\n1234567890]", "");
            Str[] Eal = new Str[11];
            Eal[0] = new Str();
            Eal[0].text = text;

            for (int i = 1; i < Eal.Length; i++)
            {
                Eal[i] = new Str();
                Eal[i].key = File.ReadAllLines("key.txt")[i - 1];
                Eal[i].text = v.Encode(text.ToLower(), Eal[i].key);
                Eal[i].text = Regex.Replace(Eal[i].text, "[-–.?!»«,:;\n1234567890]", "");
            }
            Entropy_res(Eal);
            Console.ReadKey();


        }
        static void Entropy_res(Str[] Eal)
        {
           

                for (int i = 0; i < Eal.Length; i++)
            {
                double x = Eal[i].text.Count();
                countries.Clear();
                foreach (var letter in Eal[i].text.Distinct().ToArray())
                {
                    try
                    {
                        double count = Regex.Matches(Eal[i].text, letter.ToString()).Count;
                        if (count != 0)
                              countries.Add(letter.ToString(), (count / x) * Math.Log(count / x, 2));
                    }

                    catch { }
                }

                Eal[i].E1 = -countries.Values.Sum();

                bigrams.Clear();
                var c = new string[Eal[i].text.Count() / 2 == 0 ? Eal[i].text.Count()-1 : Eal[i].text.Count() - 2];
                for (int j = 0; j < c.Count(); j++)
                {
                    foreach (var a in Eal[i].text.Skip(j).Take(2))
                    {
                        c[j]+=a;
                    }
                }
                bigrams = c.Distinct().ToDictionary(k=>k.ToString(),z=>Convert.ToDouble(0));
               

                for (int j = 0; j < bigrams.Keys.Count(); j++)
                {
                        double count = Regex.Matches(Eal[i].text,bigrams.Keys.ElementAt(j).ToString()).Count;
                        if (count != 0)
                            bigrams[bigrams.Keys.ElementAt(j).ToString()] = count != 0 ? (count / x) * Math.Log(count / x, 2) : 0.0;
                }
                Eal[i].E2 = -bigrams.Values.Sum();
            }

                Console.WriteLine(string.Format("{0,-10} | {1,-5} {2, -5} {3, -5} {4, -5} {5, -5} {6, -5} {7, -5} {8, -5} {9, -5} {10, -5}", "Испытание", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10));

                Console.WriteLine("{0,-10} | {1,-5} {2, -5} {3, -5} {4, -5} {5, -5} {6, -5} {7, -5} {8, -5} {9, -5} {10, -5}", "E1", Eal[1].E1, Eal[2].E1, Eal[3].E1, Eal[4].E1, Eal[5].E1, Eal[6].E1, Eal[7].E1, Eal[8].E1, Eal[9].E1, Eal[10].E1);
                Console.WriteLine("{0,-10} | {1,-5} {2, -5} {3, -5} {4, -5} {5, -5} {6, -5} {7, -5} {8, -5} {9, -5} {10, -5}", "E2", Eal[1].E2, Eal[2].E2, Eal[3].E2, Eal[4].E2, Eal[5].E2, Eal[6].E2, Eal[7].E2, Eal[8].E2, Eal[9].E2, Eal[10].E2);
                Console.WriteLine("{0,-10} | {1,-5} {2, -5} {3, -5} {4, -5} {5, -5} {6, -5} {7, -5} {8, -5} {9, -5} {10, -5}", "E1* - E1", Eal[0].E1 - Eal[1].E1, Eal[0].E1 - Eal[2].E1, Eal[0].E1 - Eal[3].E1, Eal[0].E1 - Eal[4].E1, Eal[0].E1 - Eal[5].E1, Eal[0].E1 - Eal[6].E1, Eal[0].E1 - Eal[7].E1, Eal[0].E1 - Eal[8].E1, Eal[0].E1 - Eal[9].E1, Eal[0].E1 - Eal[10].E1);
                Console.WriteLine("{0,-10} | {1,-5} {2, -5} {3, -5} {4, -5} {5, -5} {6, -5} {7, -5} {8, -5} {9, -5} {10, -5}", "E2* - E2", Eal[0].E2 - Eal[1].E2, Eal[0].E2 - Eal[2].E2, Eal[0].E2 - Eal[3].E2, Eal[0].E2 - Eal[4].E2, Eal[0].E2 - Eal[5].E2, Eal[0].E2 - Eal[6].E2, Eal[0].E2 - Eal[7].E2, Eal[0].E2 - Eal[8].E2, Eal[0].E2 - Eal[9].E2, Eal[0].E2 - Eal[10].E2);
            }





        }



    
}

