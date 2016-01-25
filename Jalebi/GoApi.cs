namespace Go
{
    public class GoApi
    {
        private readonly Credentials _credentials;
        private readonly HttpWrapper _httpWrapper;
        protected HttpWrapper Dispatch => _httpWrapper;
        protected readonly GoUrls Urls;

        public GoApi(Credentials credentials, GoUrls urls)
        {
            _credentials = credentials;
            _httpWrapper = new HttpWrapper(credentials);
            this.Urls = urls;
        }
    }
}