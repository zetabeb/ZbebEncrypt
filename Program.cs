int opcion = 0;

do
{
    Console.WriteLine("");
    Console.WriteLine("Hello, World!");
    Console.WriteLine("");
    Console.WriteLine("¿Qué desea hacer?");
    Console.WriteLine("1) Encriptar en MD5");
    Console.WriteLine("2) Desencriptar MD5");
    Console.WriteLine("3) Encriptar en MD5Zbeb");
    Console.WriteLine("4) Desencriptar en MD5Zbeb");
    Console.WriteLine("5) Encriptar en SHA256 (No se puede desencriptar)");
    Console.WriteLine("6) Generar una cadena aleatoria");
    Console.WriteLine("7) Salir");
    Console.WriteLine("");
    Console.WriteLine("");    
    opcion = int.Parse(Console.ReadLine());
    Console.WriteLine("");
    switch (opcion)
    {
        case 1:
            Console.WriteLine("Agrega una key necesaria para desencriptar (NO OLVIDARLA)");
            string key0 = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("confirmar key");
            string key1 = Console.ReadLine();

            if(!string.IsNullOrEmpty(key0) && key0 == key1)
            {
                Console.WriteLine("");  
                Console.WriteLine("Ingrese la cadena a encriptar");
                string cadena = Console.ReadLine();
                Encryptacion.Encrypt.hashMD5 = key0;
                Console.WriteLine("");
                Console.WriteLine("El resultado es:");
                try
                {
                    Console.WriteLine(Encryptacion.Encrypt.GetEncryptMD5(cadena));    
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Ocurrió un error");
                }                
            }
            else
            {
                Console.WriteLine("No coinciden los key");
            }
            break;
        case 2:
            Console.WriteLine("Ingrese la key para desencriptar");
            Encryptacion.Encrypt.hashMD5 = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Ingrese la cadena a desencriptar");  
            string cadenaMD5 = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("El resultado es:");  
            try
            {
                Console.WriteLine(Encryptacion.Encrypt.GetDecryptMD5(cadenaMD5));
            }
            catch (System.Exception)
            {
                
                Console.WriteLine("Ocurrió un error");
            }
            break;
        case 3:
            Console.WriteLine("Agrega una key necesaria para desencriptar (No olvidarla)");
            string key2 = Console.ReadLine();
            Console.WriteLine("");  
            Console.WriteLine("confirmar key");
            string key3 = Console.ReadLine();

            if(!string.IsNullOrEmpty(key2) && key2 == key3)
            {
                Console.WriteLine("");  
                Console.WriteLine("Ingrese la cadena a encriptar");
                string cadena = Console.ReadLine();
                Encryptacion.Encrypt.hashMD5ZBeb = key2;
                Console.WriteLine("");
                Console.WriteLine("El resultado es:");
                try
                {
                    Console.WriteLine(Encryptacion.Encrypt.GetEncryptMD5ZBeb(cadena));
                }
                catch (System.Exception)
                {
                    Console.WriteLine("Ocurrió un error");
                }
            }
            else
            {
                Console.WriteLine("No coinciden los key");                
            }
            break;
        case 4:
            Console.WriteLine("Ingrese la key para desencriptar");  
            Encryptacion.Encrypt.hashMD5ZBeb = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Ingrese la cadena a desencriptar");  
            string cadenaMD5Zbeb = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("El resultado es:");
            try
            {
                Console.WriteLine(Encryptacion.Encrypt.GetDecryptMD5Zbeb(cadenaMD5Zbeb));
            }
            catch (System.Exception)
            {
                Console.WriteLine("Ocurrió un error");
            }
            break;
        case 5:
            Console.WriteLine("Ingrese la cadena a encriptar");  
            string cadenaSHA256 = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("El resultado es:");  
            try
            {
                Console.WriteLine(Encryptacion.Encrypt.GetSHA256(cadenaSHA256));
            }
            catch (System.Exception)
            {
                
                Console.WriteLine("Ocurrió un error");
            }
            break;
        case 6:
            Console.WriteLine("Ingrese la longitud de la cadena que desea generar");
            int length = int.Parse(Console.ReadLine());
            Console.WriteLine();
            if(length != null && length > 0)
            {
                Console.WriteLine("Cadena aleatoria:");
                Console.WriteLine(Encryptacion.Encrypt.GetRandomString(length));
            }
            else
            {
                Console.WriteLine("Ocurrió un error");
            }
            break;
        case 7:
            Console.WriteLine("");
            Console.WriteLine("Adiós");
            break;
        default:
            Console.WriteLine("No ha seleccionado una opción correcta");
            break;
    }    
    Console.WriteLine("");
} 
while (opcion != 7);





