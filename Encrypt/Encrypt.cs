using System.Security.Cryptography;
using System.Text;

namespace Encryptacion
{
    public class Encrypt
    {
        //hash es la palabra clave para encriptar
        public static string hashMD5 = "capital";
        //hash para MD5Zbeb
        public static string hashMD5ZBeb = "capital";
        //distintas llaves que servirán para separar el mensaje real con el de relleno para MD5Zbeb
        //tener en cuenta que estos string se agregarán 
        //y en la base de datos debe tener espacio suficiente para correcto funcionamiento
        private static string [] keysMD5Zbeb =
        {
            "<???_", "1==1", "3aaa-", "hhh_222", "rtr-200", "777ddd-"
        };
        //longitud mínima que deben tener los códigos generados en MD5Zbeb
        private static int minCharacterLengthMD5Zbeb = 50;
        
        /// <summary>
        /// Encriptamiento fuerte OJO este no se puede desencriptar
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i=0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        /// <summary>
        /// Encriptamiento con MD5, se debe configurar una palabra o frase hash
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GetEncryptMD5(string message)
        {
            string hash = hashMD5;
            byte[] data = UTF8Encoding.UTF8.GetBytes(message);

            MD5 md5 = MD5.Create();
            TripleDES tripledes = TripleDES.Create();

            tripledes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripledes.Mode = CipherMode.ECB;

            ICryptoTransform transorm = tripledes.CreateEncryptor();
            byte[] result = transorm.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(result);
        }
        public static string GetDecryptMD5(string encrypt)
        {
            string hash = hashMD5;
            byte[] data = Convert.FromBase64String(encrypt);

            MD5 md5 = MD5.Create();
            TripleDES tripledes = TripleDES.Create();

            tripledes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripledes.Mode = CipherMode.ECB;

            ICryptoTransform transorm = tripledes.CreateDecryptor();
            byte[] result = transorm.TransformFinalBlock(data, 0, data.Length);
            return UTF8Encoding.UTF8.GetString(result);
        }

        /// <summary>
        /// Encriptamiento mejorado con MD5 + un código personal.
        /// Este agregará código de relleno para lograr una longitud de valores mínima especifica.
        /// La longitud de valores se debe configurar en characterLengthMD5Zbeb.
        /// </summary>
        /// <param name="mensaje">string a encriptar</param>
        /// <returns>string encriptado</returns>
        public static string GetEncryptMD5ZBeb(string mensaje)
        {
            string hash = hashMD5ZBeb;
            
            string [] keysMD5 = keysMD5Zbeb;
            
            byte[] data = UTF8Encoding.UTF8.GetBytes(mensaje);

            MD5 md5 = MD5.Create();
            TripleDES tripledes = TripleDES.Create();

            tripledes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripledes.Mode = CipherMode.ECB;

            ICryptoTransform transorm = tripledes.CreateEncryptor();
            byte[] result = transorm.TransformFinalBlock(data, 0, data.Length);
            string resultConverted = Convert.ToBase64String(result);

            if(resultConverted.Length < minCharacterLengthMD5Zbeb)
            {
                Random r = new Random ();
                
                int indexKeysMD5 = r.Next(0,keysMD5.Count());
                resultConverted += keysMD5[indexKeysMD5];
                string letterRamdom;
                do
                {                    
                    letterRamdom = GetRandomChar();
                    resultConverted += letterRamdom;
                } while (resultConverted.Length < 30);
            }
            return resultConverted;
        }
        
        /// <summary>
        /// Para desencriptar MD5Zbeb || OJO se debe utilizar el mismo hash conque fue encriptado
        /// </summary>
        /// <param name="encrypt">string a traducir</param>
        /// <returns></returns>
        public static string GetDecryptMD5Zbeb(string encrypt)
        {
            try
            {
                string hash = hashMD5ZBeb;
                string [] keysMD5 = keysMD5Zbeb;
                int indexKeysMD5 = 0;
                string keyMD5;
                string trueEncrypt = "";
                string[] falseEncrypt;
                do
                {
                    //verifica cada una de las keys en orden
                    keyMD5 = keysMD5[indexKeysMD5];
                    if(encrypt.Contains(keyMD5))
                    {
                        //si encuentra la key en encrypt, entonces partir en 2
                        falseEncrypt = encrypt.Split(keyMD5);
                        //el primer dato es el mensaje real, el otro es relleno
                        trueEncrypt = falseEncrypt[0];
                    }
                    //si no encuentra esa key en el codigo entonces buscar el siguiente
                    indexKeysMD5++;
                } 
                while (indexKeysMD5 < keysMD5Zbeb.Length);
                
                byte[] data = trueEncrypt != "" ? Convert.FromBase64String(trueEncrypt) : Convert.FromBase64String(encrypt);

                MD5 md5 = MD5.Create();
                TripleDES tripledes = TripleDES.Create();

                tripledes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                tripledes.Mode = CipherMode.ECB;

                ICryptoTransform transorm = tripledes.CreateDecryptor();
                byte[] result = transorm.TransformFinalBlock(data, 0, data.Length);
                return UTF8Encoding.UTF8.GetString(result);
            }
            catch (System.Exception)
            {               
                return "error";
            }
            
        }
        public static int GetNumRealEncrypt(string encrypt)
        {           
            string hash = hashMD5ZBeb;
            string [] keysMD5 = keysMD5Zbeb;
            int indexKeysMD5 = 0;
            string keyMD5;
            int numEncryptChat = 0;
            string[] falseEncrypt;
            do
            {
                //verifica cada una de las keys en orden
                keyMD5 = keysMD5[indexKeysMD5];
                if(encrypt.Contains(keyMD5))
                {
                    //si encuentra la key en encrypt, entonces partir en 2
                    falseEncrypt = encrypt.Split(keyMD5);
                    //el primer dato es el mensaje real, el otro es relleno
                    numEncryptChat = falseEncrypt[0].Length;
                }
                //si no encuentra esa key en el codigo entonces buscar el siguiente
                indexKeysMD5++;
            }
            while (numEncryptChat <= 0 && indexKeysMD5 < keysMD5Zbeb.Length);
            return numEncryptChat;
        }        

        /// <summary>
        /// Genera un caracter alfanumérico tipo string de forma aleatoria.
        /// </summary>
        /// <returns></returns>
        public static string GetRandomChar()
        {
            char result;
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random r = new Random();
            result = chars[r.Next(chars.Length)];

            return result.ToString();
        }

        public static string GetRandomString(int length)
        {
            string result = "";

            for(int i = 0; i < length; i++)
            {
                result+= GetRandomChar();
            }
            return result;
        }
    }
}