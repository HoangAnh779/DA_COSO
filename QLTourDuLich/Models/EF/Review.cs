using QLTourDuLich.Models.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanHangOnline.Models.EF
{
    [Table("Review")]
    public  class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
         
        public virtual Product Product { get; set; }
    }
}
