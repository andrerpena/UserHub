using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserHub.Model.Entities
{
    public class Suggestion
    {
        public Suggestion()
        {
            this.Votes = new HashSet<Vote>();
        }

        public int Id { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<Vote> Votes { get; set; } 
    }
}
