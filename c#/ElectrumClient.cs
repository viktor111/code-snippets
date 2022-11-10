public class Electrum
    {
        public Electrum(string username, string password, string url)
        {
            this.Validate(username, password, url);

            this.Username = username;
            this.Password = password;
            this.EncodedCredentials = Encoder.EncodeCredentialsToBase64(username, password);
            this.Url = url;
        }

        public string Username { get; private set; } = default!;

        public string Password { get; private set; } = default!;

        public string EncodedCredentials { get; private set; } = default!;

        public string Url { get; private set; } = default!;

        private void ValidateUsername(string username)
        {
            Guard.AgainstEmptyString<InvalidElectrumException>(username);
        }

        private void ValidatePassword(string password)
        {
            Guard.AgainstEmptyString<InvalidElectrumException>(password);
        }

        private void ValidateUrl(string url)
        {
            Guard.AgainstEmptyString<InvalidElectrumException>(url);
        }

        private void Validate(string username, string password, string url)
        {
            this.ValidateUsername(username);
            this.ValidatePassword(password);
            this.ValidateUrl(url);
        }

        public async Task<DepositToAddressResponse> PayTo(HttpClient httpClient, WithdrawDto withdrawDto)
        {
            var amountInBtc = decimal.Parse(withdrawDto.Amount, CultureInfo.InvariantCulture);

            using (var request = new HttpRequestMessage(new HttpMethod("POST"), this.Url))
            {
                var requestData = new DepositToAddressDto()
                {
                    Id = "1",
                    Jsonrpc = "2.0",
                    Method = "payto",
                    Params = new DepositToAddressDtoParams()
                    {
                        Destination = withdrawDto.Address,
                        Amount = withdrawDto.Amount,
                        FeeRate = 200,
                        Fee = "0.000001"
                    }
                };

                var json = JsonConvert.SerializeObject(requestData);
                request.Headers.Add("Authorization", "Basic " + this.EncodedCredentials);
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                var response = await httpClient.SendAsync(request);

                var result = await response.Content.ReadAsStringAsync();

                var depositRespones = JsonConvert.DeserializeObject<DepositToAddressResponse>(result);

                return depositRespones;
            }
        }

        public async Task<BroadcastResponse> Broadcast(HttpClient httpClient,string transaction)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), this.Url))
            {
                var requestData = new BroadcastDto() { Id = "1", Jsonrpc = "2.0", Method = "broadcast", Params = new BroadcastDtoParams() { Tx = transaction} };

                var json = JsonConvert.SerializeObject(requestData);
                request.Headers.Add("Authorization", "Basic " + this.EncodedCredentials);
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                var response = await httpClient.SendAsync(request);

                var result = await response.Content.ReadAsStringAsync();

                var broadcastResponse = JsonConvert.DeserializeObject<BroadcastResponse>(result);

                return broadcastResponse;
            }
        }

        public async Task<TransactionDataResponse> GetTransactionData(HttpClient httpClient, string tid)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), this.Url))
            {
                var data = new TransactionDataDto() { };
                var requestData = new TransactionDataDto() { Id = "1", Jsonrpc = "2.0", Method = "deserialize", Params = new TransactionDataDtoParams() { Txid = tid } };

                var json = JsonConvert.SerializeObject(requestData);
                request.Headers.Add("Authorization", "Basic " + this.EncodedCredentials);
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                var response = await httpClient.SendAsync(request);

                var result = await response.Content.ReadAsStringAsync();

                var transactionData = JsonConvert.DeserializeObject<TransactionDataResponse>(result);

                return transactionData;
            }
        }

        public async Task<GetTransactionFromIdResponse> GetTransactionFromId(HttpClient httpClient, GetTransactionHashFromIdDto getTransactionHashFromIdDto)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), this.Url))
            {
                var json = JsonConvert.SerializeObject(getTransactionHashFromIdDto);
                request.Headers.Add("Authorization", "Basic " + this.EncodedCredentials);
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                var response = await httpClient.SendAsync(request);

                var result = await response.Content.ReadAsStringAsync();

                var transaction = JsonConvert.DeserializeObject<GetTransactionFromIdResponse>(result);

                return transaction;
            }
        }

        public async Task<GetTransactionHistoryForAddressResponse> GetTransactionHistoryForAddress(HttpClient httpClient, GetAddressTransactionHistoryDto getAddressTransactionHistoryDto)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), this.Url))
            {
                var json = JsonConvert.SerializeObject(getAddressTransactionHistoryDto);
                request.Headers.Add("Authorization", "Basic " + this.EncodedCredentials);
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                var response = await httpClient.SendAsync(request);

                var result = await response.Content.ReadAsStringAsync();

                var transactionHsitoryForAddress = JsonConvert.DeserializeObject<GetTransactionHistoryForAddressResponse>(result);

                return transactionHsitoryForAddress;
            }
        }

        public async Task<string> LoadWallet(HttpClient httpClient)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), this.Url))
            {
                request.Headers.Add("Authorization", "Basic " + this.EncodedCredentials);
                request.Content = new StringContent("{\"id\":\"curltext\",\"jsonrpc\":\"2.0\",\"method\":\"load_wallet\"}");
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                var response = await httpClient.SendAsync(request);

                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
        }

        public async Task<AddressesResponse> GetAddresses(HttpClient httpClient, AddressesDto addressesDto)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), this.Url))
            {
                var json = JsonConvert.SerializeObject(addressesDto);
                request.Headers.Add("Authorization", "Basic " + this.EncodedCredentials);
                request.Content = new StringContent(json);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                var response = await httpClient.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AddressesResponse>(responseContent);
                return result;
            }
        }
    }
}