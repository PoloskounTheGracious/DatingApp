using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class TestCaseController : BaseApiController
    {
        private readonly DataContext _context;

        public TestCaseController(DataContext context)
        {
            _context = context;

        }

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<TestCaseData>>> GetAllCases() 
        {
            return await _context.TestCase.ToListAsync();
        }

        [HttpGet("{Id}")]

        public async Task<ActionResult<TestCaseData>> GetTestCaseData(int Id)
        {
            return await _context.TestCase.FindAsync(Id);
        }

        [HttpPost("setcase")]

        public async Task<ActionResult<TestCaseData>> SetTestCaseData(TestCaseDto testCaseDto)
        {
            if (testCaseDto.TestBool == false) return BadRequest("TestBool is false");

            var test = new TestCaseData
            {
                TestString = testCaseDto.TestString,
                TestBool = testCaseDto.TestBool
            };

            _context.TestCase.Add(test);

            await _context.SaveChangesAsync();

            return test;
        }

    }
}