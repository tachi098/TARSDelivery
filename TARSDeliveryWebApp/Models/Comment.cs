using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebApp.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "Fullname must not be blank")]
        public string FullName { get; set; }


        [Required(ErrorMessage = "Email must not be blank")]
        [RegularExpression(@"^\w{1,}@\w{2,}(\.\w{2,}){1,2}$", ErrorMessage = "Email is invalid...")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Message must not be blank")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime? Update_at { get; set; }
        public DateTime? Delete_at { get; set; }
    }
}
