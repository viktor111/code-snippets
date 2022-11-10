#[actix_web::main]
async fn main() -> std::io::Result<()> {

    env_logger::init_from_env(Env::default().default_filter_or("info"));

    HttpServer::new(|| {
        App::new()
            .app_data(web::Data::new( DbContextData::new(
                "blog_rust_db".to_string(),
                "viktordraganov".to_string(),
                "viktor11".to_string(),
                "localhost:5432".to_string(),
            )))
            .wrap(Logger::default())
            .service(health)
            .service(
                web::scope("/posts")
                    .service(create)
                    .service(details)
                    .service(list),
            )
    })
    .bind(("127.0.0.1", 8080))?
    .run()
    .await
}