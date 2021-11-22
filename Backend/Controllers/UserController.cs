﻿using Backend.Infrastructure.Data.Repositories;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{UserId}")]
        [ActionName("GetTodoAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserModel>> GetUserById(int UserId)
        {
            var user = await _userRepository.GetUserById(UserId);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateTodoAsync(int userId, UserModel updateUserModel)
        {
            if (userId != updateUserModel.UserId)
            {
                return BadRequest();
            }

            var userbyid = await _userRepository.GetUserById(userId);
            if (userbyid is null)
            {
                return NotFound();
            }

            var userModel = new UserModel
            {
                UserId = userId,
                FullName = updateUserModel.FullName,
                Email = updateUserModel.Email,
            };

            var updatedUser = await _userRepository.UpdateUser(userModel);
            return Ok();
        }
    }
}
