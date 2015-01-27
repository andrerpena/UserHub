using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserHub.Model.Entities
{
    public class Tenancy
    {
        public Tenancy()
        {
            this.Suggestions = new HashSet<Suggestion>();
            this.Votes = new HashSet<Vote>();
            this.SuggestionCategories = new HashSet<SuggestionCategory>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public ApplicationUser CreatedBy { get; set; }

        public ICollection<Suggestion> Suggestions { get; set; }

        public ICollection<Vote> Votes { get; set; }

        public ICollection<SuggestionCategory> SuggestionCategories { get; set; }
    }
}
