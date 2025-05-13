using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.Assets
{
    public class AssetDetailDto
    {
        public int AssetId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }


    public class GetAssetDetailsDto
    {
        public int AssetId { get; set; }
        public List<AssetDetailDtosA> AssetDetails { get; set; }
    }

    public class AssetDetailDtosA
    {
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }

}
