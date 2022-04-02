using TG.API.Model.Request;
using TG.Common.Models.Request;
using TG.Core.Exceptions;
using TG.Core.Security;
using TG.Domain.Repository;
using TG.Domain.UnitOfWork;
using TG.Entities;
using TG.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<User> Register(RegisterRequestModel model)
        {
            if (model.Password != model.RePassword)
                throw new ValidationException("Passwords does not match.");
            if ((await unitOfWork.userRepository.AnyAsync(x => x.Email == model.Email)))
                throw new ValidationException("This e-mail is taken.");
            if ((await unitOfWork.userRepository.AnyAsync(x => x.PhoneNumber == model.PhoneNumber)))
                throw new ValidationException("This phone number is taken.");

            var user = new User 
            {
                Title = $"{model.FirstName} {model.LastName}",
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = new Cryptography().Encrypt(model.Password),
                Role = Core.Enums.Enums.Authority.User,
                UpdatedOn = DateTimeOffset.UtcNow
            };

            await unitOfWork.userRepository.CreateAsync(user);
            await unitOfWork.CommitAsync();

            return user;
        }

        public async Task<User> SignInWithEmail(SignInWithEmailRequestModel model)
        {
            var user = await unitOfWork.userRepository.GetAllAsQueryable().Where(x => x.Email == model.Email && x.Password == new Cryptography().Encrypt(model.Password)).FirstOrDefaultAsync();

            if (user == null)
                throw new ValidationException("You entered invalid e-mail or password.");

            return user;
        }
    }
}
