﻿using HS14MVC.Domain.Core.BaseEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS14MVC.Domain.Core.BaseEntityConfigurations
{
    public class BaseUserConfiguration<TEntity> : AuditableEntityConfiguration<TEntity> where TEntity : BaseUser
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Email).IsRequired();
            base.Configure(builder);
        }
    }
}