using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Location;

namespace Domain.Models.HotelModels;

public class Room
{
    public long Id { get; set; }
    public long HotelId { get; set; }
    public long CityId { get; set; }
    public virtual City City { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int GuestNumber { get; set; }
    public bool IsActive { get; set; }
    public float PriceBase { get; set; }
    public float TaxPercentaje { get; set; }
    public float Total => PriceBase * TaxPercentaje;
    public long CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
