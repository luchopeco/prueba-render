using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace price_list.Infraestructura.Repository
{
    public class GoogleSheetRepository : IGoogleSheetRepository
    {

        private readonly string _credentialPath;

        public GoogleSheetRepository(string credentialPath)
        {
            _credentialPath = credentialPath;
        }

        public async Task<List<object>> GetAllRowsAsync(string spreadsheetId, string range)
        {
            GoogleCredential credential = GoogleCredential.FromFile(_credentialPath)
                .CreateScoped(SheetsService.Scope.SpreadsheetsReadonly);

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Sheets API .NET"
            });

            var request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = await request.ExecuteAsync();

            var rows = new List<object>();

            if (response.Values != null)
            {
                foreach (var row in response.Values)
                {
                    rows.Add((object)row);
                }
            }

            return rows;
        }
    }
}
