//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ViviPlusTT.Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResponseData
    {
        public long Id { get; set; }
        public int ResponseId { get; set; }
        public int CustomerId { get; set; }
        public long InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountConverted { get; set; }
        public string Account { get; set; }
        public string AccountType { get; set; }
        public string Creditcard { get; set; }
        public string CardExpiry { get; set; }
    
        public virtual Response Response { get; set; }
    }
}
