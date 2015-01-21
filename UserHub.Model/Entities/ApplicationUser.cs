using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserHub.Model.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Votes = new HashSet<Vote>();
            this.Suggestions = new HashSet<Suggestion>();
            this.Tenancies = new List<Tenancy>();
        }

        public ICollection<Suggestion> Suggestions { get; set; }

        public ICollection<Tenancy> Tenancies { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
