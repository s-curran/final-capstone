﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    public class ChangeStartVM
    {
        public int itineraryId { get; set; }
        public string newStartingPoint { get; set; }
        
    }
}
