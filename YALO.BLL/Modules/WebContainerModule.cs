using YALO.BLL.Models;

namespace YALO.BLL.Modules;

public class WebContainerModule : Module
{
    public override string ModuleName => "Web Container Module";
    public override string StartupCommand => $"chromium --app=\"{Parameters["url"]}\" --user-data-dir=\"/tmp/chromium-profile-$$\" --window-position={X},{Y} --window-size={Width},{Height} &";
    public override string StopCommand => "pkill chromium";
    public override Dictionary<string, Type> AvailableParameters => new()
    {
        { "url", typeof(string) }
    };


    public override void Configure()
    {
        ExecuteCommand("apk", "update");
        ExecuteCommand("apk", "add chromium");
    }

    public override string SaveToJson()
    {
        throw new NotImplementedException();
    }

    public override void LoadFromJson(string json)
    {
        throw new NotImplementedException();
    }
}