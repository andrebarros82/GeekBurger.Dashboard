using System;
using System.ComponentModel.DataAnnotations;

namespace GeekBurger.Dashboard.Repository.Model
{
    public class UsersRestrictions
    {
        [Key]
        public Guid Id { get; set; }
        public int Users { get; set; }
        public string Restrictions { get; set; }
    }
}
