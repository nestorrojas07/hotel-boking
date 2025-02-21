using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Auth;
using Domain.Models.HotelModels;
using Infraestructure.Contexts;

namespace Infraestructure.Seeds;

public static class HotelSeed
{

    public static void SeedHotel(this BookingContext bookingContext)
    {
        if (!bookingContext.Hotels.Any())
        {
            var city = bookingContext.Cities.FirstOrDefault();
            var hotel = new Hotel
            {
                Description = """
                    At Santa Marta Smart Resort Playa Dormida, you will discover the perfect 
                    blend of high-tech and high-touch amenities. Our Santa Marta, Colombia hotel
                    offers a convenient location near hot spots
                    """,
                IsActive = true,
                Name = "Smart Resort",
                Starts = 5,
                City = city,
                CityId = city.Id,
                CreatedBy = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,

            };
            bookingContext.Hotels.Add(hotel);
            bookingContext.SaveChanges();
            List<Room> rooms = new List<Room>();
            for (int i = 1; i < 50; i++) {
                rooms.Add(new Room { 
                    Hotel = hotel,
                    IsActive = true,
                    Name = $"{(int)(i/6) + 1}{i%6}",
                    PriceBase = 30f + i,
                    TaxPercentaje = 0.15f,
                    City = city,
                    CityId = city.Id,
                    GuestNumber = i%4 +1,
                    CreatedBy = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Description = "Discover upcoming events in Magdalena, Colombia with the help of our high-speed Wi-Fi"
                });
            }
            bookingContext.Rooms.AddRange(rooms);
            bookingContext.SaveChanges();

        }
    }
}
