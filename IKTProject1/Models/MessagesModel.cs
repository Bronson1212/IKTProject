using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace IKTProject1.Models
{
    public class MessagesModel
    {
        [Key]
        public int Id { get; set; }
        public string Autor { get; set; }
        public string Text { get; set; }
    }
}
