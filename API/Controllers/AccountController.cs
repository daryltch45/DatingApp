using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context): BaseApiController
{
    [HttpPost("register")] // account/register

    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto){
        
        if(await userExists(registerDto.Username)) return BadRequest("Username is taken"); 

        using var hmac = new HMACSHA512(); 

        var user = new AppUser
        {
            UserName = registerDto.Username.ToLower(), 
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)), 
            PasswordSalt = hmac.Key
        }; 

        context.Users.Add(user); 
        await context.SaveChangesAsync(); 

        return user; 


    }

    [HttpPost("login")] // account/login
    public async  Task<ActionResult<AppUser>> login(LoginDto loginDto){

        if(!(await userExists(loginDto.Username))) return BadRequest($"User {loginDto.Username} doesn't exists."); 

        var user = await context.Users.FirstOrDefaultAsync(
            u => u.UserName.ToLower() == loginDto.Username.ToLower()); 

        using var hmac = new HMACSHA512(user.PasswordSalt); 
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for(int i = 0; i < computedHash.Length && i < user.PasswordHash.Length; i++){
            if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password !");             
        }

        return user; 
    }


    private async Task<bool> userExists(string username){
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower()); 
    }
}
