using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextConverter
{
    abstract public class Builder
    {
        public virtual void BuildPart(string [] worktext) { } 
        public abstract Product GetResult();
    }

}
