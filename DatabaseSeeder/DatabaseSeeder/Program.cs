using System;
using System.Linq;
using System.Collections.Generic;

namespace DatabaseSeeder
{
    class Program
    {
        public static void Main(string[] args)
        {
            // first create Locations list
            List<Location> Locations = new List<Location>()
            {
                new Location {LocationId = 1, Name = "Living Room"},
                new Location {LocationId = 2, Name = "Kitchen"},
                new Location {LocationId = 3, Name = "Master Bedroom"},
                new Location {LocationId = 4, Name = "Spare Bedroom"},
                new Location {LocationId = 5, Name = "Bathroom"}
            };
            // create date object containing current date/time
            DateTime localDate = DateTime.Now;

            DateTime eventDate = localDate.AddMonths(-6);

            Random rnd = new Random();

            List<Event> events = new List<Event>();
            // loop for each day in the range from 6 months ago to today
            while (eventDate < localDate)
            {
                // random between 0 and 5 determines the number of events to occur on a given day
                int eventsPerDay = rnd.Next(0, 6);
                // a sorted list will be used to store daily events sorted by date/time - each time an event is added, the list is re-sorted
                SortedList<DateTime, Event> dailyEvents = new SortedList<DateTime, Event>();
                // for loop to generate times for each event
                for (int i = 0; i < eventsPerDay; i++)
                {
                    // random between 0 and 23 for hour of the day
                    int hour = rnd.Next(0, 24);
                    // random between 0 and 59 for minute of the day
                    int minute = rnd.Next(0, 60);
                    // random between 0 and 59 for seconds of the day
                    int second = rnd.Next(0, 60);
                    // random location (use Locations)
                    int location = rnd.Next(0, Locations.Count);
                    // create date/time for event
                    DateTime dateTime = new DateTime(eventDate.Year, eventDate.Month, eventDate.Day, hour, minute,
                        second);
                    // create event from date/time and location
                    Event eve = new Event{Flagged = false, Location = Locations[location], LocationId = Locations[location].LocationId, TimeStamp = dateTime};
                    // add daily event to sorted list
                    dailyEvents.Add(dateTime, eve);
                }


                // loop thru sorted list for daily events
                foreach (var e in dailyEvents)
                {
                    events.Add(e.Value);
                }
                // add 1 day to eventDate
                eventDate = eventDate.AddDays(1);
            }

            int totalEvents = 1;

            // loop thru Events
            foreach (Event e in events)
            {

                Console.Write("Event Number: " + totalEvents + " : ");
                Console.Write("Time: " + e.TimeStamp + " : ");
                Console.Write("Location: " + e.Location.Name);
                Console.WriteLine();
                totalEvents++;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(("-------------------"));
            Console.WriteLine("Total Events = " + events.Count());
            Console.WriteLine(("-------------------"));

        }

        public class Location
        {
            public int LocationId { get; set; }
            public string Name { get; set; }
        }

        public class Event
        {
            public int EventId { get; set; }
            public DateTime TimeStamp { get; set; }

            public bool Flagged { get; set; }

            // foreign key for location 
            public int LocationId { get; set; }

            // navigation property
            public Location Location { get; set; }
        }
    }
}