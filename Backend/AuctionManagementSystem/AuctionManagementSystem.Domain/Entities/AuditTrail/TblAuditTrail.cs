using System;
using AuctionManagementSystem.Domain.Entities.User;

namespace AuctionManagementSystem.Domain.Entities.AuditTrail
{
    public partial class TblAuditTrail
    {
        public int AuditId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public DateTime? ActivityPerformedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public string ModelName { get; set; }

        public string ChangeType { get; set; }

        public int? RecordId { get; set; }

        public string? BeforeChange { get; set; }

        public string? AfterChange { get; set; }

        public virtual TblRole Role { get; set; }

        public virtual TblUser User { get; set; }
    }
}
