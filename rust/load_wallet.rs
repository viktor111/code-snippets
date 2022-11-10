fn load_wallet() -> Vec<(Address, u64, SecretKey)>{
    let mut result: Vec<(Address, u64, SecretKey)> = Vec::new();
    for i in 1..10{
        
        let key_pair = generate_key_pair(i);
        let address = public_key_address(&key_pair.1);
        result.push((address, 0, key_pair.0));
    }
    return result;
}