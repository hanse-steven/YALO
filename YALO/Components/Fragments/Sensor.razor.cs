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

    protected override void OnInitialized()
    {
        foreach (SensorData sensorData in this._sensorService.GetSensors(SensorName))
        {
            if (sensorData.Field == "t")
            {
                this.Temperature = sensorData.Value?.ToString("F0") ?? "N/A";
            }
            else if (sensorData.Field == "h")
            {
                this.Humidity = sensorData.Value?.ToString("F0") ?? "N/A";
            }
        }
        this._timer = new Timer(UpdateSensorData, null, 15000, 15000);
    }
    
    private void UpdateSensorData(object state)
    {
        InvokeAsync(() =>
        {
            foreach (SensorData sensorData in this._sensorService.GetSensors(SensorName))
            {
                if (sensorData.Field == "t")
                {
                    this.Temperature = sensorData.Value?.ToString("F0") ?? "N/A";
                }
                else if (sensorData.Field == "h")
                {
                    this.Humidity = sensorData.Value?.ToString("F0") ?? "N/A";
                }
            }
            StateHasChanged();
        });
    }
}