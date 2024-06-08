﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS14MVC.Domain.Core.BaseEntities
{
    public class AuditableEntity : BaseEntity
    {
        public DateTime? DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
    }
}
