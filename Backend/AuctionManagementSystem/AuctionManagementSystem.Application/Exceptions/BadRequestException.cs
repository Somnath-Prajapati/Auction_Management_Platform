using System;
using System.Collections.Generic;

namespace AuctionManagementSystem.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<string> Errors { get; }

        public BadRequestException(string message) : base(message)
        {
            Errors = new List<string>();
        }

        public BadRequestException(string message, List<string> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
