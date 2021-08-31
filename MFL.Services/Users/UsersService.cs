﻿using MFL.Services.Clients;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using MFL.Services.Users.Models;
using System.Security.Claims;
using MFL.Common.Config;

namespace MFL.Services.Users
{
    public class UsersService
    {
        private readonly IMFLHttpClient _client;
        private readonly IConfiguration _config;
        private readonly CookieContainer _cookieContainer;
        public UsersService(IMFLHttpClient client, IConfiguration config, CookieContainer cookieContainer)
        {
            _client = client;
            _config = config;
            _cookieContainer = cookieContainer;
        }

        public async Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequest user)
        {
            var response = await _client.Client.GetAsync($"/2020/login?USERNAME={user.Username}&PASSWORD={user.Password}&XML=1");
            response.EnsureSuccessStatusCode();
            var element = XElement.Parse(await response.Content.ReadAsStringAsync());

            var authResponse = new AuthenticationResponse(user, element?.Value);

            if (!authResponse.IsAuthenticated) return authResponse;

            var userIdAttribute = element.FirstAttribute;
            authResponse.Token = GenerateJwtToken(user, userIdAttribute.Value);
            return authResponse;
        }

        public bool ValidateCurrentToken(string token)
        {
            var _appSettings = _config.GetSection(AppSettingsOptions.AppSettings) as AppSettingsOptions;
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {
                return false;
            }

            GetClaim(token, "mflId");
            _cookieContainer.Add(_client.Client.BaseAddress, new Cookie("MFL_USER_ID", GetClaim(token, "mflId")));

            return true;
        }

        private string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }

        private string GenerateJwtToken(AuthenticationRequest user, string mflId)
        {
            var _appSettings = _config.GetSection(AppSettingsOptions.AppSettings) as AppSettingsOptions;
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("mflId", mflId)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
