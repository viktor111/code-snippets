public class ElectrumFactory : IElectrumFactory
    {
        private string username = default!;
        private string password = default!;
        private string url = default!;

        public IElectrumFactory WithUsername(string username)
        {
            this.username = username;
            return this;
        }

        public IElectrumFactory WithPassword(string password)
        {
            this.password = password;
            return this;
        }

        public IElectrumFactory WithUrl(string url)
        {
            this.url = url;
            return this;
        }

        public Electrum Build()
        {
            return new Electrum(this.username, this.password, this.url);
        }

    }