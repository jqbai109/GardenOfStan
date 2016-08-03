using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PromolinkUS.Common
{
    public class SecEncrypt
    {
        private string privateKey = "<RSAKeyValue><Modulus>5y3H0kU1IcFXQtuTi7+KiL1K3MjBI4PM+3rg9OtGBtTlDpKlsxBfGitZ2BLQHBAOzTQT4/zUSpbPTOFgnCDzbH+pq1E75NzWlY/NzPxNYaMkqqRi31dMGy3xHNBe+ztaAvFkxsKJSYEnoWK/bsQWJaomhi4W8likjfbsaPf9PeU=</Modulus><Exponent>AQAB</Exponent><P>/QZgGIrH+/FQBUZtag7IYlT1mrdpjZ1gmJ/MmoUz9/U1UdhzNXYWY1SbRJtTEUPBIltED7Ehq/T5kYgpHRp3ZQ==</P><Q>6eWllJcTDvAKx6rnA4FCnDxpEd08NIMV9eY5jumRhSAn+ihvK8dG6cKU0O45ShOxWTE18Hafqha4ruHK38uEgQ==</Q><DP>TxiaG7HumES0ViHQ/FGItKyIZogld/6VfsCdjkRLnZzKwT4frbuvffep3gWMLx52Fo5fNWSS+RVImwTsjUuDvQ==</DP><DQ>CnMoIqtdPp2a3bDVAWMVBsXsK9AmkHIK2SGgELJ+vePh5VtJHDua/3aiD3OVO6oFkmaQcl3aZE7/kVClL7p4AQ==</DQ><InverseQ>mKHMOYLieAetUCTiyiQZqAR6ZvEb7QUpPuXYbuDbUxEH6d0+H42WPTsvUf8azYB4aiFMEY4qxI9uLBjvgRjjKA==</InverseQ><D>ohuUN3qYW2c8TYGz4Rcbie9TA6cy6DiQEPiFrrkcjcXUKXfkAcDFL5Cem8n1bPhFqNeP6xtrsjI2g263VEQ27NTWh8SfDavRDrd96nwNgSg+sdK/JdnYlMPFUEkXwFf/wzhK5+biWmvX69t8oZtZvkQ7ebbsN63eipNcv7MjCAE=</D></RSAKeyValue>";
        public string publicKey = "<RSAKeyValue><Modulus>5y3H0kU1IcFXQtuTi7+KiL1K3MjBI4PM+3rg9OtGBtTlDpKlsxBfGitZ2BLQHBAOzTQT4/zUSpbPTOFgnCDzbH+pq1E75NzWlY/NzPxNYaMkqqRi31dMGy3xHNBe+ztaAvFkxsKJSYEnoWK/bsQWJaomhi4W8likjfbsaPf9PeU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public string EncryptString(string sSource)
        {
            return EncryptString(sSource, publicKey);
        }

        /// <summary> 
        /// RSA Encrypt 
        /// </summary> 
        /// <param name="sSource" >Source string</param> 
        /// <param name="sPublicKey" >public key</param> 
        /// <returns></returns> 
        public string EncryptString(string sSource, string sPublicKey)
        {
            if (sSource == null)
            {
                sSource = "";
            }
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string plaintext = sSource;
            rsa.FromXmlString(sPublicKey);
            byte[] cipherbytes;
            byte[] byteEn = rsa.Encrypt(Encoding.UTF8.GetBytes("1qaz2wsx3edc4rfv5tgb6yhn7ujm8ik,9ol.0p;/-[']=~!@#$%^&*()_+"), false);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(plaintext), false);
            return Convert.ToBase64String(cipherbytes);
        }

        public string DecryptString(String sSource)
        {
            return DecryptString(sSource, privateKey);
        }

        /// <summary> 
        /// RSA Decrypt 
        /// </summary> 
        /// <param name="sSource">Source string</param> 
        /// <param name="sPrivateKey">Private Key</param> 
        /// <returns></returns> 
        public string DecryptString(String sSource, string sPrivateKey)
        {
            if (String.IsNullOrEmpty(sSource) == false)
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(sPrivateKey);
                byte[] byteEn = rsa.Encrypt(Encoding.UTF8.GetBytes("1qaz2wsx3edc4rfv5tgb6yhn7ujm8ik,9ol.0p;/-[']=~!@#$%^&*()_+"), false);
                byte[] plaintbytes = rsa.Decrypt(Convert.FromBase64String(sSource), false);
                return Encoding.UTF8.GetString(plaintbytes);
            }
            return "";
        }
    }
}
