using System.ComponentModel.DataAnnotations;

namespace MVCAuth.Models
{
    public class UpdateProfileVM
    {
        public UpdateProfileVM()
        {
            ID = -1; //doi tuong chua xac lap or kg ton tai
        }
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string FullName { get; set; }


        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required]
        public string MobileNo { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

       
    }
}
