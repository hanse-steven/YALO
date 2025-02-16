using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Xml;
using System.Xml.Serialization;
using YALO.Domain.Models;
using YALO.DAL.Interfaces;

namespace YALO.DAL.Repositories;

public class ModuleRepository : IModuleRepository
{
    public IEnumerable<Module> GetAll()
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules.xml");
        if (!File.Exists(path)) return [];
        
        Type[] types = Module.GetAvailableParameters().Select(m => m.Key).ToArray();
        var modules = new List<Module>();
        
        var doc = new XmlDocument();
        doc.Load(path);
        
        var moduleNodes = doc.SelectNodes("//Module");
        if (moduleNodes == null) return modules;
        
        foreach (XmlNode moduleNode in moduleNodes)
        {
            try
            {
                using var nodeReader = new XmlNodeReader(moduleNode);
                var serializer = new XmlSerializer(typeof(Module), types);
                if (serializer.CanDeserialize(nodeReader))
                {
                    nodeReader.MoveToContent();
                    var module = (Module)serializer.Deserialize(nodeReader);
                    if (module != null)
                    {
                        modules.Add(module);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deserializing XML: {e.Message}");
                if (e.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {e.InnerException.Message}");
                }
                Console.WriteLine(e.StackTrace);
            }
        }
        
        return modules;

        // List<Module> modules = [];
        // var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules.xml");
        // if (!File.Exists(path)) return [];
        //
        // Type[] types = Module.GetAvailableParameters().Select(m => m.Key).ToArray();
        // XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Module>), types);
        // using (Stream fStream = File.OpenRead(path))
        // {
        //     modules = (List<Module>)xmlFormat.Deserialize(fStream);
        // }
        // return modules;
    }
    
    public void SaveAll(IEnumerable<Module> modules)
    {
        // Type[] types = Module.GetAvailableParameters().Select(m => m.Key).ToArray();
        // XmlSerializer serializer = new XmlSerializer(typeof(List<Module>), types);
        // var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules.xml");
        //
        // using (StreamWriter writter = new StreamWriter(path))
        // {
        //     serializer.Serialize(writter, modules.ToList());
        // }
        

        Type[] types = Module.GetAvailableParameters().Select(m => m.Key).ToArray();
        XmlSerializer serializer = new XmlSerializer(typeof(List<Module>), types);
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules.xml");

        using (StreamWriter writer = new StreamWriter(path))
        {
            serializer.Serialize(writer, modules);
        }
    }
}