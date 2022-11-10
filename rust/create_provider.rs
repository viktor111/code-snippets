pub async fn create_provider(url: &str) -> Web3<Http>{
    let transport = web3::transports::Http::new(url).unwrap();
    let provider = web3::Web3::new(transport); 
    return provider;
}