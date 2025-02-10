using System.Diagnostics;
using System.Text.RegularExpressions;

namespace YALO.BLL.Models;

public abstract class Module
{
    public abstract string ModuleName { get; }
    public abstract string StartupCommand { get; }
    public abstract string StopCommand { get; }
    public abstract Dictionary<string, Type> AvailableParameters { get; }
    public Dictionary<string, Parameter?> Parameters { get; set; } = new();
    
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;
    public int Width { get; set; } = 100;
    public int Height { get; set; } = 100;
    
    
    protected Module()
    {
        this.Name = Regex.Replace(this.GetType().Name, @"(\B[A-Z])", " $1") ;
    }
    
    public virtual void Configure() { }
    public abstract string SaveToJson();
    public abstract void LoadFromJson(string json);
    
    
    protected bool ExecuteCommand(string command, string arguments)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = command,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using Process process = new Process();
        process.StartInfo = processStartInfo;
        process.Start();
        process.WaitForExit();
            
        return process.ExitCode == 0;
    }
}