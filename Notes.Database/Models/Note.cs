namespace Notes.Database.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Note
    {
        [Key]
        public long Id { get; set; }

        [StringLength(100), Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [StringLength(300)]
        public string Content { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
