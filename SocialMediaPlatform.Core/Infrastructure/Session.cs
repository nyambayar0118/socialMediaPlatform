using SocialMediaPlatform.Core.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMediaPlatform.Core.Infrastructure
{
    /// <summary>
    /// Нэвтэрсэн хэрэглэгчийн мэдээллийг хадгалах Session Singleton класс
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Классын зөвхөн цорын ганц объект
        /// </summary>
        private static Session? _instance;
        /// <summary>
        /// Классыг lock хийсэн эсэхийг илтгэх талбар
        /// </summary>
        private static readonly object _lock = new object();
        /// <summary>Нэвтэрсэн хэрэглэгчийн мэдээлэл</summary>
        private UserDTO? _currentUser;

        /// <summary>
        /// Классын хувийн байгуулагч. Гаднаас хандах боломжгүй ба зөвхөн GetInstance() методаар дамжуулан цорын ганц объект үүсгэх боломжтой
        /// </summary>
        private Session() { }

        /// <summary>
        /// Классын цорын ганц объектыг буцаах статик метод. Хэрэв объект аль хэдийн үүссэн бол тухайн объектыг буцаана. Хэрэв объект үүсээгүй бол шинэ объект үүсгэж буцаана. Энэ процесс thread-safe байдлаар хийгдэнэ
        /// </summary>
        /// <returns>Классын цорын ганц объект</returns>
        public static Session GetInstance()
        {
            // Instance байгаа эсэхийг шалгана
            if (_instance == null)
            {
                // Thread-үүд давхцахаас сэргийлж түгжинэ
                lock (_lock)
                {
                    // Instance байгаа эсэхийг шалгана
                    if (_instance == null)
                    {
                        // Байхгүй бол instance үүсгэнэ
                        _instance = new Session();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// Нэвтэрсэн хэрэглэгчийн мэдээллийг авах метод
        /// </summary>
        /// <returns>Нэвтэрсэн хэрэглэгчийн DTO</returns>
        public UserDTO? GetCurrentUser() => _currentUser;
        /// <summary>
        /// Хэрэглэгч нэвтэрсэн эсэхийг шалгах метод
        /// </summary>
        /// <returns>Нэвтэрсэн бол true, үгүй бол false</returns>
        public bool IsLoggedIn() => _currentUser != null;
        /// <summary>
        /// Хэрэглэгч нэвтрэх үед session-д мэдээллийг хадгалах метод
        /// </summary>
        /// <param name="user">Нэвтэрсэн хэрэглэгчийн DTO</param>
        public void Login(UserDTO user) => _currentUser = user;
        /// <summary>
        /// Хэрэглэгч гарах үед хэрэглэгчийн түр хадгалсан мэдээллийг устгах метод
        /// </summary>
        public void Logout() => _currentUser = null;
    }
}
