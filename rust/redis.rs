use std::collections::HashMap;

extern crate redis;

fn main() -> redis::RedisResult<()>{
    let client = redis::Client::open("redis://127.0.0.1/")?;
    let mut con = client.get_connection()?;

    return Ok(());
}

fn set_str(key: &str, value: &str, con: &mut redis::Connection) -> redis::RedisResult<()>{
    let _ : () = redis::cmd("SET").arg(key).arg(value).query(con)?;

    return Ok(());
}

fn get_str(key: &str, con: &mut redis::Connection) -> String {
    let res: String = redis::cmd("GET").arg("my_name").query(con).unwrap();

    return res;
}

fn set_num(key: &str, value: i64, con: &mut redis::Connection) -> redis::RedisResult<()>{
    let _ : () = redis::cmd("SET").arg(key).arg(value).query(con)?;

    return Ok(());
}

fn get_num(key: &str, con: &mut redis::Connection) -> i64{
    let res: i64 = redis::cmd("GET").arg("my_name").query(con).unwrap();

    return res;
}