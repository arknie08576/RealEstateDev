using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDev.Entities
{
    public class Apartment : RealEstate
    {
        public int Floor { get; set; }
        public Apartment() { }
        public Apartment(string name, int value, int area, int floor) : base(name, value, area)
        {
            Floor = floor;
        }
        public override string ToString()
        {
            return $"Id: {Id}, FirstName: {Name}, Value: {Value}, Area: {Area}, Floor: {Floor}";
        }
    }
}
