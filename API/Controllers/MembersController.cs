using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase
    {
        [HttpGet("getmembers")]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync();
            return Ok(members);
        }

        [HttpGet("getmember/{id}")]
        public async Task<ActionResult<AppUser>> GetmemberById(string id)
        {
            var member = await context.Users.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (member == null) return NotFound();
            return Ok(member);
        }
    }
}
