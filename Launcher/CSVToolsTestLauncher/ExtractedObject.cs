using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVToolsTestLauncher
{
    internal class ExtractedObject
    {
        public ExtractedObject()
        {
        }

        public ExtractedObject(string? columna1, string? columna2, string? columna3, string? columna4, string? columna5)
        {
            Columna1 = columna1;
            Columna2 = columna2;
            Columna3 = columna3;
            Columna4 = columna4;
            Columna5 = columna5;
        }

        public string? Columna1 { get; set; }
        public string? Columna2 { get; set; }
        public string? Columna3 { get; set; }
        public string? Columna4 { get; set; }
        public string? Columna5 { get; set; }
        
        public override string  ToString()
        {
            return $"{nameof(Columna1)}:'{Columna1}'" +
                $"{nameof(Columna2)}:'{Columna2}'" +
                $"{nameof(Columna3)}:'{Columna3}'" +
                $"{nameof(Columna4)}:'{Columna4}'" +
                $"{nameof(Columna5)}:'{Columna5}'" ;
        }
    }

}
