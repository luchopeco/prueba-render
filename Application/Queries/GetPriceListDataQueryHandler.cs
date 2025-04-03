using MediatR;
using price.list.Models.PriceList.Respomse;
using price_list.Infraestructura.Repository;

namespace price_list.Application.Queries
{
    public class GetPriceListDataQueryHandler : IRequestHandler<GetPriceListDataQuery, GetResponse>
    {
        private readonly IGoogleSheetRepository _googleSheetRepository;
        public GetPriceListDataQueryHandler(IGoogleSheetRepository googleSheetRepository)
        {
            _googleSheetRepository = googleSheetRepository;
        }

        public async Task<GetResponse> Handle(GetPriceListDataQuery request, CancellationToken cancellationToken)
        {
            var listRows =  await _googleSheetRepository.GetAllRowsAsync("1TaF8qKBJxh3CkxTDYJJxiGL8jyT8X939DdvefGvJGG8", "productos");
            return new GetResponse
            {
                ProductList = mapRowsToProducts(listRows)
            };

        }

        private List<Product> mapRowsToProducts(List<object> listRows)
        {
            var productos = new List<Product>();

            // Iteramos por cada fila de rows
            for (int i = 1; i < listRows.Count; i++) // Skip(1) para omitir la primera fila (encabezados)
            {
                var row = (List<object>)listRows[i]; // Cada fila es una lista de objetos

                var producto = new Product
                {
                    Name = row.Count > 0 ? row[0]?.ToString() : null,
                    Description = row.Count > 1 ? row[1]?.ToString() : null,
                    Price = row.Count > 2 && decimal.TryParse(row[2]?.ToString(), out var precio) ? precio : (decimal?)null,
                    UrlImage = row.Count > 3 ? row[3]?.ToString() : null,
                    Brand = row.Count > 4 ? row[4]?.ToString() : null
                };

                productos.Add(producto);
            }

            return productos;
        }
    }
}
