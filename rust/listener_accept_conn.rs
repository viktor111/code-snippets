use std::{net::SocketAddr, error::Error};

use tokio::net::{TcpListener, TcpStream};
/// Accept a connection from a client
/// 
/// # Arguments
/// 
/// * `listener` - The listener to accept a connection from
/// 
/// # Returns
/// 
/// * Result<TcpStream, Box<dyn Error>> - The stream to the client
/// 
/// # Errors
/// 
/// * If the connection cannot be accepted

pub async fn listener_accept_conn(listener: &TcpListener) -> Result<(TcpStream, SocketAddr), Box<dyn Error>> {
    let accepted = listener.accept().await;

    match accepted {
        Ok((stream, addr)) => {
            Ok((stream, addr))
        },
        Err(e) => Err(e.into()),
    }
}