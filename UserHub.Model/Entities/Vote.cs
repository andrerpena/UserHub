using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserHub.Model.Entities
{
    public class Vote
    {
        public int Id { get; set; }

        public VoteType Type { get; set; }

        public Suggestion Suggestion { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public ApplicationUser CreatedBy { get; set; }
    }
}
