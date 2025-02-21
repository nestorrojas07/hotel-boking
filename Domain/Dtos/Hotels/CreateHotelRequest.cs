﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Hotels;

public class CreateHotelRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Starts { get; set; }

    public bool IsActive { get; set; }
}
