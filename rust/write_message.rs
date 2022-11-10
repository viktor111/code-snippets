use std::error::Error;

use tokio::{net::TcpStream, io::AsyncWriteExt};

/// Write a message to a TCP stream
/// 
/// # Arguments
/// 
/// * `stream` - The stream to write to
/// * `message` - The message to write
/// 
/// # Returns
/// 
/// * Result<(), Box<dyn Error>> - Empty result
/// 
/// # Errors
/// 
/// * If the message cannot be written

pub async fn write_message(stream: &mut TcpStream, message: String) -> Result<(), Box<dyn Error>> {
    let written = stream.write(message.as_bytes()).await;

    match written {
        Ok(_) => Ok(()),
        Err(e) => Err(e.into()),
    }
}