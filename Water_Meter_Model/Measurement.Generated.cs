﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 3/25/2023 5:42:16 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Water_Meter_Model
{
    public partial class Measurement {

        public Measurement()
        {
            OnCreated();
        }

        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public decimal Liters { get; set; }

        public int DeviceId { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
