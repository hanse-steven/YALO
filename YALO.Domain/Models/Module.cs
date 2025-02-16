using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace YALO.Domain.Models;

public abstract class Module
{
    [XmlIgnore] public abstract string ModuleName { get; }
    [XmlIgnore] public abstract string StartupCommand { get; }
    [XmlIgnore] public abstract string StopCommand { get; }
    [XmlIgnore] public Dictionary<string, string> Parameters { get; set; } = new();
    
    public string JsonParameters
    {
        get => JsonSerializer.Serialize(Parameters);
        set => Parameters = JsonSerializer.Deserialize<Dictionary<string, string>>(value);
    }
    
    [XmlIgnore] private static readonly Dictionary<Type, Dictionary<string, Type>> _parametersRegistry = new();
    
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    
    [XmlIgnore] public string DisplayName => string.IsNullOrWhiteSpace(Name) ? ModuleName : Name;
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;
    public int Width { get; set; } = 100;
    public int Height { get; set; } = 100;
    
    public virtual void Configure() { }

    public static Dictionary<Type, Dictionary<string, Type>> GetAvailableParameters()
    {
        return _parametersRegistry;
    }

    public static Dictionary<string, Type> GetAvailableParameters(Type moduleType)
    {
        if (_parametersRegistry.TryGetValue(moduleType, out var parameters))
        {
            return parameters;
        }
        return new Dictionary<string, Type>();
    }
    
    protected static void RegisterModuleParameters<T>(Dictionary<string, Type> parameters) where T : Module
    {
        _parametersRegistry[typeof(T)] = parameters;
    }
}