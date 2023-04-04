using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Water_Meter_Model;

namespace Water_Meter_Service
{
  public class measurementService
  {
    Utsa_WaterMeterContext _context;

    public measurementService(Utsa_WaterMeterContext waterMeterContext)
    {
      _context = waterMeterContext;
    }

    public List<Measurement> GetMeasurement()
    {
      return _context.Measurements.ToList();
    }

    public Measurement? GetMeasurementById(int id)
    {
      Measurement? measurement = _context.Measurements.FirstOrDefault(d => d.Id == id);
      return measurement;
    }

    public Measurement? GetMeasurementByDeviceId(int deviceid)
    {
      Measurement? measurement = _context.Measurements.FirstOrDefault(d => d.DeviceId == deviceid);
      return measurement;
    }

    public List<Measurement> GetMeasurementByDate(DateTime? StartDate, DateTime? EndDate)
    {
      List<Measurement> measurements = new List<Measurement>();
      if (StartDate == null || EndDate == null || (StartDate > EndDate))
      {
        return measurements;
      }
      return _context.Measurements.Where(m => m.TimeStamp >= StartDate && m.TimeStamp <= EndDate).ToList();
    }

      public Measurement AddMeasurement(MeasurementDTO measurementDto)
    {
      Device? device;
      measurementDto.Id = 0;
      Measurement measurement = new Measurement();
      measurement.Id = measurementDto.Id; 
      measurement.Liters = measurementDto.Liters;
      measurement.TimeStamp = measurementDto.TimeStamp;
      device = _context.Devices.FirstOrDefault(d => d.MAC_Address == measurementDto.Mac_Address);
      if (device != null)
      {
        measurement.DeviceId = device.Id;
        _context.Measurements.Add(measurement);
        _context.SaveChanges();
      }
      else
      {
        measurement = new Measurement();
      }
      return measurement;
    }

  }
}
