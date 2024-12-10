﻿using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Repository
{
    public interface IRepository<TEntity>
    {
        #region Get User InformaTion by keyword
        Task<NguoiDung> GetUserByEmail(string email);


        Task<NguoiDung> GetUserByPhoneNumber(string phoneNumber);
        #endregion
        #region Get Role, Add Roles
        Task AddUserToRoleAsync(NguoiDung user, IEnumerable<string> listRoles);
        Task<IEnumerable<string>> GetRolesOfUserAsync(NguoiDung user);
        #endregion
        #region GetAllAsync
        Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification = null);
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> property = null);
        Task<IQueryable<TEntity>> GetAllAsync(List<string> includes, Expression<Func<TEntity, bool>> property = null);
        Task<IQueryable<TEntity>> GetAllAsync(string include, Expression<Func<TEntity, bool>> property = null);
        #endregion
        #region GetByIdAsync
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> GetByIdAsync(long id);
        Task<TEntity> GetByIdAsync(List<string> includes, Expression<Func<TEntity, bool>> prodecate = null);
        #endregion
        #region GetAsync
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> prodecate = null);
        #endregion
        #region DeleteAsync
        void Delete(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAsync(long id);
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> prodecate = null);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        #endregion
        #region CreateAsync
        void Add(TEntity entity);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities);
        #endregion
        #region UpdateAsync
        void Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(int id, TEntity entity);
        Task<TEntity> UpdateAsync(Guid id, TEntity entity);
        Task<TEntity> UpdateAsync(long id, TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, params object[] keyValues);
        Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);
        #endregion


    }
}
