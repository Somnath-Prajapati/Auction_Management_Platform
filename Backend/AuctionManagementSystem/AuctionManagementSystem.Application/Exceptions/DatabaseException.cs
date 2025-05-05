using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Application.Exceptions
{
    public class DatabaseException : ApplicationException
    {
        public DatabaseException(string msg) : base(msg)
        {

        }
    }
}
