namespace Notes.Database.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Notes.Database.Contexts;

    public abstract class NotesBaseRepository<TEntity> : BaseRepository<TEntity, NotesContext>
        where TEntity : class
    {
        protected NotesBaseRepository(NotesContext context)
            : base(context)
        {
            this.Context = context;
        }
    }
}
