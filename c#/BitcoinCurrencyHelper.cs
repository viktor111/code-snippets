public class BitcoinCurrency
    {
        public static long BitcoinToSatoshi(string amountBtc)
        {
            var decimalAmount = decimal.Parse(amountBtc);
            
            return BitcoinToSatoshi(decimalAmount);
        }

        public static long BitcoinToSatoshi(decimal amountBtc)
        {
            var divisor = new Decimal(Math.Pow(10, 8));

            var result = decimal.Multiply(amountBtc, divisor);

            return Convert.ToInt64(result);   
        }

        public static decimal SatoshiToBitcoin(string satoshiAmount) 
        {
            var divisor = new decimal(Math.Pow(10, 8));
            var decimalAmount = decimal.Parse(satoshiAmount, NumberStyles.Float);
            return decimal.Divide(decimalAmount, divisor);
        }

        public static decimal SatoshiToBitcoin(decimal satoshiAmount)
        {
            var divisor = new decimal(Math.Pow(10, 8));
            return decimal.Divide(satoshiAmount, divisor);
        }

        public static int CalculateFee(int inputs, int outputs)
        {
            var bytes = (inputs * 148) + (outputs * 34) + 10;
            
            var pricePerByte = 1;
            
            var transactionFee = bytes * pricePerByte;

            return transactionFee;
        }
    }