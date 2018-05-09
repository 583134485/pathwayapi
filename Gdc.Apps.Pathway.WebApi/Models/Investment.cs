using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gdc.Apps.Pathway.WebApi.Models
{
    public class Investment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Leader { get; set; }
        public int ManagerId { get; set; }
        public string Manager { get; set; }
    }
}