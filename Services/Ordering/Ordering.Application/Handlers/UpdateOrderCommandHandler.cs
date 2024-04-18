using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exception;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _maper;
        private readonly ILogger<UpdateOrderCommand> _logger;


        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper maper,
            ILogger<UpdateOrderCommand> logger)
        {
            _orderRepository = orderRepository;
            _maper = maper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {

            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);

            if (orderToUpdate == null)
            {
                throw new OrderNotFoundException(nameof(Order), request.Id);
            }

            _maper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));


            await _orderRepository.UpdateAsync(orderToUpdate);

            _logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated");

            return Unit.Value;
        }
    }
}
