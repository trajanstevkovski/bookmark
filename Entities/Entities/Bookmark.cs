using System;
using System.ComponentModel.DataAnnotations;

namespace ReadLater.Entities
{
    public class Bookmark : EntityBase  
    {
        [Key]
        public int ID { get; set; }

        [StringLength(maximumLength: 500)]
        public string URL {get;set;}

        public string ShortDescription { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public DateTime CreateDate { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
