﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Hotels;

public class CreateRoomRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public float PriceBase { get; set; }
    public float TaxPercentaje { get; set; }
    public int GuestNumber { get; set; }
}
