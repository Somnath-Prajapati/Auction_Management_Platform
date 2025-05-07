using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Domain.Entities.Asset;

public partial class TblAssetCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string? Subcategory { get; set; }

    public decimal DepositPercentage { get; set; }

    public string? Details { get; set; }

    public decimal AdminFees { get; set; }

    public decimal AuctionFees { get; set; }

    public decimal BuyerCommission { get; set; }

    public DateTime RegistrationDeadline { get; set; }

    public string? Icon { get; set; }
    public string? DocumentPath { get; set; } // ✅ Added field for uploaded document


    public decimal? Vatpercentage { get; set; }

    public int StatusId { get; set; }

    public DateTime? CreatedDate { get; set; }      
    public DateTime? UpdatedDate { get; set; }      

    public int? Vatid { get; set; }

    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; }
    public virtual TblAssetStatus Status { get; set; }
    public virtual TblVatoption Vat { get; set; }
    public virtual ICollection<TblAsset> TblAssets { get; set; } = new List<TblAsset>();
    public virtual ICollection<TblAssetPaymentMethod> PaymentMethods { get; set; } = new List<TblAssetPaymentMethod>();
}
