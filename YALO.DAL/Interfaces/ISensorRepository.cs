using YALO.Domain.Models;

namespace YALO.DAL.Interfaces;

public interface ISensorRepository
{
    public List<SensorData>? GetSensors(string sensorName);
}