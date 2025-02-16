using System.Diagnostics;

namespace YALO.Domain.Utils;

public static class Command
{
    public static void Execute(string command)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"export DISPLAY=:0 && {command}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });
            Console.WriteLine($"Script shell exécuté : {command}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'exécution du script : {ex.Message}");
        }
    }
}