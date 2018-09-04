namespace Notes.Database.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Notes.Database.Models;

    public class NotesContext : IdentityDbContext<ApplicationUser>
    {
        public NotesContext()
            : base("NotesConnection")
        {
            this.Database.Log = s => Debug.Write(s);
        }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Properties<DateTime>().Configure(t => t.HasPrecision(6));
            modelBuilder.Properties<string>().Configure(t => t.IsUnicode(false));
        }
    }
}
