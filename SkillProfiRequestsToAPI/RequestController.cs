namespace SkillProfiRequestsToAPI
{
    public class RequestController
    {
        public RequestController(Func<string> getBaseUrl, string originalUrl)
        {
            _originalUrl = originalUrl;
            GetBaseUrl = getBaseUrl;
        }
        protected string Url { get => GetBaseUrl() + _originalUrl; }

        private readonly string _originalUrl;

        private readonly Func<string> GetBaseUrl;

    }
}
