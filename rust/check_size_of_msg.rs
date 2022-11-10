use std::error::Error;

use tokio::net::TcpStream;

/// Check the size of a message before reading it.
/// # Arguments
/// * `stream` - The reference stream to read from.
/// 
/// # Returns
/// * Result<usize, Box<dyn Error>> - The size of the message.
/// 
/// # Errors
/// * If the size of the message cannot be read.
/// 

pub async fn check_size_of_msg(stream: &mut TcpStream) -> Result<(), Box<dyn Error>> {
    let mut buffer = [0; 1024];
    let bytes_peeked = stream.peek(&mut buffer).await;

    match bytes_peeked {
        Ok(bytes_peeked) => {
            println!("bytes_peeked: {}", bytes_peeked);
            Ok(())
        },
        Err(e) => Err(e.into()),
    }
}