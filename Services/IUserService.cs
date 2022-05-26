using Inlamningsuppgift.Entities;
using Inlamningsuppgift.Models;
using Microsoft.EntityFrameworkCore;

namespace Inlamningsuppgift.Services
{
    public interface IUserService
    {
       Task<User> CreateAsync(CreateUserModel model);
       Task<IEnumerable<User>> GetAllAsync();
       Task<User> GetAsync(int id);
       Task<bool> DeleteAsync(int id);
       Task<User> UpdateAsync(int id, UpdateUserModel model);
    }


    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> CreateAsync(CreateUserModel model)
        {
            if (!await _dataContext.Users.AnyAsync(x => x.Email == model.Email))
            {
                User user = new User
                {
                    Address = model.Address,
                    City = model.City,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    PostalCode = model.PostalCode
                };
                _dataContext.Users.Add(user);
                await _dataContext.SaveChangesAsync();

                return user;
            }

            return null!;
        }

        

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var user = new List<User>();
            foreach (var c in await _dataContext.Users.ToListAsync())
                user.Add(new User
                {
                    UserId = c.UserId,
                    FirstName = c.FirstName,
                    LastName= c.LastName,
                    Email = c.Email,
                    Password = c.Password,
                    Address = c.Address,
                    City= c.City,
                    PostalCode= c.PostalCode
                });

            return user;
        }




        public async Task<User> GetAsync(int id)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (user != null)
            {
                return new User
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    Address = user.Address,
                    City = user.City,
                    PostalCode = user.PostalCode
                };
            }
            return null!;
        }






        public async Task<User> UpdateAsync(int id, UpdateUserModel model)
        {

            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user != null)
            {
                if (user.FirstName != model.FirstName && !string.IsNullOrEmpty(model.FirstName))
                {
                    user.FirstName = model.FirstName;
                }

                if (user.LastName != model.LastName && !string.IsNullOrEmpty(model.LastName))
                {
                    user.LastName = model.LastName;
                }

                if (user.FirstName != model.FirstName && !string.IsNullOrEmpty(model.FirstName))
                {
                    user.FirstName = model.FirstName;
                }

                if (user.Address != model.Address && !string.IsNullOrEmpty(model.Address))
                {
                    user.Address = model.Address;
                }

                if (user.City != model.City && !string.IsNullOrEmpty(model.City))
                {
                    user.City = model.City;
                }

                if (user.PostalCode != model.PostalCode && !string.IsNullOrEmpty(model.PostalCode))
                {
                    user.PostalCode = model.PostalCode;
                }

                _dataContext.Entry(user).State = EntityState.Modified;
                await _dataContext.SaveChangesAsync();

                return new User
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    Address = user.Address,
                    City = user.City,
                    PostalCode = user.PostalCode
                };
            }

            return null!;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _dataContext.Users.FindAsync(id);
            if (user != null)
            {
                _dataContext.Users.Remove(user);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            return false;
        }



    }
}
