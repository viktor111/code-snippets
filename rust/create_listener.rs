use std::{error::Error, net::SocketAddr};

use tokio::net::TcpListener;

/// Create a listener on a port
/// 
/// # Arguments
/// 
/// * `port` - The port to bind to
/// 
/// # Returns
/// 
/// * Result<TcpListener, Box<dyn Error>> - The listener

pub async fn create_listener(addr: SocketAddr) -> Result<TcpListener, Box<dyn Error>> {
    let listener = TcpListener::bind(addr).await;

    match listener {
        Ok(listener) => Ok(listener),
        Err(e) => Err(e.into()),
    }
}