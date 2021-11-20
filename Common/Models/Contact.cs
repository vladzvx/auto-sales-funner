using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Column("CLIENT_ID")]
        public long ClientId { get; set; }

        [Column("ID-LINK")]
        public string LinkId { get; set; }

        [Column("COMPANY_ID")]
        public long CompanyId { get; set; }
        
        [Column("COMPANY_NAME")]
        public string CompanyName { get; set; }
        
        [Column("SHORT_LINK")]
        public string ShortLink { get; set; }

        [Column("PHONE")]
        public string Phone { get; set; }

        [Column("HAS_CLICK")]
        public bool HasClick { get; set; }
        
        [Column("CLICK_TIMESTAMP")]
        public DateTime ClickDateTime { get; set; }
    }
}
