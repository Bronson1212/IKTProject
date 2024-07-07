using System.ComponentModel.DataAnnotations;

namespace IKTProject1.Models
{
    public class CommentModel
    {
        [Key]
        public int Id { get; set; }

        public string Autor { get; set; }
        public int MessageId {  get; set; }

        public string Text { get; set; }
    }
}
