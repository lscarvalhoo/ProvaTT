using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProvaTT.Models
{
    public class Informacoes
    {
        public int Id { get; set; }
        public decimal ValorCurso { get; set; }
        public int QuantidadeVagas { get; set; }
        public int QuantidadeInscritos { get; set; }
    }
}