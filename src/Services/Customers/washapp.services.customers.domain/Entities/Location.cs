using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using washapp.services.customers.domain.Exceptions;

namespace washapp.services.customers.domain.Entities
{
    public class Location 
    {
        public Guid Id { get; set; }
        public string LocationName { get; set; }
        public string LocationColor { get; set; }

        public Location() {}
        public Location(string locationName, string locationColor)
        {
            if (string.IsNullOrWhiteSpace(locationName) || string.IsNullOrWhiteSpace(locationColor))
            {
                throw new InvalidLocationNameOrColorException();
            }
            LocationName = locationName;
            LocationColor = locationColor;
        }

        public static Location Create(string locationName, string locationColor)
        {
            Location location = new Location(locationName, locationColor);
            return location;
        }

        public void UpdateLocation(string locationName, string locationColor)
        {
            if (string.IsNullOrWhiteSpace(locationName) || string.IsNullOrWhiteSpace(locationColor))
            {
                throw new InvalidLocationNameOrColorException();
            }
        }
        
    }
    
}
