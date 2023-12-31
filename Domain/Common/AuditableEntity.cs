﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }

        public string? CreatedById { get; set; }


        public DateTime? LastModified { get; set; }

        public string? LastModifiedById { get; set; }

    }
}
