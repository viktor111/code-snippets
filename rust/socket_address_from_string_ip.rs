use std::{net::{SocketAddr, IpAddr, Ipv4Addr}, error::Error};

/// Create a SocketAddr from IP and port as string in from of "0.0.0.0:1111"
/// 
/// # Arguments
/// 
/// * `ip` - The IP address as string
/// 
/// # Returns
/// 
/// * Result<SocketAddr, Box<dyn Error>> - The SocketAddr
/// 
/// # Errors
/// 
/// * If the IP address argument is invalid

pub fn socket_address_from_string_ip(ip: String) -> Result<SocketAddr, Box<dyn Error>> {

    const INVALID_IP_ERROR: &str = "Invalid IP address - should be in format: 127.0.0.1:8080";

    let ip = ip.split(":").collect::<Vec<&str>>();
    let port = ip[1].parse::<u16>().expect(INVALID_IP_ERROR);

    let ip_parts = ip[0].split(".").collect::<Vec<&str>>();

    if ip_parts.len() != 4 {
        return Err(INVALID_IP_ERROR.into());
    }
    
    let mut ip_parts_u8 = Vec::new();
    for part in ip_parts {
        let part_u8 = part.parse::<u8>();
        if part_u8.is_err() {
            return Err(INVALID_IP_ERROR.into());
        }
        ip_parts_u8.push(part_u8.unwrap());
    }

    let ip_addr = IpAddr::V4(Ipv4Addr::new(ip_parts_u8[0], ip_parts_u8[1], ip_parts_u8[2], ip_parts_u8[3]));

    let socket_addr = SocketAddr::new(ip_addr, port);

    return Ok(socket_addr);
}