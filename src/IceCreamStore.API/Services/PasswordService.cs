using System.Security.Cryptography;
using System.Text;

namespace IceCreamStore.API.Services
{
    /*
        *  Проверка на null или пустую строку: Сначала происходит проверка на null 
           или пустую строку для plainPassword. Если plainPassword равен null 
           или пустой строке, будет сгенерировано исключение ArgumentNullException.

        *  Генерация соли: В следующей строке кода используется RandomNumberGenerator.GetBytes(10), 
           чтобы сгенерировать массив байт длиной 10. Затем эти случайные байты конвертируются 
           в строку Base64 и используются как соль для хеширования пароля.

        *  Создание хеша: Пароль вместе с солью конвертируется в массив байтов bytes 
           с кодировкой UTF-8. Этот массив байтов хешируется с использованием SHA256, 
           результат сохраняется в массив байтов hash.

        *  Преобразование хеша в строку: Затем полученный хеш представляется в виде 
           строки Base64 и сохраняется в переменную hashedPassword.

        * Возврат результатов: Наконец, метод возвращает кортеж (salt, hashedPassword), содержащий 
          сгенерированную соль и хешированный пароль.

        Этот код выполняет процесс генерации безопасного хеша для пароля, добавляя к нему 
        соль для повышения безопасности и устойчивости к атакам типа "радужной таблицы" (rainbow table). 
    */


    public class PasswordService
    {
        private const int _saltSize = 10;
        public (string salt, string hashedPassword) GenerateSaltAndHash(string plainPassword)
        {
            if(string.IsNullOrWhiteSpace(plainPassword))
                throw new ArgumentNullException(nameof(plainPassword));

            var buffer = RandomNumberGenerator.GetBytes(_saltSize);
            var salt = Convert.ToBase64String(buffer);

            var hashedPassword = GenerateHashedPassword(plainPassword, salt);

            return (salt, hashedPassword);
        }

        public bool AreEqual (string plainPassword, string salt, string hashedPassword)
        {
            var newHashedPassword = GenerateHashedPassword(plainPassword, salt);
            return newHashedPassword == hashedPassword;
        }

        private static string GenerateHashedPassword(string plainPassword, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainPassword + salt);
            var hash = SHA256.HashData(bytes);

            var hashedPassword = Convert.ToBase64String(hash);

            return hashedPassword;
        }
    }
}
