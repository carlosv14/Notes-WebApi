namespace Notes.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class NoteDetailBindingModel
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string Username { get; set; }
    }
}