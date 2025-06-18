using AuctionManagementSystem.Application.Dtos;
using AuctionManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionManagementSystem.Persistence.Context;
using MediatR;
using AuctionManagementSystem.Application.Features.FAQs.Queries.GetFAQsQuery;
using AuctionManagementSystem.Application.Features.FAQs.Queries.GetAllFAQsQuery;

namespace AuctionManagementSystem.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaqController : ControllerBase
    {
        private readonly AuctionManagementDbContext _context;
        readonly IMediator _mediatR;
        public FaqController(AuctionManagementDbContext context, IMediator mediator)
        {
            _context = context;
            _mediatR = mediator;
        }

        // GET: api/faq
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<FaqDto>>> GetFaqs()
        //{
        //    var faqs = await _context.Faqs
        //        .Select(f => new FaqDto
        //        {
        //            Id = f.Id,
        //            Question = f.Question,
        //            Answer = f.Answer,
        //            Category = f.Category,
        //            Tags = f.Tags,
        //            CreatedAt = f.CreatedAt,
        //            UpdatedAt = f.UpdatedAt
        //        })
        //        .ToListAsync();
        //    return Ok(faqs);
        //}

     


        // GET: api/faq/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FaqDto>> GetFaq(int id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            if (faq == null)
                return NotFound();
            var dto = new FaqDto
            {
                Id = faq.Id,
                Question = faq.Question,
                Answer = faq.Answer,
                Category = faq.Category,
                Tags = faq.Tags,
                CreatedAt = faq.CreatedAt,
                UpdatedAt = faq.UpdatedAt
            };
            return Ok(dto);
        }

        //// POST: api/faq
        //[HttpPost]
        //public async Task<ActionResult<FaqDto>> CreateFaq([FromBody] FaqDto faqDto)
        //{
        //    var faq = new Faq
        //    {
        //        Question = faqDto.Question,
        //        Answer = faqDto.Answer,
        //        Category = faqDto.Category,
        //        Tags = faqDto.Tags,
        //        CreatedAt = System.DateTime.UtcNow,
        //        UpdatedAt = System.DateTime.UtcNow
        //    };
        //    _context.Faqs.Add(faq);
        //    await _context.SaveChangesAsync();
        //    faqDto.Id = faq.Id;
        //    faqDto.CreatedAt = faq.CreatedAt;
        //    faqDto.UpdatedAt = faq.UpdatedAt;
        //    return CreatedAtAction(nameof(GetFaq), new { id = faq.Id }, faqDto);
        //}



        // POST: api/faq
        [HttpPost]
        public async Task<ActionResult<FaqDto>> CreateFaq([FromBody] FaqDto faqDto)
        {
            // Check if a question with the same text already exists
            bool questionExists = await _context.Faqs
                .AnyAsync(f => f.Question.Trim().ToLower() == faqDto.Question.Trim().ToLower());

            if (questionExists)
            {
                return BadRequest("This question already exists.");
            }

            var faq = new Faq
            {
                Question = faqDto.Question,
                Answer = faqDto.Answer,
                Category = faqDto.Category,
                Tags = faqDto.Tags,
                CreatedAt = System.DateTime.UtcNow,
                UpdatedAt = System.DateTime.UtcNow
            };

            _context.Faqs.Add(faq);
            await _context.SaveChangesAsync();

            faqDto.Id = faq.Id;
            faqDto.CreatedAt = faq.CreatedAt;
            faqDto.UpdatedAt = faq.UpdatedAt;

            return CreatedAtAction(nameof(GetFaq), new { id = faq.Id }, faqDto);
        }



        // PUT: api/faq/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFaq(int id, [FromBody] FaqDto faqDto)
        {
            var faq = await _context.Faqs.FindAsync(id);
            if (faq == null)
                return NotFound();
            faq.Question = faqDto.Question;
            faq.Answer = faqDto.Answer;
            faq.Category = faqDto.Category;
            faq.Tags = faqDto.Tags;
            faq.UpdatedAt = System.DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //// DELETE: api/faq/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteFaq(int id)
        //{
        //    var faq = await _context.Faqs.FindAsync(id);
        //    if (faq == null)
        //        return NotFound();
        //    _context.Faqs.Remove(faq);
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}


        // DELETE: api/faq/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaq(int id)
        {
            var faq = await _context.Faqs.FindAsync(id);
            if (faq == null)
                return NotFound();

            // Soft delete by setting IsDeleted to true (1)
            faq.IsDeleted = true;
            faq.UpdatedAt = DateTime.UtcNow;

            _context.Faqs.Update(faq);
            await _context.SaveChangesAsync();

            return NoContent();
        }











        [HttpGet("GetCategory")]

        public async Task<IActionResult> GetAllcategory()
        {
            var result = await _mediatR.Send(new GetFAQsQuery());
            return Ok(result);
        }
        [HttpGet("GetAllFaq")]
        public async Task<ActionResult<IEnumerable<FaqDto>>> GetFaqs()
        {
            var result = await _mediatR.Send(new GetAllFAQsQuery());
            return Ok(result);
        }

        
    }
} 