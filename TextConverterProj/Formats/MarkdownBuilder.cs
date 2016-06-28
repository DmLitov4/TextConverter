using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConverter
{
    public class MarkdownBuilder:Builder
    {
        private Product product = new Product();

        // ключевые слова
        string[] keys = { "p", "h1", "h2", "h3", "ordlist", "bullist" };

        /// <summary>
        /// обработка абзаца с ключевым словом "p"
        /// </summary>
        /// <param name="worktext"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private int HandleP(string[] worktext, int i)
        {
            if (worktext.Length == 2)
            {
                product.Add(worktext[i].Substring(2));
                return 0;
            }
            if (worktext[i]!="p")
            product.Add(worktext[i].Substring(2) +'\n');
            int index = i +1;
            if (worktext[index] == "")
            {
                product.Add(worktext[index]);
                product.Add("\n");
                index++;
                return index;
            }
            while (index < worktext.Length - 2 && String.IsNullOrEmpty(worktext[index + 1]) == false)
            {
                
                product.Add(worktext[index] + "\n");
                index++;
            }
            product.Add(worktext[index]);
            product.Add("\n"+'\n' );
            return index;
        }


        /// <summary>
        /// обработка абзаца с ключевым словом "h1"
        /// </summary>
        /// <param name="worktext"></param>
        /// <param name="i"></param>
        private int HandleH1(string[] worktext, int i)
        {
            product.Add("#" + worktext[i].Substring(2));
            while (worktext[i+1]!="\n" && worktext[i + 1] != "")
            {
                i++;
                product.Add("\n" + worktext[i]);
            }
            product.Add(" #" + '\n' + '\n');
            return i;
        }

        /// <summary>
        /// обработка абзаца с ключевым словом "h2"
        /// </summary>
        /// <param name="worktext"></param>
        /// <param name="i"></param>
        private int  HandleH2(string[] worktext, int i)
        {
            product.Add("##" + worktext[i].Substring(2));
            while (worktext[i + 1] != "\n" && worktext[i + 1] != "")
            {
                i++;
                product.Add("\n" + worktext[i]);
            }
            product.Add(" ##" + '\n' + '\n');
            return i;

        }

        /// <summary>
        /// обработка абзаца с ключевым словом "h3"
        /// </summary>
        /// <param name="worktext"></param>
        /// <param name="i"></param>
        private int  HandleH3(string[] worktext, int i)
        {
            product.Add("###" + worktext[i].Substring(2));
            while (worktext[i + 1] != "\n" && worktext[i + 1] != "")
            {
                i++;
                product.Add("\n" + worktext[i]);
            }
            product.Add(" ###" + '\n' + '\n');
            return i;
        }

        /// <summary>
        /// обработка абзаца с ключевым словом "bullist"
        /// </summary>
        /// <param name="worktext"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private int HandleBullist(string[] worktext, int i)
        {
            int index = i + 1;
            if (worktext[index] == "")
            {
                //product.Add("\n" + '\n');
                return index;
            }
            while (index < worktext.Length - 2 & (worktext[index ] != ""))
            {
                product.Add("* " + worktext[index] + "\n");
                index++;
            }
            product.Add("\n" );
            //product.Add("* " + worktext[index] + '\n' + '\n');
            return index;
        }

        /// <summary>
        /// обработка абзаца с ключевым словом "ordlist"
        /// </summary>
        /// <param name="worktext"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private int HandleOrdlist(string[] worktext, int i)
        {
            int index = i + 1;
            if (worktext[index] == "")
            {
                //product.Add("\n" + '\n');
                return index;
            }
            int ii = 1;
            while (index < worktext.Length - 2 & worktext[index ] != "")
            {
                product.Add(ii+". "+ worktext[index] + "\n");
                ii++;
                index++;
            }
            product.Add("\n");
            //product.Add(ii + ". " + worktext[index] + '\n' + '\n');
            return index;
        }

        /// <summary>
        /// we add null strings in the end for the sake of correct input
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        private string[] AddEnd(string[] ss)
        {
            List<string> result = new List<string>(ss);
            int i = 0;
            result.Add("\n");
            result.Add("\n");
            return result.ToArray(); 
        }

        public override void BuildPart(string[] worktextt)
        {

            int i = 0;
            string[] worktext = AddEnd(worktextt);
           
            //приводим к нижнему регистру для единой обработки
            for (int j = 0; j < worktext.Length; j++)
                worktext[j].ToLower();

            //пока не дошли до конца, обрабатываем
            while (i != worktext.Length - 1)
            {
                if (worktext[i].IndexOf(keys[0] + ' ') == 0 || (worktext[i].IndexOf(keys[0]) == 0 && worktext[i].Length == keys[0].Length))
                {
                    i = HandleP(worktext, i);
                }
                else if (worktext[i].IndexOf(keys[1] + ' ') == 0 || (worktext[i].IndexOf(keys[1]) == 0 && worktext[i].Length == keys[1].Length))
                {
                    i = HandleH1(worktext, i);
                    i++;
                }
                else if (worktext[i].IndexOf(keys[2] + ' ') == 0 || (worktext[i].IndexOf(keys[2]) == 0 && worktext[i].Length == keys[2].Length))
                {
                    i = HandleH2(worktext, i);
                    i++;
                }
                else if (worktext[i].IndexOf(keys[3] + ' ') == 0 || (worktext[i].IndexOf(keys[3]) == 0 && worktext[i].Length == keys[3].Length))
                {
                    i = HandleH3(worktext, i);
                    i++;
                }
                else if (worktext[i].IndexOf(keys[4] + ' ') == 0 || (worktext[i].IndexOf(keys[4]) == 0 && worktext[i].Length == keys[4].Length))
                {
                    i = HandleOrdlist(worktext, i);
                }
                else if (worktext[i].IndexOf(keys[5] + ' ') == 0 || (worktext[i].IndexOf(keys[5]) == 0 && worktext[i].Length == keys[5].Length))
                {
                    i = HandleBullist(worktext, i);
                }
                else
                //в начале абзаце не ключевое слово, 
                //переходим до следующего абзаца
                {
                    if (worktext[i] == "")
                    { i++; }
                    else
                    {
                        while (worktext[i + 1] != "")
                        {
                            if (i + 1 == worktext.Length - 1)
                                break;
                            i++;
                        }
                        i++;
                    }
                }
            }

        }

        /// <summary>
        /// возвращаем результат
        /// </summary>
        /// <returns></returns>
        public override Product GetResult()
        {
            product.DeleteEnd();
            return product;
        }

    }
}
