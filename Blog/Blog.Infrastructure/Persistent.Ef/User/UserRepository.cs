using Blog.Domain.UserAgg.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;

namespace Blog.Infrastructure.Persistent.Ef.User
{
    public class UserRepository<TEntity> : IUserRepository<TEntity> where TEntity : IdentityUser
    {
        protected readonly BlogContext Context;
        public UserRepository(BlogContext context)
        {
            Context = context;
        }

        public virtual async Task<TEntity?> GetAsync(long id)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(id)); ;
        }
        public async Task<TEntity?> GetTracking(long id)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<TEntity?> GetTrackingByUserName(string userName)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.UserName == userName);
        }

        public async Task<TEntity?> GetTrackingWithString(string id)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
        }
        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        void IUserRepository<TEntity>.Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public async Task AddRange(ICollection<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }
        public void Update(TEntity entity)
        {
            Context.Update(entity);
        }
        public async Task<int> Save()
        {
            return await Context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Context.Set<TEntity>().AnyAsync(expression);
        }
        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Any(expression);
        }

        public TEntity? Get(long id)
        {
            return Context.Set<TEntity>().FirstOrDefault(t => t.Id.Equals(id)); ;
        }

        //public async Task<int> SendEmail(string Email, string Suject, string token)
        //{
        //    var fromAddress = new MailAddress("parsahavaset1@gmail.com", "Parsa Karimi");
        //    var toAddress = new MailAddress(Email);
        //    const string fromPassword = "parsadam1";

        //    var smtp = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com", // آدرس سرور SMTP
        //        Port = 587, // پورت سرور SMTP
        //        EnableSsl = true, // استفاده از SSL
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = true,
        //        Credentials = new NetworkCredential(fromAddress.Address, fromPassword) // اعتبارسنجی
        //        //Host = "smtp.example.com",
        //        //Port = 587,
        //        //EnableSsl = false,
        //        //DeliveryMethod = SmtpDeliveryMethod.Network,
        //        //UseDefaultCredentials = false,
        //        //Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        //    };

        //    using (var message = new MailMessage(fromAddress, toAddress)
        //    {
        //        Subject = Suject,
        //        Body = token
        //    })
        //    {
        //        await smtp.SendMailAsync(message);
        //    }

        //    return 1;
        //}
        public async Task<int> SendEmail(string Email, string Suject, string token)
        {

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("parsahavaset1@gmail.com"),
                To = { Email },
                Subject = Suject,
                Body = token,
                IsBodyHtml = true,
            };
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 587) // Host => forExample webmail.codeyad.com
            {
                Credentials = new System.Net.NetworkCredential("parsahavaset1@gmail.com", "parsadam1"), // UserName == Email
                EnableSsl = false
            };
            smtpServer.Send(mail);
            await Task.CompletedTask;
            return 1;
        }

        public async Task<bool> Delete(long Id)
        {
            try
            {
                var entity = await Context.Set<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(Id));
                Context.Set<TEntity>().Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(string Id)
        {
            try
            {
                var entity = await Context.Set<TEntity>().FirstOrDefaultAsync(t => t.Id.Equals(Id));
                Context.Set<TEntity>().Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<TEntity?> GetTrackingByPhoneNumber(string phoneNumber)
        {
            return await Context.Set<TEntity>().AsTracking().FirstOrDefaultAsync(t => t.PhoneNumber == phoneNumber);
        }
    }
}
