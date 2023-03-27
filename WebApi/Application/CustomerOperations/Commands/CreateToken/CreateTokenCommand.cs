using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.TokenOperations.Models;
using WebApi.TokenOperations;

namespace WebApi.Application.CustomerOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public LoginModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IMovieStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public Token Handle()
        {
            Customer customer = _dbContext.Customers.FirstOrDefault(customer => customer.Email == Model.Email && customer.Password ==Model.Password);

            if (customer is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(customer);
                customer.RefreshToken = token.RefreshToken;
                customer.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(5);

                _dbContext.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Kulanıcı adı veya şifre yanlış.");
            }
        }

    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
