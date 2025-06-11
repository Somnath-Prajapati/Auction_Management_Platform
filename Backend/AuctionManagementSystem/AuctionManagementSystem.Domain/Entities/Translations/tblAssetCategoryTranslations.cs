using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Domain.Entities.Translations
{
    public class tblAssetCategoryTranslations
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public int LanguageId { get; set; }

        public string? TranslatedCategoryName { get; set; }
        public string? TranslatedSubcategory { get; set; }
        public string? TranslatedDetails { get; set; }

        public DateTime CreatedAt { get; set; }

        public TblAssetCategory? Category { get; set; }
        public tblLanguages? Language { get; set; }
    }

}
