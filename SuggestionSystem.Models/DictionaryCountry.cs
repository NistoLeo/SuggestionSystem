using System;
using System.Collections.Generic;

#nullable disable

namespace SuggestionSystem.Models
{
    public partial class DictionaryCountry
    {
        public DictionaryCountry()
        {
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
