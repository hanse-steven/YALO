using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using YALO.BLL.Interfaces;
using YALO.Domain.Models;
using YALO.Domain.Utils;

namespace YALO.BLL.Modules;

public class WebContainerModule : Module
{
    public override string ModuleName => "Web Container Module";
    public override string StartupCommand => $"chromium --app=\"{Parameters["url"]}\" --user-data-dir=\"/tmp/chromium-profile-{Id}\" --window-position={X},{Y} --window-size={Width},{Height} &";
    public override string StopCommand => "pkill -f chromium";
    private static Dictionary<string, Type> AvailableParameters => new()
    {
        { "url", typeof(string) },
    };
    
    public static void Initialize()
    {
        RegisterModuleParameters<WebContainerModule>(AvailableParameters);
    }

    public override void Configure()
    {
        Command.Execute("sudo apt update");
        Command.Execute("sudo apt install chromium-browser");
    }
}