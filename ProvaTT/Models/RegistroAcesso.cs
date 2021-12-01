using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProvaTT.Models
{
    public class RegistroAcesso
    {
        public int Id { get; set; }
        public DateTime DataAcesso { get; set; }
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}