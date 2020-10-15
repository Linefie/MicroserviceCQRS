using CQRS.WebApi.Context;
using CQRS.WebApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.WebApi.Features.ProductFeatures.Queries
{
    //suggests that we intend to return an IEnumerable list of Products from this Class implementing
    //the IRequest interface of MediatR
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
        //Every request must have a request handler
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
        {
            private readonly IApplicationContext _context;
            public GetAllProductsQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            // define how the request is being handled. 
            //This is almost the same for all types of requests and commands
            public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
            {
                var productList = await _context.Products.ToListAsync();
                if (productList == null)
                {
                    return null;
                }
                return productList.AsReadOnly();
            }

        }
    }
}
