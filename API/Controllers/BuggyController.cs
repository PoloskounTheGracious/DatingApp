using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;
        
        public BuggyController(DataContext context) {

            _context = context;

        }

        [Authorize]
        [HttpGet("auth")] 
        public ActionResult<string> GetSecret() {

            return "secret text";

        }

        [HttpGet("not-found")] 
        public ActionResult<AppUser> GetNotFound() {

            var thing = _context.Users.Find(-1); 

            if (thing == null) return NotFound();

            return Ok(thing);

        }


        [HttpGet("server-error")] 
        public ActionResult<string> GetServerError() {

            //When we use thing to Find(-1), Find() returns null and "thing" is assigned null...
            var thing = _context.Users.Find(-1); 

            //When we attempt to .ToString() a null, we get a null reference exception...
            var thingToReturn = thing.ToString();

            //Thus, thingToReturn should contain the server error we want to return from the method...
            return thingToReturn;
            
        }


        [HttpGet("bad-request")] 
        public ActionResult<string> GetBadRequest() {

            return BadRequest("HTTP 400 - Bad Request");

        }

/*
        [HttpGet("auth")] 
        public ActionResult<string> GetSecret() {

            return "secret text";
        }

        [HttpGet("auth")] 
        public ActionResult<string> GetSecret() {

            return "secret text";
        }

        [HttpGet("auth")] 
        public ActionResult<string> GetSecret() {

            return "secret text";
        }

        [HttpGet("auth")] 
        public ActionResult<string> GetSecret() {

            return "secret text";
        }
        */
        
    }
}