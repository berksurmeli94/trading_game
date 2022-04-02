using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using TG.Common.Models.Request;
using TG.Core.Response;
using TG.Entities;
using TG.Services.Interface;
using TG.API.Model.Request;

namespace TG.API.Controllers
{
    public class LoginController : BaseController<LoginController>
    {
        private readonly IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        public async ValueTask<ActionResult<BaseAPIResponse<string>>> Register([FromQuery] RegisterRequestModel model)
        {
            var response = new BaseAPIResponse<string>();

            var user = await userService.Register(model);
            response.Data = WriteToken(user);
            response.Message = "Your account created successfully, welcome!";
            return Ok(response);
        }

        public async ValueTask<ActionResult<BaseAPIResponse<string>>> SignInWithEmail([FromQuery] SignInWithEmailRequestModel model)
        {
            var response = new BaseAPIResponse<string>();
            var user = await userService.SignInWithEmail(model);
            response.Data = WriteToken(user);
            return Ok(response);
        }

        private string WriteToken(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("userID", user.ID));
            claims.Add(new Claim("firstName", user.FirstName));
            claims.Add(new Claim("lastName", (!string.IsNullOrWhiteSpace(user.LastName) ? user.LastName : "-")));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));

            var token = new JwtSecurityToken(
                issuer: "i",
                audience: "a",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                notBefore: DateTime.UtcNow,
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("supersecretkey")), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
