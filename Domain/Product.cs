using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductManager.Domain
{
    internal class Product
    {
        public required string Name { get; set; }

        private string skuNr;
        public required string SKU 
        { 
            get => skuNr; 
            set 
            {
                var skuNrRegex = "[1-9][0-9]";
                var regex = new Regex(skuNrRegex);
                if (!regex.IsMatch(value))
                {
                    throw new ArgumentException("SKU is valid");
                }
                skuNr = value;
            }
        }


        public required string Description { get; set; }

        private string URL;
        public required string Picture 
        {
            get => URL; 
            set
            {
                //var URLrexEx = "/ ^(?: ([A - Za - z] +):)?(\/{ 0,3})([0 - 9.\-A - Za - z] +)(?::(\d +)) ? (?:\/ ([^?#]*))?(?:\?([^#]*))?(?:#(.*))?$/"
                //    var regex = new Regex(URLrexEx);
                //if (!regex.IsMatch(value))
                //{
                //    throw new ArgumentException("URL is invalid");
                //}

                if (!value.StartsWith("https://"))
                    throw new ArgumentException("URL is invalid");

                URL = value;
            }
        }
        
        public required string Price { get; set; } 

    }
}
