using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebAPI.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Fullname must not be blank")]
        public string FullName { get; set; }

        [RegularExpression("^[a-zA-Z0-9]{6,20}@[a-z]{2,5}(.[a-z]{2,5}){1,2}$", ErrorMessage = "Email is invalid...")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Content must not be blank")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public DateTime Create_at { get; set; }

        public DateTime Update_at { get; set; }

        public DateTime? Delete_at { get; set; }
    }
}
