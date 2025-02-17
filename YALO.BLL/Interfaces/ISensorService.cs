using YALO.Domain.Models;

namespace YALO.BLL.Interfaces;

public interface ISensorService
{
    public IEnumerable<SensorData>? GetSensors(string sensorName, string range = "-5d");
    public List<SensorData>? GetHistorySensors(string sensorName, string range = "-5d");
}