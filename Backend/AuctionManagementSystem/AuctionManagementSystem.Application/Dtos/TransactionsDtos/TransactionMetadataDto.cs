using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Dtos.TransactionsDtos
{
    public class CardTypeDto
    {
        public int CardTypeId { get; set; }

        public string CardTypeName { get; set; }
    }

    public class TransactionTypeDto
    {
        public int TransactionTypeId { get; set; }

        public string TransactionTypeName { get; set; } = null!;
    }

    public class PaymentMethodDto
    {
        public int PaymentMethodId { get; set; }

        public string PaymentMethodName { get; set; }
    }

    public class TransactionStatusDto
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; } = null!;
    }

    public class TransactionMetadataDto
    {
        public List<CardTypeDto> CardTypes { get; set; }
        public List<TransactionTypeDto> TransactionTypes { get; set; }
        public List<PaymentMethodDto> PaymentMethods { get; set; }
        public List<TransactionStatusDto> Statuses { get; set; }
    }

}
