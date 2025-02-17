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

    public IEnumerable<SensorData>? GetSensors(string sensorName)
    {
        return this._sensorRepository.GetSensors(sensorName);
    }
}