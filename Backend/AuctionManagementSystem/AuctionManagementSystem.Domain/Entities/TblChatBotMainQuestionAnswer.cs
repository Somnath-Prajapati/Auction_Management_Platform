using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Domain.Entities;

public partial class TblChatBotMainQuestionAnswer
{
    public int Id { get; set; }

    public string Question { get; set; }

    public string Answer { get; set; }

    public virtual ICollection<TblchatbotSubQuestionAnswer> TblchatbotSubQuestionAnswers { get; set; } = new List<TblchatbotSubQuestionAnswer>();
}