using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class TestCaseDto
    {
        [Required]
        public string TestString { get; set; }

        [Required]
        public bool TestBool { get; set; }
    }
}