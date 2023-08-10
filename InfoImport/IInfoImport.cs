using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVTools.InfoImport
{
    internal interface IInfoImport<T>
    {
        void Import(List<T> data, string filePath);
    }
}
