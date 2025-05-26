using System;

namespace AuctionManagementSystem.Application.Dtos.AuditTrial
{
    public class AuditTrailDto
    {
        public int AuditId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime? ActivityPerformedAt { get; set; }
        public string CreatedBy { get; set; }
        public string? ModelName { get; set; }         
        public string? ChangeType { get; set; }         
        public int? RecordId { get; set; }              
        public string? BeforeChange { get; set; }      
        public string? AfterChange { get; set; }       
    }
}
