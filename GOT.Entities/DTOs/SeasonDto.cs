using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GOT.Entities.DTOs
{
    public class SeasonDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre de la temporada es obligatorio")]
        [StringLength(60, MinimumLength = 3, ErrorMessage ="El nombre debe tener entre {2} y {1} caracteres")]
        public string Name { get; set; } = "";


        [Required(ErrorMessage ="El año de lanzamiento es obligatorio")]
        [Range(2011, 2030, ErrorMessage ="El año debe estar entre {1} y {2}")]
        public int Year { get; set; }

    }
}
