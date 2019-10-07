using System.ComponentModel.DataAnnotations.Schema;

namespace FortuneTeller.Service.Models
{
    public class FortuneEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Text { get; set; }
    }
}
