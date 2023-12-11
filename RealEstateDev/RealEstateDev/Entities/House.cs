using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RealEstateDev.Entities
{
    internal class House : RealEstate
    {
        int LandArea { get; set; }
        public House() { }
        public House(string name, int value, int area, int landArea) : base(name, value, area)
        {
            LandArea = landArea;
        }
        public override string ToString()
        {
            return $"Id: {Id}, FirstName: {Name}, Value: {Value}, Area: {Area}, Land area: {LandArea}";
        }
    }
}
