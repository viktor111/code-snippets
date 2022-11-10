async fn main() {
    let mut interval = time::interval(Duration::from_secs(10));
        loop {
            interval.tick().await;
            deposit_job().await;
        }
}