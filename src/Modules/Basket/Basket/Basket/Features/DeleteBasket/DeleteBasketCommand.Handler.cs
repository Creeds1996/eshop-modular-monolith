namespace Basket.Basket.Features.DeleteBasket;

internal class DeleteBasketCommandHandler(IBasketRepository repository)
    : ICommandHandler<DeleteBasketCommand, DeleteBasketCommandResponse>
{
    public async Task<DeleteBasketCommandResponse> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteBasket(command.UserName, cancellationToken);

        return new DeleteBasketCommandResponse(result);
    }
}