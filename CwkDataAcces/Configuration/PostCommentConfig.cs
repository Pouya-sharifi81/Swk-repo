using CwkDomain.Aggragrate.PostAggragrate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkDataAcces.Configuration
{
    internal class PostCommentConfig : IEntityTypeConfiguration<PostComment>
    {
        

        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.HasKey(c => c.CommentId);
        }
    }
}
