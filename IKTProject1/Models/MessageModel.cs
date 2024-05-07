using System.ComponentModel.DataAnnotations;

namespace IKTProject1.Models
{
    public class MessageModel
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public string Owner { get; set; }
    }
}
