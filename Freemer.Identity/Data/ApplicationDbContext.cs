﻿using Freemer.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Freemer.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Category> Categories { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
            
        //}
    }
}
