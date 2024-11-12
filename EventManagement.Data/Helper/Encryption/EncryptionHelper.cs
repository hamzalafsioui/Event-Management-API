using System.Security.Cryptography;

namespace EventManagement.Data.Helper.Encryption
{
	public static class EncryptionHelper
	{

		public static string HashCode(string code)
		{

			// Generate a random salt
			byte[] salt = new byte[16];
			RandomNumberGenerator.Fill(salt);  // Replaces RNGCryptoServiceProvider

			// Derive a key using PBKDF2 (with SHA-256 as the hash function)
			using (var pbkdf2 = new Rfc2898DeriveBytes(code, salt, 10000, HashAlgorithmName.SHA256))
			{
				byte[] hash = pbkdf2.GetBytes(32); // 256-bit hash

				byte[] hashBytes = new byte[48];   // 16 bytes for salt + 32 bytes for hash
				Array.Copy(salt, 0, hashBytes, 0, 16);
				Array.Copy(hash, 0, hashBytes, 16, 32);

				// Convert hash + salt to Base64
				return Convert.ToBase64String(hashBytes);



				//using (Aes aesAlg = Aes.Create())
				//{
				//	// Set the key and IV for AES encryption
				//	aesAlg.Key = Encoding.UTF8.GetBytes(key);
				//	aesAlg.IV = new byte[aesAlg.BlockSize / 8];


				//	// Create an encryptor
				//	ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);


				//	// Encrypt the data
				//	using (var msEncrypt = new System.IO.MemoryStream())
				//	{
				//		using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
				//		using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
				//		{
				//			swEncrypt.Write(plainText);
				//		}


				//		// Return the encrypted data as a Base64-encoded string
				//		return Convert.ToBase64String(msEncrypt.ToArray());
				//	}
				//}
			}
		}
		public static bool VerifyCode(string enteredCode, string storedHash)
		{
			// Decode the Base64 encoded hash
			byte[] hashBytes = Convert.FromBase64String(storedHash);

			// Extract the salt (first 16 bytes)
			byte[] salt = new byte[16];
			Array.Copy(hashBytes, 0, salt, 0, 16);

			// Derive the key for the entered code
			using (var pbkdf2 = new Rfc2898DeriveBytes(enteredCode, salt, 10000, HashAlgorithmName.SHA256))
			{
				byte[] hash = pbkdf2.GetBytes(32);

				// Compare the derived hash with the stored hash
				for (int i = 0; i < 32; i++)
				{
					if (hashBytes[i + 16] != hash[i])
						return false; // Passwords don't match
				}
			}

			return true; // code match


			//using (Aes aesAlg = Aes.Create())
			//{
			//	// Set the key and IV for AES decryption
			//	aesAlg.Key = Encoding.UTF8.GetBytes(key);
			//	aesAlg.IV = new byte[aesAlg.BlockSize / 8];


			//	// Create a decryptor
			//	ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);


			//	// Decrypt the data
			//	using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
			//	using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
			//	using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
			//	{
			//		// Read the decrypted data from the StreamReader
			//		return srDecrypt.ReadToEnd();
			//	}
			//}
		}
	}
}
