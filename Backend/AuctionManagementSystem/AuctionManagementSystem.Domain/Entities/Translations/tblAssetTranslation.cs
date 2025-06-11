using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Asset;

namespace AuctionManagementSystem.Domain.Entities.Translations
{
    public class tblAssetTranslation
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int LanguageId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? SalesNotes { get; set; }
        public DateTime CreatedAt { get; set; }

        public TblAsset? Asset { get; set; }
        public tblLanguages? Language { get; set; }

    }

}
