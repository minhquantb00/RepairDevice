﻿using Microsoft.EntityFrameworkCore;
using RepairManagement.Domain.Entities;
using RepairManagement.Domain.Repository;
using RepairManagement.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        protected IDbContext _IDbContext;
        protected DbSet<TEntity> _dbset;
        protected DbContext _dbContext;
        protected DbSet<TEntity> DBSet
        {
            get
            {
                if (_dbset == null)
                {
                    _dbset = _dbContext.Set<TEntity>() as DbSet<TEntity>;
                }
                return _dbset;
            }
        }
        public bool? AutoCommitEnabled { get; set; }

        private bool AutoCommitEnabledInternal
        {
            get
            {
                return this.AutoCommitEnabled ?? true;
            }
        }
        #region Get User InformaTion by keyword
        public async Task<NguoiDung> GetUserByEmail(string email)
        {
            var user = await _context.NguoiDungs.SingleOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            return user;
        }


        public async Task<NguoiDung> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await _context.NguoiDungs.SingleOrDefaultAsync(x => x.SoDienThoai.ToLower().Equals(phoneNumber.ToLower()));
            return user;
        }
        #endregion
        #region Thêm danh sách quyền cho một người dùng
        public virtual async Task AddUserToRoleAsync(NguoiDung user, IEnumerable<string> listRoles)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (listRoles == null)
            {
                throw new ArgumentNullException(nameof(listRoles));
            }
            foreach (var role in listRoles.Distinct())
            {
                var rolesOfUser = await GetRolesOfUserAsync(user);
                if (await IsStringInListAsync(role, rolesOfUser.ToList()))
                {
                    throw new ArgumentException("Người dùng đã có quyền này rồi");
                }
                else
                {
                    var roleItem = await _context.Roles.SingleOrDefaultAsync(x => x.Code.Equals(role));
                    if (roleItem == null)
                    {
                        throw new ArgumentNullException("Không có quyền này");
                    }
                    _context.UserRoles.Add(new UserRole { NguoiDungId = int.Parse((user.Id).ToString()), RoleId = int.Parse((roleItem.Id).ToString()) });
                }
            }
            _context.SaveChanges();
        }
        public virtual async Task<IEnumerable<string>> GetRolesOfUserAsync(NguoiDung user)
        {
            List<string> roles = new List<string>();
            var listRoles = _context.UserRoles.Where(x => x.NguoiDungId == user.Id).AsQueryable();
            foreach (var item in listRoles.Distinct())
            {
                var role = _context.Roles.SingleOrDefault(x => x.Id == item.RoleId);
                roles.Add(role.Code);
            }
            return roles.AsEnumerable();
        }
        public async Task DeleteRolesOfUserAsync(NguoiDung user, List<string> listRoles)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (listRoles == null)
            {
                throw new ArgumentNullException(nameof(listRoles));
            }
            foreach (var role in listRoles.Distinct())
            {
                var rolesOfUser = await GetRolesOfUserAsync(user);
                var listPermission = new List<UserRole>();
                if (await IsStringInListAsync(role, rolesOfUser.ToList()))
                {
                    var roleItem = await _context.Roles.SingleOrDefaultAsync(x => x.Code.Equals(role));
                    var permission = await _context.UserRoles.SingleOrDefaultAsync(x => x.NguoiDungId == user.Id && x.RoleId == roleItem.Id);
                    if (permission != null)
                    {
                        listPermission.Add(permission);
                    }
                }
                else
                {

                    throw new ArgumentNullException("Không có quyền này");

                }
                _context.UserRoles.RemoveRange(listPermission);
            }
            _context.SaveChanges();
        }

        #endregion
        #region Xử lí chuỗi
        private Task<bool> CompareStringAsync(string str1, string str2)
        {
            return Task.FromResult(string.Equals(str1.ToLowerInvariant(), str2.ToLowerInvariant()));
        }



        private async Task<bool> IsStringInListAsync(string inputString, List<string> listString)
        {
            if (inputString == null)
            {
                throw new ArgumentNullException(nameof(inputString));
            }

            if (listString == null)
            {
                throw new ArgumentNullException(nameof(listString));
            }

            foreach (var str in listString)
            {
                if (await CompareStringAsync(inputString, str))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        public Repository(AppDbContext context, IDbContext idbContext)
        {
            _context = context;
            _IDbContext = idbContext;
            _dbContext = (DbContext)idbContext;
        }
        #region CreateAsync
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            DBSet.Add(entity);
            await _IDbContext.CommitChangesAsync();
            return entity;
        }
        public virtual void Add(TEntity entity)
        {
            DBSet.Add(entity);

            if (this.AutoCommitEnabledInternal)
            {
                _context.SaveChanges();
            }
            DBSet.Attach(entity).State = EntityState.Detached;
        }
        public async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities)
        {
            DBSet.AddRange(entities);
            await _IDbContext.CommitChangesAsync();
            return entities;
        }
        #endregion
        #region DeleteAsync
        public virtual void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;

            if (this.AutoCommitEnabledInternal)
            {
                _context.SaveChanges();
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var dataEntity = await DBSet.FindAsync(id);
            if (dataEntity != null)
            {
                DBSet.Remove(dataEntity);
                await _IDbContext.CommitChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var dataEntity = await DBSet.FindAsync(id);
            if (dataEntity != null)
            {
                DBSet.Remove(dataEntity);
                await _IDbContext.CommitChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var dataEntity = await DBSet.FindAsync(id);
            if (dataEntity != null)
            {
                DBSet.Remove(dataEntity);
                await _IDbContext.CommitChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> prodecate = null)
        {
            var dataEntity = prodecate != null ? DBSet.Where(prodecate) : null;
            if (dataEntity != null)
            {
                DBSet.RemoveRange(dataEntity);
                await _IDbContext.CommitChangesAsync();
                return true;
            }
            return false;
        }
        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }

            if (this.AutoCommitEnabledInternal)
            {
                _context.SaveChanges();
            }
        }
        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }

            if (this.AutoCommitEnabledInternal)
            {
                await _context.SaveChangesAsync();
            }
        }
        #endregion
        #region 
        public async Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification = null)
        {
            return await ApplySpecification(specification)
                .ToListAsync();
        }
        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> property = null)
        {
            IQueryable<TEntity> query = property != null ? DBSet.Where(property) : DBSet;
            return query;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(List<string> includes, Expression<Func<TEntity, bool>> property = null)
        {
            IQueryable<TEntity> query = BuildQueryable(includes, property);
            return query;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(string include, Expression<Func<TEntity, bool>> property = null)
        {
            IQueryable<TEntity> query;
            if (!string.IsNullOrWhiteSpace(include))
            {
                query = BuildQueryable(new List<string> { include }, property);
                return query;
            }
            else
                return await GetAllAsync(property);
        }
        #endregion
        #region GetAsync
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> prodecate = null)
        {
            return await DBSet.FirstOrDefaultAsync(prodecate);
        }
        #endregion
        #region GetByIdAsync
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await DBSet.FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await DBSet.FindAsync(id);
        }
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DBSet.FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(List<string> includes, Expression<Func<TEntity, bool>> prodecate = null)
        {
            IQueryable<TEntity> query = BuildQueryable(includes, prodecate);
            return await query.FirstOrDefaultAsync();
        }
        #endregion
        #region UpdateAsync
        private void ChangeStateToModifiedIfApplicable(TEntity entity)
        {
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                // Entity was detached before or was explicitly constructed.
                // This unfortunately sets all properties to modified.
                entry.State = EntityState.Modified;
            }
        }
        public virtual void Update(TEntity entity)
        {
            ChangeStateToModifiedIfApplicable(entity);

            if (this.AutoCommitEnabledInternal)
            {
                _context.SaveChanges();
            }
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _IDbContext.CommitChangesAsync();
            return entity;

        }

        public async Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            var Data = await DBSet.FindAsync(id);
            if (Data != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _IDbContext.CommitChangesAsync();
            }
            return entity;
        }

        public async Task<TEntity> UpdateAsync(long id, TEntity entity)
        {
            var Data = await DBSet.FindAsync(id);
            if (Data != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _IDbContext.CommitChangesAsync();
            }
            return entity;
        }

        public async Task<TEntity> UpdateAsync(Guid id, TEntity entity)
        {
            var Data = await DBSet.FindAsync(id);
            if (Data != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _IDbContext.CommitChangesAsync();
            }
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, params object[] keyValues)
        {
            var Data = await DBSet.FindAsync(keyValues);
            if (Data != null)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _IDbContext.CommitChangesAsync();
            }
            return entity;
        }

        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _IDbContext.CommitChangesAsync();
            return entities;
        }
        #endregion
        #region BuildQueryable
        protected IQueryable<TEntity> BuildQueryable(List<string> includes, Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = DBSet.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            if (includes != null && includes.Count > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }


        #endregion

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(DBSet.AsNoTracking(), spec);
        }
    }
}
