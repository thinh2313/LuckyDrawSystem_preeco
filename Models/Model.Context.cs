﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PreecoLuckyDraw.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbPreecoLuckyDrawEntities : DbContext
    {
        public DbPreecoLuckyDrawEntities()
            : base("name=DbPreecoLuckyDrawEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CAMPAIGN> CAMPAIGNs { get; set; }
        public virtual DbSet<CAMPAIGN_DETAIL> CAMPAIGN_DETAIL { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEEs { get; set; }
        public virtual DbSet<GIFT> GIFTs { get; set; }
        public virtual DbSet<JOINER> JOINERs { get; set; }
        public virtual DbSet<LUCKYNUMBER> LUCKYNUMBERs { get; set; }
        public virtual DbSet<PRIZE> PRIZEs { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<WINNER> WINNERs { get; set; }
    }
}
