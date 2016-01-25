namespace Go
{
    public class Credentials
    {
        private readonly string basicAuthCred;

        public Credentials(string basicAuthCred)
        {
            this.basicAuthCred = basicAuthCred;
        }
        public string BasicAuthCredentials => basicAuthCred;
    }
}
