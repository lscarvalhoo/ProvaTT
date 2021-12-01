using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProvaTT.Models
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public int QuantidadeVagas { get; set; }
    }
}