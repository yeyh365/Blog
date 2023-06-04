using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Dto
{
    public class IdNameItem<T>
    {
        public T Id { get; set; }

        public string Name { get; set; }

    }
}
