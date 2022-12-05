﻿using Api.Models.Token;
using Api.Models.User;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
    
        private readonly AuthService _authService;
        private readonly UserService _userService;

        public AuthController(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        // Получение токенов при аутентификации
        [HttpPost]
        public async Task<TokenModel> Token(TokenRequestModel model)
            => await _authService.GetToken(model.Login, model.Pass);


        // Обновлние токеннов 
        [HttpPost]
        public async Task<TokenModel> RefreshToken(RefreshTokenRequestModel model)
            => await _authService.GetTokenByRefreshToken(model.RefreshToken);

        // Обновленный CreateUser
        //Пост запрос на отправку данных и сохранение их в БД
        [HttpPost]
        public async Task RegisterUser(CreateUserModel model)
        {
            // добавить проверку юзернейма !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (await _userService.CheckUserExist(model.Email))
                throw new Exception("user is exist");

            await _userService.CreateUser(model);

        }

    }
}
