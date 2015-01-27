using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserHub.Model.Entities
{
    public class SuggestionCategory
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public Tenancy Tenancy { get; set; }
    }
}
