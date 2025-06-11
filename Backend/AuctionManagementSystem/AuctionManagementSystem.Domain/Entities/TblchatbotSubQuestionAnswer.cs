using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.model;

namespace AuctionManagementSystem.Domain.Entities;

    public partial class TblchatbotSubQuestionAnswer
    {
        public int Id { get; set; }

        public int MainQuestionId { get; set; }

        public int? ParentSubQuestionId { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public virtual ICollection<TblchatbotSubQuestionAnswer> InverseParentSubQuestion { get; set; } = new List<TblchatbotSubQuestionAnswer>();

        public virtual TblChatBotMainQuestionAnswer MainQuestion { get; set; }

        public virtual TblchatbotSubQuestionAnswer ParentSubQuestion { get; set; }
    }

