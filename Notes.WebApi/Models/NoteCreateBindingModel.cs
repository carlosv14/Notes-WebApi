using System.ComponentModel.DataAnnotations;

namespace Notes.WebApi.Models
{
    public class NoteCreateBindingModel
    {
        [StringLength(100)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        public string Body { get; set; }
    }
}