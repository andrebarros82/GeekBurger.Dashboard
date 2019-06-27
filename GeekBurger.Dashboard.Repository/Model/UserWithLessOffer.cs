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
                if (Restrictions != null)
                    return string.Join(",", Restrictions);
                else
                    return string.Empty;
            }
            set
            {
                if (Restrictions != null)
                    RestrictionsUser = string.Join(",", Restrictions);
            }
        }        
    }
}
