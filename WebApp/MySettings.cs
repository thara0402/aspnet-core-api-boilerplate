namespace WebApp
{
    public class MySettings
    {
        public string SqlConnection { get; set; } = null!;
        public string CosmosConnection { get; set; } = null!;
        public string StorageConnection { get; set; } = null!;
        public string AppInsightsInstrumentationKey { get; set; } = null!;
    }
}
