using MediatR;
using price.list.Models.PriceList.Respomse;

namespace price_list.Application.Queries
{
    public class GetPriceListDataQuery : IRequest<GetResponse>
    {
    }
}
