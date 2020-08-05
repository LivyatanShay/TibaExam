using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TibaExam.Models
{
    [Table("Favorites")]
    public class Favorite
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
