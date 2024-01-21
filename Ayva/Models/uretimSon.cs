using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Ayva.Models
{
    public class uretimSon
    {
        [DisplayName("İl")]
        public string il { get; set; }
        [DisplayName("2021")]
        public Nullable<double> miktar1 { get; set; }
        [DisplayName("2022")]
        public Nullable<double> miktar2 { get; set; }
        [DisplayName("2023")]
        public Nullable<double> miktar3 { get; set; }
    }
}