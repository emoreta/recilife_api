using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace recilife_api.Model
{
    [Table("session_type")]
    public class SeesionType
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("description")]
        public string description { get; set; }
    }
}
