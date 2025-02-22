﻿using Amazon.Runtime.Internal;
using Catalog.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllBrandsQuery : IRequest<IList<BrandResponse>>
    {
    }
}
