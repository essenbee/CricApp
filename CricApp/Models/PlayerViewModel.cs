using Dapper.Contrib.Extensions;

namespace CricApp.Models
{
    [Table("Players")]
    public class PlayerViewModel
    {
        [Key]
        public int Id { get; set; }

        public long CricApiId { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string PlayingRole { get; set; }
    }
}
