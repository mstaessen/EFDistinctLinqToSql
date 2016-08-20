using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LinqToSqlWrongDistinctClauseReproduction.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<QueryDefinition> Definitions { get; set; }
    }
}
