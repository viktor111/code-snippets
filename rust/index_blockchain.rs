use web3::types::BlockId;
use web3::types::BlockNumber;
use web3::types::TransactionId;
use web3::types::H160;
use web3::types::U64;

const JSON_RPC_PROVIDER: &str = "https://ropsten.infura.io/v3/ff21ecb60a304fc09a7b37e3d979f778";

#[tokio::main]
async fn main() -> web3::Result {

    deposit_job().await.unwrap_or_else(|e| {
        println!("Error: {:?}", e);
    } );

    Ok(())
}

async fn deposit_job() -> web3::Result {
    let transport = web3::transports::Http::new(
        "https://ropsten.infura.io/v3/ff21ecb60a304fc09a7b37e3d979f778",
    )?;
    let web3 = web3::Web3::new(transport);

    let addresses = [
        "0x81790B9b4baBd2b478d44560BEe8aa6D495FF0D9",
        "0xC42976553365935A8359F37cB58321F1197DC1A4",
        "0xC91f227D49094F4C6663BAf6CE2A5dF9B50d6D54",
        "0x0A7d7E85eD6e81eb18C51Bf4D926dC654111E321",
        "0xdA4A69E146eC7EF3255fE39a3f69853216e179Ae",
        "0x1E1d92F697877e0b6a0C2B66f598616d7Ab2CCB1",
        "0x749C274c1cB96F5C832928C7E1c252bd35632b2B",
    ];

    let current_block_number = web3.eth().block_number().await?;

    let current_block_number = current_block_number.0[0];

    let block_num_1hr_ago = current_block_number - (2 * 60 * 60 / 15);

    for n in block_num_1hr_ago..current_block_number {
        let block_number = BlockNumber::from(n);
        let block_id = BlockId::from(block_number);
        let block = web3.eth().block(block_id).await?;

        let block = block.unwrap();

        println!(
            "Block with ID: {:?} and tx count: {:?}",
            block.number.unwrap(),
            block.transactions.len()
        );

        let txs = block.transactions;

        tokio::spawn(async move {
            for tx in txs {
                let tx_id = TransactionId::from(tx);
                let adr = addresses[0];
                process_tx(tx_id, adr).await.unwrap_or_else(|e| {
                    println!("Error: {:?}", e);
                } );
            }
        });
    }

    Ok(())
}

async fn process_tx(tx: TransactionId, address_id: &str) -> Result<(), web3::Error> {
    let transport = web3::transports::Http::new(
        "https://ropsten.infura.io/v3/ff21ecb60a304fc09a7b37e3d979f778",
    )?;
    let web3 = web3::Web3::new(transport);
    let tx = web3.eth().transaction(tx).await?;
    if tx.is_some() {
        let tx = tx.unwrap();
        if tx.to.is_some() {
            let to = tx.to.unwrap();


        }

       // println!("Transaction with ID: {:?}", tx.hash);
    }
    Ok(())
}