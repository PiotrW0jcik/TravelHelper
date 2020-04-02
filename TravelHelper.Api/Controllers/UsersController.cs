﻿using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelHelper.Infrastructure.DTO;
using TravelHelper.Infrastructure.Services;
using TravelHelper.Infrastructure.Commands.Users;
using TravelHelper.Infrastructure.Commands;
using Microsoft.Extensions.Caching.Memory;
using TravelHelper.Infrastructure.Extensions;

namespace TravelHelper.Api.Controllers
{
     

    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _cache;
        public UsersController(IUserService userService, 
        ICommandDispatcher commandDispatcher,IJwtHandler jwtHandler,IMemoryCache cache) : base(commandDispatcher)
        {
        _cache =cache;
        _userService = userService;
        _jwtHandler = jwtHandler;  
        }
        
        [HttpGet("{email}")]
        public async Task<UserDto> GetUser(string email)
             =>await _userService.GetAsync(email);

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
            {
                await CommandDispatcher.DispatchAsync(command);
                return Created($"user/{command.Email}",new object());
            }
        
        
      //  [HttpPost("login")]
      //  public async Task<IActionResult> Post([FromBody]LoginAsync command)
      //  {//            await CommandDispatcher.DispatchAsync(command);
    //        return 
     //   }
    //       
          
         [HttpGet]
         [Route("token")]
         public async Task<IActionResult> Get()
            {
                var token = _jwtHandler.CreateToken("user1@email.com");
                return Json(token);
            }
         [HttpGet]
         [Route("auth")]
         public async Task<IActionResult> GetAuth()
            {
                return Json("Auth");
            }
        
        
    }

}
