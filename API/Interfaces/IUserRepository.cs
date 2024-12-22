using System;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user); 

    Task<bool> SaveAllAsync(); 
    // ? becausr the classes can return null 
    Task<AppUser?> GetUserByIdAsync(int id); 
    Task<AppUser?> GetUserByUsernameAsync(string username); 
    Task<IEnumerable<AppUser>> GetUsersAsync(); 

}
