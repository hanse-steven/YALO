using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using YALO.BLL.Interfaces;
using YALO.DAL.Interfaces;
using YALO.Domain.Utils;
using Module = YALO.Domain.Models.Module;

namespace YALO.BLL.Services;

public class ModuleService : IModuleService
{
    private readonly IModuleRepository _moduleRepository;
    public ModuleService(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
        LoadModules();
    }
    
    public void SaveAll(IEnumerable<Module> modules)
    {
        this._moduleRepository.SaveAll(modules);
        this.ConfigureAll(modules);
    }

    private void ConfigureAll(IEnumerable<Module> modules)
    {
        StringBuilder startupCommands = new StringBuilder();
        StringBuilder stopCommands = new StringBuilder();
        startupCommands.Append("#!/bin/sh").Append('\n');
        stopCommands.Append("#!/bin/sh").Append('\n');
        
        foreach (Module module in modules)
        {
            startupCommands.Append(module.StartupCommand).Append('\n').Append("sleep 0.5").Append('\n');
            stopCommands.Append(module.StopCommand).Append('\n');
        }

        var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var start_path = Path.Combine(path, "YALO_startup.sh");
        var stop_path = Path.Combine(path, "YALO_stop.sh");
        
        File.WriteAllText(start_path, startupCommands.ToString());
        File.WriteAllText(stop_path, stopCommands.ToString());
        Thread.Sleep(1000);
        
        Command.Execute($"chmod +x {Path.Combine(path, "YALO_startup.sh")} {Path.Combine(path, "YALO_stop.sh")}");
        Thread.Sleep(1000);
        
        Command.Execute(stop_path);
        Thread.Sleep(4000);
        Command.Execute(start_path);
    }
    
    private void LoadModules()
    {
        Console.WriteLine("Searching for internal modules...");
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (Assembly assembly in assemblies)
        {
            this.LoadModuleFromAssemby(assembly);
        }
        
        Console.WriteLine("Searching for external modules...");
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules");
        string[] dllfiles = Directory.GetFiles(path, "*.dll");
        
        foreach (string dllfile in dllfiles)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(dllfile);
                this.LoadModuleFromAssemby(assembly);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement de {dllfile} : {ex.Message}");
            }
        }
        
        Console.WriteLine($"Loaded {Module.GetAvailableParameters().Count} modules.");
    }

    private void LoadModuleFromAssemby(Assembly assembly)
    {
        Type[] types = assembly.GetTypes();
        var derivedTypes = types.Where(t => t.IsClass && t.BaseType?.FullName == typeof(Module).FullName!);
        foreach (var type in derivedTypes)
        {
            MethodInfo? initializeMethod = type.GetMethod("Initialize", BindingFlags.Public | BindingFlags.Static);
            if (initializeMethod != null)
            {
                initializeMethod.Invoke(null, null);
            }
        }
    }

    public IEnumerable<Module> LoadData()
    {
        return this._moduleRepository.GetAll();
    }
}