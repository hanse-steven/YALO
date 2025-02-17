using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YALO.BLL.Interfaces;
using YALO.DAL.Interfaces;
using YALO.Domain.Models;

namespace YALO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SensorController : ControllerBase
{
    private readonly ISensorService _sensorService;
    public SensorController(ISensorService sensorService)
    {
        _sensorService = sensorService;
    }

    [HttpGet("{sensorName}")]
    public IActionResult GetSensors(string sensorName)
    {
        IEnumerable<SensorData>? datas = this._sensorService.GetSensors(sensorName);
        return Ok(datas);
    }
    
}

