using DBH.Core.Setting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataBaseHelper.Common
{
    public class JWTHelper
    {
        //private readonly IConfiguration _configuration;
        private readonly JWTOptions _jwtOptions;
        public JWTHelper(JWTOptions jWTOptions)
        {
            _jwtOptions = jWTOptions;
        }
        public string CreateToken()
        {
            //1.定义要用到的claim，不宜过多，会导致生成的字符串太长
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name,"xiaobo"),
                new Claim(ClaimTypes.Role,"admin"),
                new Claim(JwtRegisteredClaimNames.Jti,"admin"),
                new Claim("UserName","admin"),
                new Claim("Name","管理员")
            };

            //2.读取配置的加密key
            SymmetricSecurityKey ssk = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));

            //3.加密算法
            string algorithm = SecurityAlgorithms.HmacSha256;

            //4.生成Credentials
            SigningCredentials signCredential = new SigningCredentials(ssk, algorithm);

            //5.最后，生成Token
            JwtSecurityToken jwtToken = new JwtSecurityToken(_jwtOptions.Issuer.ToString(), _jwtOptions.Audience.ToString(), claims, DateTime.Now, DateTime.Now.AddSeconds(_jwtOptions.ExprieSeconds), signCredential);

            //6.将token生成字符串返回
            string token =new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }
    }
}
