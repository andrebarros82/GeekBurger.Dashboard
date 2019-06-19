using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBurger.Dashboard.Repository.Model
{
    public class UserWithLessOffer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }

        [NotMapped]
        public string[] Restrictions;
        public List<UserRestriction> UserRestrictions { get; set; }
    }

    public class UserRestriction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Restriction { get; set; }
        public UserWithLessOffer UserWithLessOffer { get; set; }
    }
}
