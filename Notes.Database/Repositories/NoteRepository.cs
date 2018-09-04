namespace Notes.Database.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Notes.Database.Contexts;
    using Notes.Database.Models;

    public class NoteRepository : NotesBaseRepository<Note>
    {
        public NoteRepository(NotesContext context)
            : base(context)
        {
        }

        public override IQueryable<Note> All()
        {
            return this.Context.Notes;
        }
    }
}
