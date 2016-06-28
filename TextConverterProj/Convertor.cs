using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConverter
{
    public class Convertor
    {
        public void Construct(Builder builder, string[] worktext)
        {
            builder.BuildPart(worktext);
        }
    }
}
