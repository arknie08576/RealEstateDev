using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDev.Entities
{
    public class RealEstate : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public int Area { get; set; }
        public RealEstate() { }
        public RealEstate(string name, int value, int area)
        {
            Name = name;
            Value = value;
            Area = area;
        }
    }
}
