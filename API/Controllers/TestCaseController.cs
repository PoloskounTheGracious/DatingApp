using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestCaseController : BaseApiController
    {
        private readonly DataContext _context;

        public TestCaseController(DataContext context) 
        {
            _context = context;

        }
        
        [HttpGet("{Id}")]
        
        public async Task<ActionResult<TestCaseData>> GetTestCaseData(int Id) 
        {
            return await _context.TestCase.FindAsync(Id);
        }

    }
}