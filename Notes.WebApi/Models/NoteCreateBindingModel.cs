namespace Notes.WebApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class NoteCreateBindingModel
    {
        [StringLength(100)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }


        [StringLength(300)]
        public string Body { get; set; }
    }
}