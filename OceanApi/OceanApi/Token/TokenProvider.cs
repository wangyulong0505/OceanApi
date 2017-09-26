using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OceanApi.Token
{
    public class TokenProvider
    {
        readonly TokenProviderOptions options;
        public TokenProvider(TokenProviderOptions _options)
        {
            options = _options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="strUserName"></param>
        /// <param name="strPassword"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<TokenEntity> GenerateToken(HttpContext context, string strUserName, string strPassword, string role)
        {
            ClaimsIdentity identity = await GetIdentity(strUserName);
            if (identity == null)
            {
                return null;
            }
            var now = DateTime.Now;
            //声明
            Claim[] claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, strUserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.Name, strUserName)
                };
            //Jwt安全令牌
            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(options.Expiration),
                signingCredentials: options.Credentials);
            //生成令牌字符串
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            TokenEntity response = new TokenEntity
            {
                AccessToken = encodedJwt,
                Expired = (int)options.Expiration.TotalSeconds
            };

            return response;
        }

        /// <summary>
        /// 通过用户名异步获取Identity，返回值为ClaimIdentity
        /// </summary>
        /// <param name="strUserName"></param>
        /// <returns></returns>
        private Task<ClaimsIdentity> GetIdentity(string strUserName)
        {
            return Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(strUserName, "token"),
                new Claim[] {
                    new Claim(ClaimTypes.Name, strUserName)
                }));
        }

        /// <summary>
        /// 返回距离1970-01-01 00：00：00之间相隔的总秒数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }
    }
}
