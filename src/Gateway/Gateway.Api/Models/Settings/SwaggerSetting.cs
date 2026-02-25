namespace Gateway.Api.Models.Settings
{
    public class SwaggerSetting
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }  = string.Empty;
        public string Version { get; set; }  = string.Empty;
        public string TermsOfService { get; set; }  = string.Empty;
        public SwaggerContact Contact { get; set; } = new();
        public SwaggerLicense License { get; set; } = new();
    }

    public class SwaggerContact
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }

    public class SwaggerLicense
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}