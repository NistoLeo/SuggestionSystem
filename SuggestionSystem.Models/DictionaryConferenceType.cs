﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SuggestionSystem.Models
{
    public partial class DictionaryConferenceType
    {
        public DictionaryConferenceType()
        {
            Conferences = new HashSet<Conference>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Conference> Conferences { get; set; }
    }
}
