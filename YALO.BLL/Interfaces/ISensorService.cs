using YALO.Domain.Models;

namespace YALO.BLL.Interfaces;

public interface ISensorService
{
    public IEnumerable<SensorData>? GetSensors(string sensorName);
}