﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class db_a8c2c8_blooddonationEntities : DbContext
    {
        public db_a8c2c8_blooddonationEntities()
            : base("name=db_a8c2c8_blooddonationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BloodBankStockTable> BloodBankStockTables { get; set; }
        public virtual DbSet<BloodBankTable> BloodBankTables { get; set; }
        public virtual DbSet<BloodGroupTable> BloodGroupTables { get; set; }
        public virtual DbSet<CityTable> CityTables { get; set; }
        public virtual DbSet<DonorTable> DonorTables { get; set; }
        public virtual DbSet<GenderTable> GenderTables { get; set; }
        public virtual DbSet<HospitalTable> HospitalTables { get; set; }
        public virtual DbSet<RequestTable> RequestTables { get; set; }
        public virtual DbSet<RequestTypeTable> RequestTypeTables { get; set; }
        public virtual DbSet<SeekerTable> SeekerTables { get; set; }
        public virtual DbSet<AccountStatusTable> AccountStatusTables { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }
        public virtual DbSet<UserTypeTable> UserTypeTables { get; set; }
    }
}
