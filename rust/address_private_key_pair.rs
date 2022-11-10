use std::error::Error;
use rand::{distributions::Alphanumeric, Rng};
use secp256k1::{
    rand::{rngs, SeedableRng},
    PublicKey, SecretKey,
};
use tiny_keccak::keccak256;
use web3::types::{Address, U256};
use web3::{
    transports::{WebSocket, Http},
    Web3,
};

fn generate_key_pair(seed: u64) -> (SecretKey, PublicKey){
    let secp = secp256k1::Secp256k1::new();
    let mut random = rngs::StdRng::seed_from_u64(seed);
    let result = secp.generate_keypair(&mut random);
    return result;
}


// [dependencies]
// web3 = "0.18.0"
// secp256k1 = { version = "0.24.0", features = ["rand"] }
// unicode-width = "0.1.9"
// rand = "0.8.5"
// tiny-keccak = { version = "1.4" }
// tokio = "1.20.1"