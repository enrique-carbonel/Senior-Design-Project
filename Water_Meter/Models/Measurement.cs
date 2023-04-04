using System;

namespace Water_Meter.Models
{
  public class Measurement
  {
    public int Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public decimal Liters { get; set; }
    public int DeviceId { get; set; }
  }
}
