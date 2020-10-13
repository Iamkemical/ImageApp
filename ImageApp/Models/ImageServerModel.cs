using System.ComponentModel.DataAnnotations;

namespace ImageApp.Models
{
    public class ImageServerModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name="Image")]
        public string Image { get; set; }         
    }
}