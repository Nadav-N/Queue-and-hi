﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class qnhdb : DbContext
    {
        public qnhdb()
            : base("name=qnhdb")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<answer_rankings> answer_rankings { get; set; }
        public DbSet<answer> answers { get; set; }
        public DbSet<question_rankings> question_rankings { get; set; }
        public DbSet<question> questions { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<tag> tags { get; set; }
        public DbSet<user> users { get; set; }
        public DbSet<notification> notifications { get; set; }
    }
}
