using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserHub.Model.Entities
{
    public class Tenancy
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public ApplicationUser CreatedBy { get; set; }
    }
}
