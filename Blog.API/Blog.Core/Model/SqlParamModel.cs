﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Model
{
    public class SqlParamModel
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public int Size { get; set; }

        public SqlDbType Type { get; set; }
    }
}
