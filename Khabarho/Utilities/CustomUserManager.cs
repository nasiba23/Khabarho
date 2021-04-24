using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Khabarho.Db;
using Khabarho.Extensions;
using Khabarho.Models;
using Khabarho.Models.AccountModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Khabarho.Utilities
{
    public class CustomUserManager<TUSer> : UserManager<User> where TUSer : User
    {
        private DataContext _context;

        public CustomUserManager(DataContext context, IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _context = context;
        }

        public override async Task<IdentityResult> DeleteAsync(User user)
        {
            user.CustomNullCheck(ErrorMessages.NullParameterError);

            user.IsDeleted = true;
            user.DeletedDate = DateTime.Now;
            user.UserName = "Удалено";
            _context.Update(user);
            var result = await _context.SaveChangesAsync();

            return result > 0 ? IdentityResult.Success : IdentityResult.Failed();
        }
    }
}