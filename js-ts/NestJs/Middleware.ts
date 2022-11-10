@Injectable()
export class PrivateKeyMiddleware implements NestMiddleware {
    constructor(private readonly walletsService: WalletsService) {}
    use(req: Request, res: Response, next: NextFunction) {
        res.locals.privateKey = this.walletsService.randAddressPrivateKeyPair().privateKey;
        next();
    }
}