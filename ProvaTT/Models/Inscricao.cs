using System;
using System.ComponentModel.DataAnnotations;

namespace ProvaTT.Models
{
    public class Inscricao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public int CursoId { get; set; }
        
        public int UsuarioId { get; set; }

        [Required]
        public DateTime DataInscricao { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}