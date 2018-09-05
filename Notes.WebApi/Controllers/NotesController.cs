namespace Notes.WebApi.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Notes.Database.Models;
    using Notes.Database.Repositories;
    using Notes.WebApi.Models;

    public class NotesController : ApiController
    {
        private readonly IRepository<Note> noteRepository;

        public NotesController(IRepository<Note> noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public IEnumerable<NoteListItemBindingModel> Get()
        {
            return this.noteRepository.Transform(x =>
            new NoteListItemBindingModel
            {
                Id = x.Id,
                Title = x.Title,
                Body = x.Content.Substring(0, 5),
                Username = x.User.UserName
            });
        }

        [Authorize]
        [Route("api/me/Notes")]
        public IEnumerable<NoteListItemBindingModel> GetCurrentUserNotes()
        {
            var currentUserId = Thread.CurrentPrincipal.Identity.GetUserId();
            return this.noteRepository.All()
                .Where(x => x.UserId == currentUserId)
                .Select(x => new NoteListItemBindingModel
                {
                    Id = x.Id,
                    Body = x.Content.Substring(0, 5),
                    Title = x.Title,
                    Username = x.User.UserName
                });
        }

        public async Task<NoteDetailBindingModel> Get(long id)
        {
            var note = await this.noteRepository.FirstOrDefaultAsync(x => x.Id == id);
            return new NoteDetailBindingModel
            {
                Body = note.Content,
                Title = note.Title
            };
        }

        [Authorize]
        [HttpPost]
        public async Task<IHttpActionResult> Post(NoteCreateBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var note = new Note
            {
                Content = model.Body,
                Title = model.Title,
                UserId = Thread.CurrentPrincipal.Identity.GetUserId(),
            };

            this.noteRepository.Create(note);
            await this.noteRepository.SaveChangesAsync();

            return this.Ok();
        }
    }
}
