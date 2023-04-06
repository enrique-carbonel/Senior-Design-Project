using System;

namespace Water_Meter2.Models
{
  public class Measurement
  {
    public int Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public double Liters { get; set; }
    public int DeviceId { get; set; }
  }
}
