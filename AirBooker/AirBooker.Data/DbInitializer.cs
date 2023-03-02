using AirBooker.Data.Contexts;
using AirBooker.Data.Entities;
using System.Globalization;

namespace AirBooker.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Airlines.Any())
            {
                await context.Airlines.AddRangeAsync(GetPreconfiguredAirlines());

                await context.SaveChangesAsync();
            }

            if (!context.Airports.Any())
            {
                await context.Airports.AddRangeAsync(GetPreconfiguredAirports());

                await context.SaveChangesAsync();
            }

            if (!context.Bookings.Any())
            {
                await context.Bookings.AddRangeAsync(GetPreconfiguredBookings());

                await context.SaveChangesAsync();
            }

            if (!context.Flights.Any())
            {
                await context.Flights.AddRangeAsync(GetPreconfiguredFlights(context.Airlines, context.Airports));

                await context.SaveChangesAsync();
            }

            if (!context.AirlineAirport.Any())
            {
                await context.AirlineAirport.AddRangeAsync(GetPreconfiguredAirlineAirport(context.Airlines, context.Airports));

                await context.SaveChangesAsync();
            }
        }

        private static List<Airline> GetPreconfiguredAirlines()
        {
            return new List<Airline>()
            {
                new Airline() {
                    Name = "Ryanair",
                    Description = "Ryanair Holdings plc is Europe's largest airline group and parent company of Ryanair, Ryanair UK, Buzz, Lauda and Malta Air. Together we're Europe's cleanest, greenest airline group with ambitious sustainability goals for 2030. We connect over 240 destinations in over 40 countries and offer the lowest fares in Europe.",
                    Country = "Ireland",
                    Rating = 3,
                    Founded = new DateTime(1984, 11, 28),
                    LogoFileName = "ryanair-logo.png"
                },
                new Airline() {
                    Name = "Turkish Airlines",
                    Description = "Turkish Airlines (Turkish: Türk Hava Yolları) is the national flag carrier airline of Turkey. As of 2022, it operates scheduled services to 340 destinations in Europe, Asia, Africa, and the Americas, making it the largest mainline carrier in the world by number of passenger destinations.", 
                    Country = "Turkey",
                    Rating = 4, 
                    Founded = new DateTime(1933, 5, 20),
                    LogoFileName = "tui-logo.png"
                },
                new Airline() {
                    Name = "Wizz Air",
                    Description = "Wizz Air is the fastest growing European low-cost airline (listed on the London Stock Exchange), operating a fleet of 145 Airbus A320 and A321 aircraft. We aim to fly 500 aircraft across our ever-developing network and carry over 100 million passengers per year by 2030.",
                    Country = "Hungary", 
                    Rating = 3, 
                    Founded = new DateTime(2003, 9, 1), 
                    LogoFileName = "wizzair-logo.png"
                },
                new Airline() {
                    Name = "Lufthansa", 
                    Description = "The Lufthansa Group is an aviation group with operations worldwide. With 105,290 employees, the Lufthansa Group generated revenue of EUR 16,811m in the financial year 2021. The Lufthansa Group is composed of the segments Network Airlines, Eurowings and Aviation Services.",
                    Country = "Germany",
                    Rating = 4,
                    Founded = new DateTime(1953, 1, 6),
                    LogoFileName = "lufthansa-logo.png"
                },
                new Airline() {
                    Name = "Emirates",
                    Description = "Emirates is the world's largest international airline, flying to 158 destinations in 85 countries. Emirates operates 269 aircraft and is the world's largest operator of the Airbus A380 and Boeing 777 family of aircraft.", 
                    Country = "UAE", 
                    Rating = 4,
                    Founded = new DateTime(1985, 5, 25),
                    LogoFileName = "emirates-logo.png"
                },
                new Airline() {
                    Name = "Qatar Airways",
                    Description = "Qatar Airways, the national carrier of the State of Qatar, is an award-winning airline that has received global recognition for its unparalleled services. In 2013, the airline joined the oneworld® alliance, becoming a strategic partner, and expanding the alliance's portfolio of Middle Eastern carriers.",
                    Country = "Qatar", 
                    Rating = 5,
                    Founded = new DateTime(1993, 11, 22),
                    LogoFileName = "qatar-logo.png"
                },
            };
        }

        private static List<Airport> GetPreconfiguredAirports()
        {
            return new List<Airport>()
            {
                new Airport() { 
                    Name = "Istanbul Airport", 
                    Description = "The airport currently has one terminal in service for domestic and international flights and five runways (three main and two backups) that are currently in operation. The two 17/35 runways are both 4,100 metres (13,451 feet) long, while the 16/34 runways are both 3,750 metres (12,303 feet) long.",
                    Place = "Turkey, Istanbul",
                    CityPhotoFileName = "istanbul-city-photo.png",
                    AirportPhotoFileName = "istanbul-terminal.png"
                },
                new Airport()
                {
                    Name = "Václav Havel Airport Prague",
                    Description = "Prague Airport has two main passenger terminals, two general aviation terminals, as well as a cargo facility. Most flights depart Prague Airport from the North Terminals (Terminal 1 and 2). The South Terminals (Terminal 3 and 4) handle a few irregular flights, as well as VIP flights, special flights and small aircraft.",
                    Place = "Prague, Czech Republic",
                    CityPhotoFileName = "prague-city-photo.png",
                    AirportPhotoFileName = "prague-terminal.png"
                },
                new Airport()
                {
                    Name = "Brussels Airport",
                    Description = "With approximately 26 million passengers a year, Brussels Airport is Belgium's largest airport. Located about 12 kilometres northeast of the capital Brussels, the airport is easily accessible by train, bus, car and bike. Brussels Airport has one terminal and three concourses; with the following gates: A, B and T.",
                    Place = "Brussels, Belgium",
                    CityPhotoFileName = "brussels-city-photo.png",
                    AirportPhotoFileName = "brussels-terminal.png"
                },
                new Airport()
                {
                    Name = "Adolfo Suárez Madrid–Barajas Airport",
                    Description = "The airport has four passenger terminals, an Executive terminal, an air cargo area, and two main hangar areas, on one side of the Old Industrial Area, between terminals T3 and T4 and the Industrial Area La Muñoza on the other side.",
                    Place = "Madrid, Spain",
                    CityPhotoFileName = "madrid-city-photo.png",
                    AirportPhotoFileName = "madrid-terminal.png"
                },
                new Airport()
                {
                    Name = "Warsaw Chopin Airport",
                    Description = "Warsaw Chopin Airport (IATA: WAW) is the largest airport in Poland and serves its capital, Warsaw.. - WAW Airport is located 10 kilometres south of Warsaw. - Warsaw Chopin Airport has a single passenger terminal called Terminal A. - Warsaw Airport is connected by rail to Warsaw city centre.",
                    Place = "Warsaw, Poland",
                    CityPhotoFileName = "warsaw-city-photo.png",
                    AirportPhotoFileName = "warsaw-terminal.png"
                },
            };
        }

        private static List<Booking> GetPreconfiguredBookings()
        {
            return new List<Booking>();
        }

        private static List<Flight> GetPreconfiguredFlights(DbSet<Airline> airlines, DbSet<Airport> airports)
        {
            var flights = new List<Flight>();

            for (var i = 0; i < 30; i++)
            {
                var departureDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 30, 0).AddDays(i);
                var arrivalDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 10, 0).AddDays(i);

                var newFlight = new Flight()
                {
                    FlightNumber = "TK 8471",
                    AirlineId = airlines.ToList()[1].Id,
                    Airline = airlines.ToList()[1],
                    FreeSeatsCount = 188,
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    TravelTime = arrivalDateTime - departureDateTime,
                    DepartureAirportId = airports.ToList()[0].Id,
                    DepartureAirport = airports.ToList()[0],
                    ArrivalAirportId = airports.ToList()[4].Id,
                    ArrivalAirport = airports.ToList()[4],
                    TicketPrice = (decimal)new Random().Next(5000, 20000) / 100,
                };

                flights.Add(newFlight);
            }

            for (var i = 0; i < 30; i++)
            {
                var departureDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 10, 0).AddDays(i);
                var arrivalDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 50, 0).AddDays(i);

                var newFlight = new Flight()
                {
                    FlightNumber = "TK 8472",
                    AirlineId = airlines.ToList()[1].Id,
                    Airline = airlines.ToList()[1],
                    FreeSeatsCount = 188,
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    TravelTime = arrivalDateTime - departureDateTime,
                    DepartureAirportId = airports.ToList()[4].Id,
                    DepartureAirport = airports.ToList()[4],
                    ArrivalAirportId = airports.ToList()[0].Id,
                    ArrivalAirport = airports.ToList()[0],
                    TicketPrice = (decimal)new Random().Next(5000, 20000) / 100,
                };

                flights.Add(newFlight);
            }

            for (var i = 0; i < 30; i++)
            {
                var departureDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 20, 0).AddDays(i);
                var arrivalDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 0).AddDays(i);

                var newFlight = new Flight()
                {
                    FlightNumber = "W 64298",
                    AirlineId = airlines.ToList()[2].Id,
                    Airline = airlines.ToList()[2],
                    FreeSeatsCount = 126,
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    TravelTime = arrivalDateTime - departureDateTime,
                    DepartureAirportId = airports.ToList()[4].Id,
                    DepartureAirport = airports.ToList()[4],
                    ArrivalAirportId = airports.ToList()[2].Id,
                    ArrivalAirport = airports.ToList()[2],
                    TicketPrice = (decimal)new Random().Next(1000, 5000) / 100,
                };

                flights.Add(newFlight);
            }

            for (var i = 0; i < 30; i++)
            {
                var departureDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 30, 0).AddDays(i);
                var arrivalDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 40, 0).AddDays(i);

                var newFlight = new Flight()
                {
                    FlightNumber = "W 64299",
                    AirlineId = airlines.ToList()[2].Id,
                    Airline = airlines.ToList()[2],
                    FreeSeatsCount = 126,
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    TravelTime = arrivalDateTime - departureDateTime,
                    DepartureAirportId = airports.ToList()[2].Id,
                    DepartureAirport = airports.ToList()[2],
                    ArrivalAirportId = airports.ToList()[4].Id,
                    ArrivalAirport = airports.ToList()[4],
                    TicketPrice = (decimal)new Random().Next(1000, 5000) / 100,
                };

                flights.Add(newFlight);
            }

            for (var i = 0; i < 30; i++)
            {
                var departureDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 00, 0).AddDays(i);
                var arrivalDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 25, 0).AddDays(i);

                var newFlight = new Flight()
                {
                    FlightNumber = "FR 5580",
                    AirlineId = airlines.ToList()[0].Id,
                    Airline = airlines.ToList()[0],
                    FreeSeatsCount = 126,
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    TravelTime = arrivalDateTime - departureDateTime,
                    DepartureAirportId = airports.ToList()[3].Id,
                    DepartureAirport = airports.ToList()[3],
                    ArrivalAirportId = airports.ToList()[2].Id,
                    ArrivalAirport = airports.ToList()[2],
                    TicketPrice = (decimal)new Random().Next(1000, 5000) / 100,
                };

                flights.Add(newFlight);
            }

            for (var i = 0; i < 30; i++)
            {
                var departureDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 00, 0).AddDays(i);
                var arrivalDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 25, 0).AddDays(i);

                var newFlight = new Flight()
                {
                    FlightNumber = "FR 5581",
                    AirlineId = airlines.ToList()[0].Id,
                    Airline = airlines.ToList()[0],
                    FreeSeatsCount = 126,
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    TravelTime = arrivalDateTime - departureDateTime,
                    DepartureAirportId = airports.ToList()[2].Id,
                    DepartureAirport = airports.ToList()[2],
                    ArrivalAirportId = airports.ToList()[3].Id,
                    ArrivalAirport = airports.ToList()[3],
                    TicketPrice = (decimal)new Random().Next(1000, 5000) / 100,
                };

                flights.Add(newFlight);
            }

            for (var i = 0; i < 30; i++)
            {
                var departureDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 35, 0).AddDays(i);
                var arrivalDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 35, 0).AddDays(i);

                var newFlight = new Flight()
                {
                    FlightNumber = "DLH 1131",
                    AirlineId = airlines.ToList()[3].Id,
                    Airline = airlines.ToList()[3],
                    FreeSeatsCount = 150,
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    TravelTime = arrivalDateTime - departureDateTime,
                    DepartureAirportId = airports.ToList()[3].Id,
                    DepartureAirport = airports.ToList()[3],
                    ArrivalAirportId = airports.ToList()[1].Id,
                    ArrivalAirport = airports.ToList()[1],
                    TicketPrice = (decimal)new Random().Next(5000, 20000) / 100,
                };

                flights.Add(newFlight);
            }

            for (var i = 0; i < 30; i++)
            {
                var departureDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 22, 45, 0).AddDays(i);
                var arrivalDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, 45, 0).AddDays(i + 1);

                var newFlight = new Flight()
                {
                    FlightNumber = "DLH 1132",
                    AirlineId = airlines.ToList()[3].Id,
                    Airline = airlines.ToList()[3],
                    FreeSeatsCount = 150,
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    TravelTime = arrivalDateTime - departureDateTime,
                    DepartureAirportId = airports.ToList()[1].Id,
                    DepartureAirport = airports.ToList()[1],
                    ArrivalAirportId = airports.ToList()[3].Id,
                    ArrivalAirport = airports.ToList()[3],
                    TicketPrice = (decimal)new Random().Next(5000, 20000) / 100,
                };

                flights.Add(newFlight);
            }

            for (var i = 0; i < 30; i++)
            {
                var departureDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 4, 10, 0).AddDays(i);
                var arrivalDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0).AddDays(i);

                var newFlight = new Flight()
                {
                    FlightNumber = "UAE 162",
                    AirlineId = airlines.ToList()[4].Id,
                    Airline = airlines.ToList()[4],
                    FreeSeatsCount = 232,
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    TravelTime = arrivalDateTime - departureDateTime,
                    DepartureAirportId = airports.ToList()[3].Id,
                    DepartureAirport = airports.ToList()[3],
                    ArrivalAirportId = airports.ToList()[4].Id,
                    ArrivalAirport = airports.ToList()[4],
                    TicketPrice = (decimal)new Random().Next(30000, 60000) / 100,
                };

                flights.Add(newFlight);
            }

            for (var i = 0; i < 30; i++)
            {
                var departureDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 50, 0).AddDays(i);
                var arrivalDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 40, 0).AddDays(i);

                var newFlight = new Flight()
                {
                    FlightNumber = "UAE 165",
                    AirlineId = airlines.ToList()[4].Id,
                    Airline = airlines.ToList()[4],
                    FreeSeatsCount = 232,
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    TravelTime = arrivalDateTime - departureDateTime,
                    DepartureAirportId = airports.ToList()[4].Id,
                    DepartureAirport = airports.ToList()[4],
                    ArrivalAirportId = airports.ToList()[3].Id,
                    ArrivalAirport = airports.ToList()[3],
                    TicketPrice = (decimal)new Random().Next(30000, 60000) / 100,
                };

                flights.Add(newFlight);
            }

            for (var i = 0; i < 30; i++)
            {
                var departureDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0).AddDays(i);
                var arrivalDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 35, 0).AddDays(i);

                var newFlight = new Flight()
                {
                    FlightNumber = "QTR 13",
                    AirlineId = airlines.ToList()[5].Id,
                    Airline = airlines.ToList()[5],
                    FreeSeatsCount = 212,
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    TravelTime = arrivalDateTime - departureDateTime,
                    DepartureAirportId = airports.ToList()[0].Id,
                    DepartureAirport = airports.ToList()[0],
                    ArrivalAirportId = airports.ToList()[4].Id,
                    ArrivalAirport = airports.ToList()[4],
                    TicketPrice = (decimal)new Random().Next(50000, 120000) / 100,
                };

                flights.Add(newFlight);
            }

            for (var i = 0; i < 30; i++)
            {
                var departureDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 0, 0).AddDays(i);
                var arrivalDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 35, 0).AddDays(i);

                var newFlight = new Flight()
                {
                    FlightNumber = "QTR 14",
                    AirlineId = airlines.ToList()[5].Id,
                    Airline = airlines.ToList()[5],
                    FreeSeatsCount = 212,
                    DepartureDateTime = departureDateTime,
                    ArrivalDateTime = arrivalDateTime,
                    TravelTime = arrivalDateTime - departureDateTime,
                    DepartureAirportId = airports.ToList()[4].Id,
                    DepartureAirport = airports.ToList()[4],
                    ArrivalAirportId = airports.ToList()[0].Id,
                    ArrivalAirport = airports.ToList()[0],
                    TicketPrice = (decimal)new Random().Next(50000, 120000) / 100,
                };

                flights.Add(newFlight);
            }

            return flights;
        }

        private static List<AirlineAirport> GetPreconfiguredAirlineAirport(DbSet<Airline> airlines, DbSet<Airport> airports)
        {
            return new List<AirlineAirport>()
            {
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Ryanair").Id,
                    Airline = airlines.First(x => x.Name == "Ryanair"),
                    AirportId = airports.First(x => x.Name == "Václav Havel Airport Prague")!.Id,
                    Airport = airports.First(x => x.Name == "Václav Havel Airport Prague")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Ryanair").Id,
                    Airline = airlines.First(x => x.Name == "Ryanair"),
                    AirportId = airports.First(x => x.Name == "Brussels Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Brussels Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Ryanair").Id,
                    Airline = airlines.First(x => x.Name == "Ryanair"),
                    AirportId = airports.First(x => x.Name == "Adolfo Suárez Madrid–Barajas Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Adolfo Suárez Madrid–Barajas Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Turkish Airlines").Id,
                    Airline = airlines.First(x => x.Name == "Turkish Airlines"),
                    AirportId = airports.First(x => x.Name == "Istanbul Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Istanbul Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Turkish Airlines").Id,
                    Airline = airlines.First(x => x.Name == "Turkish Airlines"),
                    AirportId = airports.First(x => x.Name == "Václav Havel Airport Prague")!.Id,
                    Airport = airports.First(x => x.Name == "Václav Havel Airport Prague")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Turkish Airlines").Id,
                    Airline = airlines.First(x => x.Name == "Turkish Airlines"),
                    AirportId = airports.First(x => x.Name == "Brussels Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Brussels Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Turkish Airlines").Id,
                    Airline = airlines.First(x => x.Name == "Turkish Airlines"),
                    AirportId = airports.First(x => x.Name == "Adolfo Suárez Madrid–Barajas Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Adolfo Suárez Madrid–Barajas Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Turkish Airlines").Id,
                    Airline = airlines.First(x => x.Name == "Turkish Airlines"),
                    AirportId = airports.First(x => x.Name == "Warsaw Chopin Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Warsaw Chopin Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Wizz Air").Id,
                    Airline = airlines.First(x => x.Name == "Wizz Air"),
                    AirportId = airports.First(x => x.Name == "Václav Havel Airport Prague")!.Id,
                    Airport = airports.First(x => x.Name == "Václav Havel Airport Prague")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Wizz Air").Id,
                    Airline = airlines.First(x => x.Name == "Wizz Air"),
                    AirportId = airports.First(x => x.Name == "Adolfo Suárez Madrid–Barajas Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Adolfo Suárez Madrid–Barajas Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Wizz Air").Id,
                    Airline = airlines.First(x => x.Name == "Wizz Air"),
                    AirportId = airports.First(x => x.Name == "Warsaw Chopin Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Warsaw Chopin Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Lufthansa").Id,
                    Airline = airlines.First(x => x.Name == "Lufthansa"),
                    AirportId = airports.First(x => x.Name == "Václav Havel Airport Prague")!.Id,
                    Airport = airports.First(x => x.Name == "Václav Havel Airport Prague")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Lufthansa").Id,
                    Airline = airlines.First(x => x.Name == "Lufthansa"),
                    AirportId = airports.First(x => x.Name == "Adolfo Suárez Madrid–Barajas Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Adolfo Suárez Madrid–Barajas Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Lufthansa").Id,
                    Airline = airlines.First(x => x.Name == "Lufthansa"),
                    AirportId = airports.First(x => x.Name == "Warsaw Chopin Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Warsaw Chopin Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Emirates").Id,
                    Airline = airlines.First(x => x.Name == "Emirates"),
                    AirportId = airports.First(x => x.Name == "Adolfo Suárez Madrid–Barajas Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Adolfo Suárez Madrid–Barajas Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Emirates").Id,
                    Airline = airlines.First(x => x.Name == "Emirates"),
                    AirportId = airports.First(x => x.Name == "Warsaw Chopin Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Warsaw Chopin Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Qatar Airways").Id,
                    Airline = airlines.First(x => x.Name == "Qatar Airways"),
                    AirportId = airports.First(x => x.Name == "Istanbul Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Istanbul Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Qatar Airways").Id,
                    Airline = airlines.First(x => x.Name == "Qatar Airways"),
                    AirportId = airports.First(x => x.Name == "Adolfo Suárez Madrid–Barajas Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Adolfo Suárez Madrid–Barajas Airport")!
                },
                new AirlineAirport()
                {
                    AirlineId = airlines.First(x => x.Name == "Qatar Airways").Id,
                    Airline = airlines.First(x => x.Name == "Qatar Airways"),
                    AirportId = airports.First(x => x.Name == "Warsaw Chopin Airport")!.Id,
                    Airport = airports.First(x => x.Name == "Warsaw Chopin Airport")!
                },
            };
        }
    }
}
