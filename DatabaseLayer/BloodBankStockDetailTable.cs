//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class BloodBankStockDetailTable
    {
        public int BloodBankStockDetailID { get; set; }
        public int BloodBankStockID { get; set; }
        public int BloodGroupID { get; set; }
        public double Quantity { get; set; }
        public int DonorID { get; set; }
        public System.DateTime DonateDateTime { get; set; }
    
        public virtual BloodBankStockTable BloodBankStockTable { get; set; }
        public virtual BloodGroupTable BloodGroupTable { get; set; }
        public virtual DonorTable DonorTable { get; set; }
    }
}
