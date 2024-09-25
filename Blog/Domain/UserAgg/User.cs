using Blog.Domain.UserAgg.Services;
using Common.Domain;
using Common.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Blog.Domain.UserAgg
{
    public sealed class User : IdentityUser
    {
        private User()
        {
            
        }
        public User(string email, string userName, string phoneNumber, string password, IUserDomainService userService)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (userName == null) throw new ArgumentNullException(nameof(userName));
            if (phoneNumber == null) throw new ArgumentNullException(nameof(phoneNumber));
            Guard(phoneNumber, userName, userService);
            Password = password;
            Email = email;
            UserName = userName;
            //Family = family;
            PhoneNumber = phoneNumber;
            AvatarName = "Avatar.png"; 
 
            CreationDate = DateTime.Now;
        }
        public User(string email, string userName, string phoneNumber, string password)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (userName == null) throw new ArgumentNullException(nameof(userName));
          
            Password = password;
            Email = email;
            UserName = userName;
            PhoneNumber = phoneNumber;
        }

        #region comment

        //public User(string name, string family, string password, string phoneNumber, string userName, IUserDomainService userDomainService)
        //{
        //    Guard(phoneNumber,userName,userDomainService);
        //    Name = name;
        //    Family = family;
        //    AvatarName = "Avatar.png";
        //    Password = password;
        //    PhoneNumber = phoneNumber;
        //    UserName = userName;
        //    Roles = new List<UserRole>();
        //    Tokens = new List<UserToken>();

        //    CreationDate = DateTime.Now;
        //}
        #endregion


        public DateTime CreationDate { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string AvatarName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<UserRole> Roles { get; } = new List<UserRole>();
        public List<UserToken> Tokens { get; }= new List<UserToken>();
        public void SetRoles(List<UserRole> roles)
        {
            roles.ForEach(f => f.UserId = Id);
            Roles.Clear();
            Roles.AddRange(roles);
        }
        
        public void SetUserRoles(List<string> roles)
        {
            List<UserRole> userRoles = new List<UserRole>();
            foreach (var item in roles)
            {
                userRoles.Add( new UserRole(item));
            }
            userRoles.ForEach(f => f.UserId = Id);
            Roles.Clear();
            Roles.AddRange(userRoles);
        }

        public void Edit(string name, string family, string phoneNumber, string userName,
            IUserDomainService userDomainService)
        {
            Guard(phoneNumber, userName, userDomainService);
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            UserName = userName;
            
        }

        public void ChangePassword(string newPassword)
        {
            NullOrEmptyDomainDataException.CheckString(newPassword, nameof(newPassword));

            Password = newPassword;
        }
        public static User RegisterUser(string Email, string phoneNumber, string password, string userName, IUserDomainService userDomainService)
        {
            return new User(Email, userName, phoneNumber, password, userDomainService);
        }

        public void AddToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
        {
            var activeTokenCount = Tokens.Count(c => c.RefreshTokenExpireDate > DateTime.Now);
            if (activeTokenCount == 3)
                throw new InvalidDomainDataException("امکان استفاده از 4 دستگاه همزمان وجود ندارد");

            var token = new UserToken(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device);
            token.UserId = Id;
            Tokens.Add(token);
        }
        public void SetAvatar(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                imageName = "avatar.png";

            AvatarName = imageName;
        }
        public string RemoveToken(long tokenId)
        {
            var token = Tokens.FirstOrDefault(f => f.Id == tokenId);
            if (token == null)
                throw new InvalidDomainDataException("invalid TokenId");

            Tokens.Remove(token);
            return token.HashJwtToken;
        }

        public void Guard(string phoneNumber, string userName, IUserDomainService userDomainService)
        {
            if (phoneNumber.Length != 11)
                throw new InvalidDomainDataException("شماره موبایل نامعتبر است");

            if (string.IsNullOrWhiteSpace(userName))
                    throw new InvalidDomainDataException(" نام کاربری نامعتبر است");

            if (phoneNumber != PhoneNumber)
                if (userDomainService.PhoneNumberIsExist(phoneNumber))
                    throw new InvalidDomainDataException("شماره موبایل تکراری است");
            if (userName != UserName)
                if (userDomainService.UserNameIsExist(userName))
                    throw new InvalidDomainDataException(" نام کاربری تکراری است");
            if(userName!=UserName)
                if (userDomainService.UserNameIsExist(userName))
                    throw new Exception("نام کاربری تکراری است!");
                     
            if(phoneNumber!=PhoneNumber)
                if (userDomainService.PhoneNumberIsExist(phoneNumber))
                    throw new Exception("شما با این شماره تماس ثبت نام کرده اید!");
        }

    }

  
}
