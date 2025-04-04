using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.DTOs
{
    public class RestaurantUpdateDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Cuisine { get; set; }
        public int Note { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? Image { get; set; }
    }

}
