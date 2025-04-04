using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Cuisine { get; set; }
        public double Note { get; set; }
        public string? ImagePath { get; set; }
    }
}
