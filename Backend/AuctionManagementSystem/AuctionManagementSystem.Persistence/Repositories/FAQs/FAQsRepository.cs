using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.FAQ;
using AuctionManagementSystem.Application.Dtos;
using AuctionManagementSystem.Application.Features.FAQs.Queries.GetFAQsQuery;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Persistence.Repositories.FAQs
{
    public class FAQsRepository:IGetAllFAQ
    {
        readonly AuctionManagementDbContext _context;

        public FAQsRepository(AuctionManagementDbContext context)
        {
            _context = context;
        }

        public  async Task<List<GetFaqDto>> GeAllCategory()
        {
            return await _context.getFaqDtos.FromSqlRaw("AuctionM_dbuser.GetAllCatagory").ToListAsync();

            
        }

        public async Task<IEnumerable<FaqDto>> GetAllFAQ()
        {
            return await _context.faqDtos.FromSqlRaw("AuctionM_dbuser.GetAllFAQ").ToListAsync();
        }



        //public async Task<int> InsertFAQ(FaqDto faq)
        //{
        //    var result = await _context.Database.ExecuteSqlRawAsync(
        //        "EXEC AuctionM_dbuser.InsertFAQ @Question = {0}, @Answer = {1}, @Category = {2}, @Tags = {3}",
        //        faq.Question, faq.Answer, faq.Category, faq.Tags
        //    );
        //    return result;
        //}


        //public async Task<int> UpdateFAQById(FaqDto faq)
        //{
        //    var result = await _context.Database.ExecuteSqlRawAsync(
        //        "EXEC AuctionM_dbuser.UpdateFAQById @Id = {0}, @Question = {1}, @Answer = {2}, @Category = {3}, @Tags = {4}",
        //        faq.Id, faq.Question, faq.Answer, faq.Category, faq.Tags
        //    );
        //    return result;
        //}


        //public async Task<int> DeleteFAQById(int id)
        //{
        //    var result = await _context.Database.ExecuteSqlRawAsync(
        //        "EXEC AuctionM_dbuser.DeleteFAQById @Id = {0}",
        //        id
        //    );
        //    return result;
        //}


    }
}
