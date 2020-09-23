using System;
using System.Collections.Generic;
using System.Text;

namespace CBR_Parser
{
    public class MetalCurs
    {
        public DateTime DateMet { get; set;}
        public int Code { get; set; }
        public string Price { get; set; }
        public string ISOCode => Code switch
        {
            (1) => "A98",//Золото
            (2) => "A99",//Серебро
            (3) => "A76",//Платина
            (4) => "A33",//Палладий
            _ => string.Empty,
        };
    }
}
