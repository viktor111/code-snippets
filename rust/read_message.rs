use std::{error::Error};

use tokio::{net::TcpStream, io::AsyncReadExt};

/// Read a message from a TCP stream and return it as a String
/// 
/// # Arguments
/// 
/// * `stream` - The stream to read from
/// 
/// # Returns
/// 
/// * Result<String, Box<dyn Error>> - The message
/// 
/// # Errors
/// 
/// * If the message cannot be read

pub async fn read_message(stream: &mut TcpStream) -> Result<String, Box<dyn Error>> {
    let mut buffer = [0; 1024];
    let bytes_read = stream.read(&mut buffer).await;

    match bytes_read {
        Ok(bytes_read) => {
            println!("bytes_read: {}", bytes_read);
            let message = String::from_utf8_lossy(&buffer[..bytes_read]);
            Ok(message.to_string())
        },
        Err(e) => Err(e.into()),
    }
}