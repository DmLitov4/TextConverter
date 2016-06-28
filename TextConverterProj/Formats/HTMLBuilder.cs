using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConverter
{
    public class HTMLBuilder: Builder
    {
        
        private Product product = new Product();
        
        // ключевые слова
        string[] keys = { "p", "h1", "h2", "h3", "ordlist", "bullist" };

        //обработка абзаца с ключевым словом "p"
        private int HandleP(string[] worktext, int i)
        {
            if (worktext.Length == 2)
            {
                product.Add("<p> " + worktext[i].Substring(2) + " </p>");
                return 0;
            }
            product.Add("<p> " + worktext[i].Substring(2) + " </br>" + '\n');
            int index = i + 1;
            while (index < worktext.Length - 2 & worktext[index + 1] != "")
            {

                product.Add(worktext[index] + " </br>" + "\n");
                index++;
            }
            product.Add(worktext[index] + " </p>" + '\n' + '\n');
            return index;
        }

        //обработка абзаца с ключевым словом "h1"
        private void HandleH1(string [] worktext, int i)
        {
             product.Add("<h1>" + worktext[i].Substring(2) + " </h1>" + '\n' + '\n');
             
        }

        //обработка абзаца с ключевым словом "h2"
        private void HandleH2(string [] worktext, int i)
        {
            product.Add("<h2>" + worktext[i].Substring(2) + " </h2>" + '\n' + '\n');
            
        }

        //обработка абзаца с ключевым словом "h3"
        private void HandleH3(string[] worktext, int i)
        {
            product.Add("<h3>" + worktext[i].Substring(2) + " </h3>" + '\n' + '\n');     
        }

        //обработка абзаца с ключевым словом "bullist"
        private int HandleBullist(string[] worktext, int i)
        {
            product.Add("<ul>" + '\n');
            int index = i + 1;
            while (index < worktext.Length - 2 & worktext[index + 1] != "")
            {
                product.Add("<li>" + worktext[index] + " </li>" + '\n');
                index++;
            }
            product.Add("<li>" + worktext[index] + " </li>" + '\n' + " </ul>" + '\n' + '\n');
            return index;
        }

        private int HandleOrdlist(string[] worktext, int i)
        {
            product.Add("<ol>" + '\n');
            int index = i + 1;
            while (index < worktext.Length - 2 & worktext[index + 1] != "")
            {
                product.Add("<li>" + worktext[index] + " </li>" + '\n');
                index++;
            }
            product.Add("<li>" + worktext[index] + " </li>" + '\n'+ "</ol>" + '\n' + '\n');
            return index;
        }

        private string[] AddEnd(string[] ss)
        {
            List<string> result = new List<string>(ss);
            int i = 0;
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
                    if (worktext[i].IndexOf("p ") == 0)
                    {
                        i = HandleP(worktext, i);
                    }
                    else if (worktext[i].IndexOf("h1 ") == 0)
                    {
                        HandleH1(worktext, i);
                        i++;
                    }
                    else if (worktext[i].IndexOf("h2 ") == 0)
                    {
                        HandleH2(worktext, i);
                        i++;
                    }
                    else if (worktext[i].IndexOf("h3 ") == 0)
                    {
                        HandleH3(worktext, i);
                        i++;
                    }
                    else if (worktext[i].IndexOf("bullist") == 0)
                    {
                        i = HandleBullist(worktext, i);
                    }
                    else if (worktext[i].IndexOf("ordlist") == 0)
                    {
                        i = HandleOrdlist(worktext, i);
                    }
                    else
                        i++;
                }

            }
      
        //возвращаем результат
        public override Product GetResult()
        {
            return product;
        }
    }
}
