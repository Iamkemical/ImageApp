using System.ComponentModel.DataAnnotations;

namespace ImageApp.Models
{
    public class ImageDatabaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] Picture  { get; set; }
    }
}