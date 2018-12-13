using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Course.Models.DataBaseModel;

namespace CourseWork.Models.DataBaseModel
{
    public class User
    {
        [Key]
        public string Login { get; set; }

        public string Password { get; set; }

        public int Style { get; set; }

        [InverseProperty("User")]
        public ICollection<AccessTokens> AccessTokensTable { get; set; }
    }
}