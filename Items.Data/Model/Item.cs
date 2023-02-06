using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items.Data.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid ColorGuid { get; set; }
        public Color Color { get; set; }
    }
}
