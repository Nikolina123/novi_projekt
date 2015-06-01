using AVACOM_Online_Testiranje.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AVACOM_Online_Testiranje.DAL
{
    public class MojContext:DbContext
    {

        public MojContext()
            : base("Name=AVACOMConectionstring")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Entity<TestOblast>().HasKey(a=>new {a.TestId,a.OblastId});
            modelBuilder.Entity<TestOdgovor>().HasKey(a => new { a.TestId, a.PitanjeId });

            //modelBuilder.Entity<Korisnik>().Property(a => a.KorisnickoIme)
            //    .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(
            //        new IndexAttribute("Index",1) {IsUnique = true}));
        }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Oblast> Oblasti { get; set; }
        public DbSet<Odgovor> Odgovori { get; set; }
        public DbSet<Pitanje> Pitanja { get; set; }
        public DbSet<Test> Testovi { get; set; }
        public DbSet<TestOblast> TestOblast { get; set; }
        public DbSet<TestOdgovor> TestOdgovori { get; set; }
        public DbSet<VrstaPitanja> VrstePitanja { get; set; }
        public DbSet<KorisnikOdgovor> KorisnikOdgovori { get; set; }

    }
}