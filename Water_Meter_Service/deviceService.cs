using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Water_Meter_Model;

namespace Water_Meter_Service
{
  public class deviceService
  {
    Utsa_WaterMeterContext _context;

    public deviceService(Utsa_WaterMeterContext waterMeterContext)
    {
      _context = waterMeterContext;
    }

    public List<Device> GetDevice()
    {
      return _context.Devices.ToList();
    }

    public Device? GetDeviceById (int id) 
    { 
      Device? device = _context.Devices.FirstOrDefault(d => d.Id == id);
      return device;    
    }

    public Device AddDevice(Device device)
    {
      device.Id = 0;
      _context.Devices.Add(device);
      _context.SaveChanges();
      return device;
    }

    public Device? UpdateDevice(Device device)
    {
      Device currentDevice = _context.Devices.FirstOrDefault(d => d.Id == device.Id);
      if (currentDevice != null)
      {
        _context.Entry(currentDevice).CurrentValues.SetValues(device);
        _context.SaveChanges();
        return currentDevice;
      }
      else
      {
        return null;
      }
    }

    public bool DeleteDevice(int Id)
    {
      Device currentDevice = _context.Devices.FirstOrDefault(d => d.Id == Id);
      if (currentDevice != null)
      {
        _context.Devices.Remove(currentDevice);
        _context.SaveChanges();
        return true;
      }
      else
      {
        return false;
      }
    }

  }
}
