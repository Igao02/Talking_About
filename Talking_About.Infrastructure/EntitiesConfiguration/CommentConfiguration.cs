﻿using Talking_About.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Talking_About.Infrastructure.EntitiesConfiguration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CommentContent)
                .IsRequired()
                .HasMaxLength(1000);
                

            builder.Property(c => c.CommentDate)
                .IsRequired();
        }
    }
}
