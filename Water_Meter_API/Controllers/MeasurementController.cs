using Microsoft.AspNetCore.Mvc;
using Water_Meter_Service;
using Water_Meter_Model;

namespace Water_Meter_API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MeasurementController : ControllerBase
  {
    private readonly measurementService _measurementService;

    public MeasurementController(measurementService measurementService)
    {
      _measurementService = measurementService;
    }

    [HttpPost]
    [Route("AddMeasurement")]
    public IActionResult AddMeasurement([FromBody] MeasurementDTO measurementDto)
    {
      Measurement newmeasurement = _measurementService.AddMeasurement(measurementDto);
      return Ok(newmeasurement);
    }

    [HttpGet]
    [Route("GetMeasurement")]
    public IActionResult GetMeasurement()
    {
      List<Measurement> measurements = _measurementService.GetMeasurement();
      return Ok(measurements);
    }

    [HttpGet]
    [Route("GetMeasurementByDate")]
    public IActionResult GetMeasurementByDate([FromQuery]DateTime? StartDate,[FromQuery] DateTime? EndDate)
    {
      List<Measurement> measurements = _measurementService.GetMeasurementByDate(StartDate,EndDate);
      return Ok(measurements);
    }
    

    [HttpGet]
    [Route("GetMeasurementById/{Id}")]
    public IActionResult GetMeasurementById([FromRoute] int Id)
    {
      Measurement? measurement = _measurementService.GetMeasurementById(Id);
      if (measurement == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(measurement);
      }
    }

    [HttpGet]
    [Route("GetMeasurementByDeviceId/{deviceId}")]
    public IActionResult GetMeasurementByDeviceId([FromRoute] int deviceId)
    {
      Measurement? measurement = _measurementService.GetMeasurementByDeviceId(deviceId);
      if (measurement == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(measurement);
      }
    }

  }
}