using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // /api/users -- the controller suffix automatically get replaced by ASP.Net 
public class UsersController(DataContext context) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        var users = context.Users.ToList(); 

        return Ok(users); 
    }

    [HttpGet("{id:int}")] // api/users/1
    public ActionResult<IEnumerable<AppUser>> GetUsers(int id)
    {
        var user = context.Users.Find(id); 

        if(user == null) return NotFound(); 

        return Ok(user); 
    }

}
