using Microsoft.AspNetCore.Mvc;
using Water_Meter_Service;
using Water_Meter_Model;

namespace Water_Meter_API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class DeviceController : ControllerBase
  {
    private readonly deviceService _deviceService;

    public DeviceController(deviceService deviceService)
    {
      _deviceService = deviceService;
    }

    [HttpPost]
    [Route("AddDevice")]
    public IActionResult AddDevice([FromBody] Device device)
    {
      Device newdevice = _deviceService.AddDevice(device);
      return Ok(newdevice);
    }

    [HttpGet]
    [Route("GetDevices")]
    public IActionResult GetDevice()
    {
      List<Device> devices = _deviceService.GetDevice();
      return Ok(devices);
    }

    [HttpGet]
    [Route("GetDeviceById/{Id}")]
    public IActionResult GetDeviceById([FromRoute] int Id)
    {
      Device? device = _deviceService.GetDeviceById(Id);
      if (device == null) 
      { 
        return NotFound();
      }
      else
      {
        return Ok(device);
      }
    }

    [HttpPut]
    [Route("UpdateDevice")]
    public IActionResult UpdateDevice([FromBody] Device device)
    {
      Device? newdevice = _deviceService.UpdateDevice(device);
      if (newdevice == null) 
      {
        return NotFound();
      }
      else
      {
        return Ok(newdevice);
      }      
    }

    [HttpDelete]
    [Route("DeleteDevice/{Id}")]
    public IActionResult DeleteDevice([FromRoute] int Id)
    {
      bool result = _deviceService.DeleteDevice(Id);
      if(result) 
      {
        return Ok();
      }
      else
      {
        return NotFound();
      }
    }
  }
}
