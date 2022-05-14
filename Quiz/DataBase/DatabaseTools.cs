using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Newtonsoft.Json;
using Quiz.MVVM.Model;

namespace Quiz.DataBase
{
    public class DatabaseTools
    {
        private const string _key = "u7x!A%D*G-JaNdRg";
        private static readonly string _questionsPath = @"..\..\..\DataBase\questions.json";
        public List<Question> Questions { get; set; } = LoadQuestions();
        private static List<Question> LoadQuestions()
        {
            using var reader = new StreamReader(_questionsPath);
            var json = reader.ReadToEnd();
            var decrypted = DecryptString(json);
            var questions = JsonConvert.DeserializeObject<List<Question>>(decrypted);

            return questions;
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(Questions);
            var encrypted = EncryptString(json);
            File.WriteAllText(_questionsPath, encrypted);
        }
        public static string EncryptString( string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString( string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
