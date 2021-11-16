using Microsoft.AspNetCore.Mvc;

// terminal commands to add migration and update database (run in order top to bottom):
// dotnet ef migrations add NameOfMigration
// dotnet ef database update 

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}