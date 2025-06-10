using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Translations
{
    public class CreateAuctionCategoryTranslationDto
    {
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        public string TranslatedName { get; set; }
    }
}
