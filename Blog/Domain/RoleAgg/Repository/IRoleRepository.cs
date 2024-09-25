﻿using Common.Domain.Repository;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Blog.Domain.RoleAgg.Repository
{
    
    public interface IRoleRepository<T> where T : IdentityRole
    {
        Task<T?> GetAsync(long id);
        Task<bool> DeleteRole(string roleId);

        Task<T?> GetTracking(long id);
        //Task<T?> GetTrackingByPhoneNumber(string PhoneNumber);
        //Task<T?> GetTrackingByUserName(string UserName);
        Task<T?> GetTrackingWithString(string id);

        Task AddAsync(T entity);
        void Add(T entity);

        Task AddRange(ICollection<T> entities);

        void Update(T entity);

        Task<int> Save();

        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);

        bool Exists(Expression<Func<T, bool>> expression);
        Task<bool> Delete(long Id);
        Task<bool> Delete(string Id);


        T? Get(long id);

    }
}
