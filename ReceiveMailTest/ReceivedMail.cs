//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReceiveMailTest
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReceivedMail
    {
        public int Id { get; set; }
        public string MessageId { get; set; }
        public string Uid { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string SendBy { get; set; }
        public string Cc { get; set; }
        public System.DateTime ReceiveDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public byte Status { get; set; }
    }
}