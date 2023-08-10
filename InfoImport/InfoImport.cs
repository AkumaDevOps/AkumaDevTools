using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVTools.InfoImport
{
    public class InfoImport<T> : IInfoImport<T>
    {
        public void Import(List<T> data, string filePath)
        {
            using var writer = new StreamWriter(filePath);

            // Escribe la línea de cabecera con los nombres de las propiedades
            var headerLine = string.Join(";", typeof(T).GetProperties().Select(p => p.Name));
            writer.WriteLine(headerLine);

            // Escribe las filas con los valores de las propiedades de los objetos
            foreach (var item in data)
            {
                var values = typeof(T).GetProperties()
                    .Select(p => p.GetValue(item)?.ToString() ?? string.Empty);
                var dataLine = string.Join(";", values);
                writer.WriteLine(dataLine);
            }
        }
    }
}
