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
        public Guid UserId { get; set; }
        
        [NotMapped]
        public string[] Restrictions;

        public string RestrictionsUser
        {
            get
            {
                return string.Join(",", Restrictions);
            }
            set
            {
                RestrictionsUser = string.Join(",", Restrictions);
            }
        }        
    }
}
