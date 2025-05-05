using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.RequestsDtos;
using AuctionManagementSystem.Domain.Entities.Request;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Requests.Command.AddRequest
{
    public class AddRequestCommand : IRequest<CreateRequestDto>
    {
        public string RequestNumber { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int RequestTypeId { get; set; }
        public int AssetId { get; set; }
        public int TransactionId { get; set; }
        public DateTime RequestDateTime { get; set; }
        public int RequestStatusId { get; set; }
        public string CustomerNote { get; set; }
        public string AdminNote { get; set; }
        public bool CreatedByAdmin { get; set; }
    }
}