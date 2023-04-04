using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Water_Meter_Model
{
    public partial class MeasurementDTO {

        public MeasurementDTO()
        {
            OnCreated();
            Mac_Address = string.Empty;
        }

        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public decimal Liters { get; set; }

        public string Mac_Address { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
