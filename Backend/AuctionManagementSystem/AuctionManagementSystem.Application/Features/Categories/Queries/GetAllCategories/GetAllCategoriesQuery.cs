using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Categories;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery : IRequest<List<CategoryDto>>;

    
}
