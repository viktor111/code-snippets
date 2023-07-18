internal static class Generator
{
    public static string ByteArrayToHexString(byte[] byteArray)
    {
        if (byteArray == null)
            throw new BoricaException($"{nameof(byteArray)} is null");

        return BitConverter.ToString(byteArray).Replace("-", "");
    }

    public static byte[] GenerateRandomByteArray(int minLength, int maxLength)
    {
        ValidateArrayLength(minLength, maxLength);

        var random = RandomNumberGenerator.Create();
        var arrayLength = GetRandomLength(random, minLength, maxLength);

        var array = new byte[arrayLength];
        random.GetBytes(array);

        return array;
    }

    public static int GenerateOrderNumber(string nonce, PortalType portalType)
    {
        var inputData = ConcatenateTimestampAndNonce(nonce);

        var hash = ComputeHash(inputData);

        var randomNumber = BitConverter.ToInt32(hash, 0) % 900000 + 100000;

        var str = Math.Abs(randomNumber);

        if (str.ToString().Length < 6)
        {
            return GenerateOrderNumber(nonce, portalType);
        }

        var okOrd = Math.Abs(randomNumber);

        return ChangeOrderNumberBasedOnPortalType(portalType, okOrd);
    }


    private static byte[] GetKey()
    {
        var secretKey = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(secretKey);

        return secretKey;
    }

    private static void ValidateArrayLength(int minLength, int maxLength)
    {
        if (minLength < 0)
            throw new BoricaException($"{nameof(minLength)} must be non-negative.");

        if (maxLength < minLength)
            throw new BoricaException($"{nameof(maxLength)} must be greater than or equal to minLength.");
    }

    private static int GetRandomLength(RandomNumberGenerator random, int minLength, int maxLength)
    {
        var length = new byte[4];
        random.GetBytes(length);
        var arrayLength = BitConverter.ToInt32(length, 0);
        arrayLength = Math.Abs(arrayLength % (maxLength - minLength + 1)) + minLength;

        return arrayLength;
    }

    private static byte[] ConcatenateTimestampAndNonce(string nonce)
    {
        var data = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
        var nonceBytes = Encoding.UTF8.GetBytes(nonce);
        var inputData = new byte[data.Length + nonceBytes.Length];

        Buffer.BlockCopy(data, 0, inputData, 0, data.Length);
        Buffer.BlockCopy(nonceBytes, 0, inputData, data.Length, nonceBytes.Length);

        return inputData;
    }

    private static byte[] ComputeHash(byte[] inputData)
    {
        using var hmac = new HMACSHA256(GetKey());
        return hmac.ComputeHash(inputData);
    }

    private static int ChangeOrderNumberBasedOnPortalType(PortalType portalType, int generatedOrder)
    {
        Random random = new Random();
        if (portalType == PortalType.HP)
        {
            var okOrdStr = generatedOrder.ToString();
            var hpRandom = random.Next(1, 5);
            var myNewString = okOrdStr.Substring(0, okOrdStr.Length - 1) + hpRandom;
            return int.Parse(myNewString);
        }
        else
        {
            var okOrdStr = generatedOrder.ToString();
            var sspRandom = random.Next(6, 9);
            var myNewString = okOrdStr.Substring(0, okOrdStr.Length - 1) + sspRandom;
            return int.Parse(myNewString);
        }
    }
}