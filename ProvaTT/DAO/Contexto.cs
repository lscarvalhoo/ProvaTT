using ProvaTT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ProvaTT.DAO
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Inscricao> Inscricao { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}