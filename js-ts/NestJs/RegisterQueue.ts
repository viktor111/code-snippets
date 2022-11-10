@Module({
    imports: [
      HttpModule,
      BullModule.forRoot({
        redis: {
          host: 'redis-service-cluster',
          port: 6379,
        },
      }),
      BullModule.registerQueue({
        name: 'deposit',
        defaultJobOptions: {
          attempts: 10,
          backoff: {
            type: 'exponential',
            delay: 1000,
          }
        }
      }),
      BullModule.registerQueue({
        name: 'withdraw',
        defaultJobOptions: {
          attempts: 10,
          backoff: {
            type: 'exponential',
            delay: 1000,
          }
        }
      }),
    ],
    controllers: [AppController],
    providers: [
      AppService,
      DepositProcessor,
      WithdrawProcessor
    ],
  })
  export class AppModule {}