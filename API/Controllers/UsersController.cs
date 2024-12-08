using System;
using System.Runtime.CompilerServices;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// [ApiController]
// [Route("api/[controller]")] // /api/users -- the controller suffix gets automatically  replaced by ASP.Net | UsersController -> users
public class UsersController(DataContext context) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users.ToListAsync(); 

        return users; 
    }

    [HttpGet("{id:int}")] // api/users/1
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(int id)
    {
        var user = await context.Users.FindAsync(id); 

        if(user == null) return NotFound(); 

        return Ok(user); 
    }

}
