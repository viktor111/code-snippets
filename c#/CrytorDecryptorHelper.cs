public static class CryptoHelper
    {
        private static readonly int BlockBitSize = 128;

        private static readonly int KeyBitSize = 256;

        private static readonly int SaltBitSize = 128;

        private static readonly int Iterations = 10000;

        public static string Encrypt(string plainText, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException("plainText");
            }

            using (var keyDerivationFunction = new Rfc2898DeriveBytes(key, SaltBitSize / 8, Iterations))
            {
                using (var aesManaged = Aes.Create())
                {
                    aesManaged.KeySize = KeyBitSize;
                    aesManaged.BlockSize = BlockBitSize;

                    aesManaged.GenerateIV();

                    byte[] saltBytes = keyDerivationFunction.Salt;
                    byte[] keyBytes = keyDerivationFunction.GetBytes(KeyBitSize / 8);
                    byte[] ivBytes = aesManaged.IV;

                    using (var encryptor = aesManaged.CreateEncryptor(keyBytes, ivBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                using (var streamWriter = new StreamWriter(cryptoStream))
                                {
                                    streamWriter.Write(plainText);
                                }
                            }

                            byte[] cipherTextBytes = memoryStream.ToArray();

                            Array.Resize(ref saltBytes, saltBytes.Length + ivBytes.Length);
                            Array.Copy(ivBytes, 0, saltBytes, SaltBitSize / 8, ivBytes.Length);

                            Array.Resize(ref saltBytes, saltBytes.Length + cipherTextBytes.Length);
                            Array.Copy(cipherTextBytes, 0, saltBytes, SaltBitSize / 8 + ivBytes.Length, cipherTextBytes.Length);

                            return Convert.ToBase64String(saltBytes);
                        }
                    }
                }
            }
        }

        public static string Decrypt(string ciphertext, string key)
        {
            if (string.IsNullOrEmpty(ciphertext))
            {
                throw new ArgumentNullException("cipherText");
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            byte[] saltBytes = new byte[SaltBitSize / 8];
            byte[] ivBytes = new byte[BlockBitSize / 8];

            byte[] allTheBytes = Convert.FromBase64String(ciphertext);

            Array.Copy(allTheBytes, 0, saltBytes, 0, saltBytes.Length);
            Array.Copy(allTheBytes, saltBytes.Length, ivBytes, 0, ivBytes.Length);

            byte[] ciphertextBytes = new byte[allTheBytes.Length - saltBytes.Length - ivBytes.Length];
            Array.Copy(allTheBytes, saltBytes.Length + ivBytes.Length, ciphertextBytes, 0, ciphertextBytes.Length);

            using (var keyDerivationFunction = new Rfc2898DeriveBytes(key, saltBytes, Iterations))
            {
                byte[] keyBytes = keyDerivationFunction.GetBytes(KeyBitSize / 8);

                using (var aesManaged = Aes.Create())
                {
                    aesManaged.KeySize = KeyBitSize;
                    aesManaged.BlockSize = BlockBitSize;

                    using (var decryptor = aesManaged.CreateDecryptor(keyBytes, ivBytes))
                    {
                        using (var memoryStream = new MemoryStream(ciphertextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                using (var streamReader = new StreamReader(cryptoStream))
                                {
                                    return streamReader.ReadToEnd();
                                }
                            }
                        }
                    }
                }
            }
        }
    }