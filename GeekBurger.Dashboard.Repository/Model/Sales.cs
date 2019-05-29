using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GeekBurger.Dashboard.Repository.Model
{
    public class Sales
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StoredId { get; set; }
        public int Total { get; set; }
        public string Value { get; set; }
    }
}
