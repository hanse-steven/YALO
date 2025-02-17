using YALO.Domain.Models;

namespace YALO.DAL.Interfaces;

public interface ISensorRepository
{
    public List<SensorData>? GetSensors(string sensorName, string range = "-5d");
    public List<SensorData>? GetHistorySensors(string sensorName, string range = "-5d");
}