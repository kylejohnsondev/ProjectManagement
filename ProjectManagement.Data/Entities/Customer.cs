﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Data.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public DateTimeOffset CustomerAdded { get; set; }
        public DateTimeOffset? LastUpdated { get; set; }
    }
}
