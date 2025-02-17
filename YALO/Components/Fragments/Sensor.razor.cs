using Microsoft.AspNetCore.Components;
using MudBlazor;
using YALO.BLL.Interfaces;
using YALO.Domain.Models;

namespace YALO.Components.Fragments;

public partial class Sensor
{
    [Parameter] public string SensorName { get; set; }
    [Parameter] public string SensorAlias { get; set; }
    [Parameter] public Color Color { get; set; } = Color.Primary;
    
    private string Temperature { get; set; } = "N/A";
    private string Humidity { get; set; } = "N/A";

    private Timer _timer;
    
    [Inject] private ISensorService _sensorService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await UpdateSensorDataAsync();
        _timer = new Timer(async _ => await UpdateSensorDataAsync(), null, 15000, 15000);
    }
    
    private async Task UpdateSensorDataAsync()
    {
        try
        {
            var sensorDataList = await Task.Run(() => _sensorService.GetSensors(SensorName));
            foreach (SensorData sensorData in sensorDataList)
            {
                if (sensorData.Field == "t")
                {
                    Temperature = sensorData.Value?.ToString("F0") ?? "N/A";
                }
                else if (sensorData.Field == "h")
                {
                    Humidity = sensorData.Value?.ToString("F0") ?? "N/A";
                }
            }
            InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            Console.WriteLine($"Error updating sensor data: {ex.Message}");
        }
    }
}