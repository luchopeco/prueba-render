
namespace price_list.Infraestructura.Repository
{
    public interface IGoogleSheetRepository
    {
        Task<List<object>> GetAllRowsAsync(string spreadsheetId, string range);
    }
}