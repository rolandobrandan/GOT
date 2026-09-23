using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GOT.Entities.DTOs
{
    public class BattleTypeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del tipo de batalla es obligatorio.")]
        public string? BattleType1 { get; set; } = string.Empty;

    }
}
