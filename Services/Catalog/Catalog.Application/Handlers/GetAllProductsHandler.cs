using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllProductsHandler:IRequestHandler<GetAllProductsQueries,Pagination<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;


        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQueries request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetProducts(request.CatalogSpecParams);

            var productResponseList = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(productList);

            return productResponseList;
        }
    }
}
