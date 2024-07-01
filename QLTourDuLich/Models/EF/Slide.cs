using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanHangOnline.Models.EF
{
    [Table("tb_Slide")]
    public class Slide
    {
        public long Id { get; set; }

        public string Image { get; set; }

        public int DisPlayOrder { get; set; } 

        public string Title { get; set; }
        public string Link { get; set; }

        public bool Status { get; set; }
    }
}
