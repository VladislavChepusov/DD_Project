﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Configs
{
    /// <summary>
    /// Класс описывающий конфиг авторизации 
    /// </summary>
    public class AuthConfig
    {
        public const string Position = "auth"; // выбор свойство из appsetings.json
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public int LifeTime { get; set; }
        public SymmetricSecurityKey SymmetricSecurityKey() // метод отдающий симетричный 
            => new(Encoding.UTF8.GetBytes(Key));           // ключ безопастности
    }
}
