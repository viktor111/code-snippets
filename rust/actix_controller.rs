#[post("/create")]
pub async fn create(req_body: web::Json<BlogPost>) -> impl Responder {
    HttpResponse::Created().json("")
}