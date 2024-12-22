using System;
using System.Runtime.CompilerServices;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// [ApiController]
// [Route("api/[controller]")] // /api/users -- the controller suffix gets automatically  replaced by ASP.Net | UsersController -> users
[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
//  [AllowAnonymous] // Register without Authentication 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await userRepository.GetUsersAsync(); 

        return Ok(users); 
    }

    [HttpGet("{username}")] // api/users/username
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUser(string username)
    {
        var user = await userRepository.GetUserByUsernameAsync(username); 

        if(user == null) return NotFound(); 

        return Ok(user); 
    }

    [HttpGet("{id:int}")] // api/users/1
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUser(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id); 

        if(user == null) return NotFound(); 

        return Ok(user); 
    }

}


