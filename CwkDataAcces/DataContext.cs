using CwkDataAcces.Configuration;
using CwkDomain.Aggragrate.PostAggragrate;
using CwkDomain.Aggragrate.UserProfileAggragrate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkDataAcces
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions options)
        : base(options) { }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Ignore<BasicInfo>();
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            modelBuilder.ApplyConfiguration(new IdentityUserLoginCongig());
            modelBuilder.ApplyConfiguration(new IdentityUserTokenConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserRoleConfig());
            modelBuilder.ApplyConfiguration(new PostInteractionConfig());
            modelBuilder.ApplyConfiguration(new PostCommentConfig());
            modelBuilder.ApplyConfiguration(new UserProfileConfig());
            modelBuilder.ApplyConfiguration(new PostModelConfig());
        }
    }
}




