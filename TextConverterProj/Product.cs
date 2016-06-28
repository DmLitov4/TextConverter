using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConverter
{
    public class Product
    {
       
        private List<string> parts = new List<string>();

        /// <summary>
        /// Delete  unnecessary empty string at the end
        /// </summary>
        public void DeleteEnd()
        {
            for (var i = parts.Count - 1; i >= 0; i--)
                if (string.IsNullOrWhiteSpace(parts[i]))
                    parts.RemoveAt(i);
                else
                {
                    parts[parts.Count - 1] = parts[parts.Count - 1].Replace("\n", ""); return;
                }
            
        }

        /// <summary>
        /// добавление строк
        /// </summary>
        /// <param name="part"></param>
        public void Add(string part)
        {
            parts.Add(part);
        }

        public void RemoveLast()
        {
            parts.RemoveAt(parts.Count - 1);
        }

        /// <summary>
        /// печать полученного результата
        /// </summary>
        /// <returns></returns>
        public string Show()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < parts.Count; i++)
                sb.Append(parts[i]);
            parts.Clear();
            return sb.ToString();
        }
    }
}
