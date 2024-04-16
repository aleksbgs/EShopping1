using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Extensions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<DeleteOrderCommand> _logger;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository,
            ILogger<DeleteOrderCommand> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }


        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);

            if (orderToDelete == null)
            {
                throw new OrderNotFoundException(nameof(Order), request.Id);
            }

            await _orderRepository.DeleteAsync(orderToDelete);
            _logger.LogInformation($"Order with id{request.Id} is deleted successfully");

            return Unit.Value;


        }
    }
}
