﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Basket.Application.Commands
{
    public class DeleteBasketByUserNameCommand : IRequest
    {
        public string UserName { get; set; }

        public DeleteBasketByUserNameCommand(string userName)
        {
            UserName = userName;
        }
    }
}
