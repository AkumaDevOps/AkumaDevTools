using CSVTools.InfoExtract;
using CSVTools.InfoImport;
using CSVToolsTestLauncher;
using System.Runtime.CompilerServices;

InfoExtractorCSV<ExtractedObject> infoExtractor = new(@"C\:Pruebacsv.csv");
InfoImport<ExtractedObject> infoImport = new();
List<ExtractedObject> extractedObjects = infoExtractor.Extract();
extractedObjects.ForEach(x => Console.WriteLine(x.ToString()));
infoImport.Import(extractedObjects, @$"C:\Pruebacsv{DateTime.Now.Ticks}.csv");
