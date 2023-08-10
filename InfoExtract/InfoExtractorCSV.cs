using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSVTools.InfoExtract
{
    public class InfoExtractorCSV<T> : IInfoExtractor<T>
    {
        private readonly string _FilePath;
        public InfoExtractorCSV(string filePath)
        {
            if (!File.Exists(filePath)) throw new ArgumentException(message: $"The file {filePath} don't exist.");
            if (Path.GetExtension(filePath).ToUpper() != ".CSV") throw new ArgumentException(message: $"The file {filePath} extension is not CSV.");
            _FilePath = filePath;
        }
        public List<T> Extract()
        {
            return FileToListOf();
        }

        private List<T> FileToListOf()
        {
            string[] fileTextItems = File.ReadAllText(_FilePath).Split(Environment.NewLine);
            if (fileTextItems.ToList().Count == 0) { throw new Exception(message: $"The file {_FilePath} is empty."); }
            var objTProperties = typeof(T).GetProperties();
            List<ColumnPropertyClass> columnPropertyClasses = new();
            objTProperties.ToList().ForEach(property => columnPropertyClasses.Add(new ColumnPropertyClass() { Name = property.Name }));
            if (columnPropertyClasses.Count == 0) { throw new Exception(message: $"The file {_FilePath} is empty."); }
            List<T> list = new List<T>();
            //Como la lista no está vacía podemos acceder sin miedo al primer registro para obtener los indices de las columnas respecto a las propiedades
            var columns = fileTextItems[0].Split(';').ToList();
            // Rellenamos los indices de las propiedades de T con el indice encontrado en la columna
            for (int i = 0; i < columns.Count; i++)
            {
                var item = columnPropertyClasses.Find(x => x.Name.ToUpperInvariant() == columns[i].ToUpperInvariant());
                if (item != null)
                {
                    item.IndexColumn = i;
                }
            }
            for (int i = 1; i < fileTextItems.Length; i++)
            {
                var values = fileTextItems[i].Split(';');

                if (values.Length != columns.Count)
                {
                    throw new Exception($"Mismatch in the number of columns in line {i + 1} of {_FilePath}.");
                }

                // Creamos una instancia de tipo T y asignamos los valores del archivo a las propiedades correspondientes
                T obj = Activator.CreateInstance<T>();
                foreach (var property in objTProperties)
                {
                    var column = columnPropertyClasses.Find(x => x.Name.ToUpperInvariant() == property.Name.ToUpperInvariant());
                    if (column != null)
                    {
                        var value = values[column.IndexColumn];
                        property.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
                    }
                }

                list.Add(obj);
            }
            return list;
        }

    }

    class ColumnPropertyClass
    {
        public string Name { get; set; } = string.Empty;
        public int IndexColumn { get; set; }
    }

}
