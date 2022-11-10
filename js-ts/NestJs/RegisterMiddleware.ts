export class AppModule implements NestModule{
    configure(consumer: MiddlewareConsumer) {
      consumer.apply(PrivateKeyMiddleware).forRoutes('*');
    }
  }