pub fn new_contract() -> Contract<Http> {
    let transport = web3::transports::Http::new("").unwrap();
    let web3 = web3::Web3::new(transport);
    let address: Address = "0x42699A7612A82f1d9C36148af9C77354759b210b".parse().unwrap();
    let smart_contract = Contract::from_json(web3.eth(),address, include_bytes!("../../abi.json"));
    let smart_contract = match smart_contract {
        Ok(contract) => contract,
        Err(e) => {
            println!("{:?}", e);
            panic!("Failed to create contract");
        }
    };
    smart_contract
}