using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GOT.Entities.DTOs
{
    public class KingdomDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del reino es obligatorio.")]
        public string Name { get; set; } = string.Empty;

        public string? Summary { get; set; }

        public string? Url { get; set; }

    }
}
