using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entropy
{
    class Cipher
    {


        char[] characters = new char[] {'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и',
                                                'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с',
                                                'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь',
                                                'э', 'ю', 'я'};

        public string Encode(string input, string keyword)
        {
            string result = "";

            int keyword_index = 0;

            foreach (char symbol in input)
            {
                if (Array.IndexOf(characters, symbol) != -1)
                {
                    int c = (Array.IndexOf(characters, symbol) +
                        Array.IndexOf(characters, keyword[keyword_index])) % characters.Length;

                    result += characters[c];
                    keyword_index++;
                    if (keyword_index == keyword.Length)
                        keyword_index = 0;

                }
                else
                result += symbol;
               

            }
            return result.ToLower();
        }
    }
}
