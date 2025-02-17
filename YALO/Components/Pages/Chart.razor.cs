using Microsoft.AspNetCore.Components;
using MudBlazor;
using YALO.BLL.Interfaces;
using YALO.Domain.Models;

namespace YALO.Components.Pages;

public partial class Chart
{
    [Inject] public ISensorService SensorService { get; set; }
    
    private TimeSeriesChartSeries _humidtyData = new TimeSeriesChartSeries();
    private TimeSeriesChartSeries _temperatureData = new TimeSeriesChartSeries();
    private List<TimeSeriesChartSeries> _series = new List<TimeSeriesChartSeries>();
    
    public string? SelectedSensor { get; set; }
    public List<(string, string)> Capteurs { get; set; } =
    [
        ("Chambre", "t1"),
        ("Salon", "t2"),
        ("Chambre2", "t3"),
    ];
    
    public string? SelectedPeriod { get; set; }
    public List<(string label, string value, TimeSpan time)> Pediodes { get; set; } =
    [
        ("1 minute", "-1m", TimeSpan.FromSeconds(10)),
        ("5 minutes", "-5m", TimeSpan.FromMinutes(1)),
        ("15 minutes", "-15m", TimeSpan.FromMinutes(5)),
        ("1 heure", "-1h", TimeSpan.FromMinutes(10)),
        ("3 heures", "-3h", TimeSpan.FromMinutes(30)),
        ("6 heures", "-6h", TimeSpan.FromHours(1)),
        ("12 heures", "-12h", TimeSpan.FromHours(3)),
        ("2 jours", "-2d", TimeSpan.FromHours(6)),
        ("7 jours", "-1w", TimeSpan.FromDays(1)),
        ("30 jours", "-1m", TimeSpan.FromDays(5))
    ];
    public TimeSpan TimePeriod { get; set; } = TimeSpan.FromMinutes(5);
    
    private void RefreshData()
    {
        if (SelectedSensor is not null && SelectedPeriod is not null)
        {
            List<TimeSeriesChartSeries.TimeValue> humidtyData = new List<TimeSeriesChartSeries.TimeValue>();
            List<TimeSeriesChartSeries.TimeValue> temperatureData = new List<TimeSeriesChartSeries.TimeValue>();
            
            foreach (SensorData sensorData in this.SensorService.GetHistorySensors(SelectedSensor, SelectedPeriod))
            {
                if (sensorData.Field == "h" && sensorData.Value is not null)
                {
                    humidtyData.Add(new TimeSeriesChartSeries.TimeValue((DateTime)sensorData.Time!,(double)sensorData.Value));
                }
                else if (sensorData.Field == "t" && sensorData.Value is not null)
                {
                    temperatureData.Add(new TimeSeriesChartSeries.TimeValue((DateTime)sensorData.Time!,(double)sensorData.Value));
                }
            }

            _temperatureData = new TimeSeriesChartSeries
            {
                Index = 0,
                Name = "Température",
                Data = temperatureData,
                IsVisible = true,
                Type = TimeSeriesDisplayType.Line
            };
            
            _humidtyData = new TimeSeriesChartSeries
            {
                Index = 1,
                Name = "Humidité",
                Data = humidtyData,
                IsVisible = true,
                Type = TimeSeriesDisplayType.Line
            };
            
            _series.Clear();
            _series.Add(_temperatureData);
            _series.Add(_humidtyData);
            
            TimePeriod = Pediodes.Find(x => x.value == SelectedPeriod).time;
            
            StateHasChanged();
        }
    }
}