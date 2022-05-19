using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace recilife_api.Model
{
    [Table("schedule")]
    public class Schedule
    {

        [Column("id")]
        public int Id { get; set; }
        [Column("start_schedule")]
        public TimeSpan Start { get; set; }
        [Column("end_schedule")]
        public TimeSpan End { get; set; }
        [Column("active")]
        public Boolean Active { get; set; }
    }
}
