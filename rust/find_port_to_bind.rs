use std::net::{SocketAddr, IpAddr, Ipv4Addr};

use tokio::net::TcpListener;
/// Finds a valid port to bind to given a range of ports and returns it. If no valid port is found, returns 0.
/// 
/// # Arguments
/// 
/// * `from` - The port to start searching from
/// * `to` - The port to stop searching at
/// 
/// # Returns
/// 
/// * Result<SocketAddr, Box<dyn Error>> - The port to bind to
/// 

pub async fn find_port_to_bind(from: u16, to: u16) -> Result<u16, u16> {

    for port in from..to {
        let addr = SocketAddr::new(IpAddr::V4(Ipv4Addr::new(127, 0, 0, 1)), port);
        let listener = TcpListener::bind(addr).await;    
        if listener.is_ok() {
            return Ok(port);
        }
    }
    return Ok(0);
}
