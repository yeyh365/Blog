using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Helper
{
    public class BlueReadBean
    {
        Guid s;
        Guid c;
        String name;

        public BlueReadBean(Guid s, Guid c, String name)
        {
            this.s = s;
            this.c = c;
            this.name = name;
        }

        public String getName()
        {
            return name;
        }

        public BlueReadBean(Guid s, Guid c)
        {
            this.s = s;
            this.c = c;
        }

        public Guid getC()
        {
            return c;
        }

        public Guid getS()
        {
            return s;
        }
    }
}
