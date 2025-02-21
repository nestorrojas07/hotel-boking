using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Location;

namespace Domain.Models.HotelModels;

public class Hotel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Starts { get; set; }
    public long CityId { get; set; }
    public virtual City City { get; set; }

    public bool IsActive { get; set; }

    public long CreatedBy{ get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual List<Room> Rooms { get; set; }
}
