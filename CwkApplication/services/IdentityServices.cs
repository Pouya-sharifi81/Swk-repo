﻿using CwkApplication.Option;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.services
{
    public class IdentityService
    {
        private readonly JwtSetting _jwtSettings;
        private readonly byte[] _key;
        private IConfiguration _configuration;

        public IdentityService(IOptions< JwtSetting> jwtOptions , IConfiguration configuration)
        {
            _jwtSettings = jwtOptions.Value; 
            _key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);
            _configuration = configuration;
        }

        public JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

        public SecurityToken CreateSecurityToken(ClaimsIdentity identity)
        {
            var tokenDescriptor = GetTokenDescriptor(identity);

            return TokenHandler.CreateToken(tokenDescriptor);
        }

        public string WriteToken(SecurityToken token)
        {
            return TokenHandler.WriteToken(token);
        }

            
        private SecurityTokenDescriptor GetTokenDescriptor(ClaimsIdentity identity)
        {
            return new SecurityTokenDescriptor()
            {
                
                Subject = identity,
                Expires = DateTime.Now.AddHours(2),
                Audience = _jwtSettings.Audiences[0],
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}
