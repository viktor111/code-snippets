use std::error::Error;

/// Validate an IP address
/// A valid ip address is in the format of "0.0.0.0:1111"
/// And the values of the IP address parts are between 0 and 255
/// 
/// # Arguments
/// 
/// * `ip` - The IP address to validate
/// 
/// # Returns
/// 
/// * Result<bool, Box<dyn Error>> - True if the IP address is valid, false otherwise
/// 
/// # Errors
/// 
/// * If the IP address argument is invalid

pub fn validate_ip_address(ip: String) -> Result<bool, Box<dyn Error>> {
    const INVALID_IP_ERROR: &str = "Invalid IP address -";

    let ip = ip.split(":").collect::<Vec<&str>>()[0];
    
    let ip_parts = ip.split(".").collect::<Vec<&str>>();

    if ip_parts.len() != 4 {
        return Err(format!("{} should be in format:", INVALID_IP_ERROR).into());
    }

    let mut ip_parts_u8 = Vec::new();
    for part in ip_parts {
        let part_u8 = part.parse::<u8>();
        if part_u8.is_err() {
            return Err(format!("{} max 255 and min 0", INVALID_IP_ERROR).into());
        }
        ip_parts_u8.push(part_u8.unwrap());
    }

    return Ok(true);
}