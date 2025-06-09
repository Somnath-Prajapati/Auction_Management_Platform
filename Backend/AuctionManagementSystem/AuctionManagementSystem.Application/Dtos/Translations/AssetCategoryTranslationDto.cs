using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Translations
{
    public class AssetCategoryTranslationDto
    {
        public int CategoryId { get; set; }
        public string? TranslatedCategoryName { get; set; }
        public string? TranslatedSubcategory { get; set; }
        public string? TranslatedDetails { get; set; }
    }

}
