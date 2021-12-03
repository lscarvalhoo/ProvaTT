using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProvaTT.Models
{
    public class RegistroAcesso
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataAcesso { get; set; }
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

    }
}