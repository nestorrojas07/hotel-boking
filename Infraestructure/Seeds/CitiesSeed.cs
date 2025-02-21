using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Auth;
using Domain.Models.HotelModels;
using Domain.Models.Location;
using Infraestructure.Contexts;

namespace Infraestructure.Seeds;

public static class CitiesSeed
{

    public static void SeedCities(this BookingContext bookingContext)
    {
        if (!bookingContext.Cities.Any())
        {
            var cities = new List<City>()
            {
                new City()
                {
                    Name = "Cartagena de Indias, Bolívar",
                    Abbreviation = "CTG",
                },
                new City()
                {
                    Name = "Bogotá",
                    Abbreviation = "BOG",
                },
                new City()
                {
                    Name = "Barranquilla, Atlántico",
                    Abbreviation = "BAQ",
                },
                new City()
                {
                    Name = "Smart Talent",
                    Abbreviation = "SMT1",
                },
                new City()
                {
                    Name = "Santa Marta, Magdalena",
                    Abbreviation = "SMR",
                },
            };
            
            bookingContext.Cities.AddRange(cities);
            bookingContext.SaveChanges();

        }
    }
}
