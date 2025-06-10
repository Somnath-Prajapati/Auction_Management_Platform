using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Auction;

namespace AuctionManagementSystem.Domain.Entities.Translations
{
    public class tblAuctionCategoriesTranslations
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        public string TranslatedName { get; set; }
        public DateTime CreatedAt { get; set; }

        
        public TblAuctionCategory Category { get; set; }
        public tblLanguages Language { get; set; }
    }
}
