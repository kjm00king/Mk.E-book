using System;
using System.Collections.Generic;
using System.Text;

namespace Mk.Entity
{
    public class Ebook
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public bool Empty { get; set; }

        public Ebook()
        {
            Year = string.Empty;
            Month = string.Empty;
            Empty = true;
        }
    }
}
