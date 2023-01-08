﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPUScraper.Models.Models
{
    public class GPUQuery
    {
        public string? searchPhrase { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
