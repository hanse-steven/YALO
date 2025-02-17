using YALO.BLL.Interfaces;
using YALO.DAL.Interfaces;
using YALO.Domain.Models;

namespace YALO.BLL.Services;

public class SensorService : ISensorService
{
    private readonly ISensorRepository _sensorRepository;
    public SensorService(ISensorRepository sensorRepository)
    {
        _sensorRepository = sensorRepository;
    }

    public IEnumerable<SensorData>? GetSensors(string sensorName, string range = "-5d")
    {
        return this._sensorRepository.GetSensors(sensorName, range);
    }
    
    public List<SensorData>? GetHistorySensors(string sensorName, string range = "-5d")
    {
        return this._sensorRepository.GetHistorySensors(sensorName, range);
    }
}