using InfluxDB.Client;
using Microsoft.Extensions.Configuration;
using YALO.DAL.Interfaces;
using YALO.Domain.Models;

namespace YALO.DAL.Repositories;

public class SensorRepository : ISensorRepository
{
    private readonly string _host;
    private readonly string _token;
    private readonly string _bucket;
    public SensorRepository(IConfiguration configuration)
    {
        this._host = configuration["InfluxDB:Host"];
        this._token = configuration["InfluxDB:Token"];
        this._bucket = configuration["InfluxDB:Bucket"];
    }
    
    public List<SensorData>? GetSensors(string sensorName)
    {
        using var client = new InfluxDBClient(_host, _token);

        var flux =
            $"from(bucket: \"{_bucket}\") " +
            $"|> range(start: -5d)" +
            $"|> filter(fn: (r) => r[\"_field\"] == \"h\" or r[\"_field\"] == \"t\")" +
            $"|> filter(fn: (r) => r[\"sensor_name\"] == \"{sensorName}\")" + 
            $"|> group(columns: [\"_field\"])" + 
            $"|> last()";

        return client.GetQueryApi().QueryAsync<SensorData>(flux, "makerhub").Result;
    }
}