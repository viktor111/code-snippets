pub async fn updated_with_amount(provider: &Web3::<Http>, addresses: &mut Vec<(Address,u64)>){
    for el in addresses{
        let address = el.0;
        let balance = provider.eth().balance(address,None).await.unwrap();
        el.1 = balance.low_u64();
    }
}