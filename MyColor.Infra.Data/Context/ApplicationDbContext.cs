﻿using Microsoft.EntityFrameworkCore;
using MyColor.Domain.Entities;
using System;
namespace MyColor.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}