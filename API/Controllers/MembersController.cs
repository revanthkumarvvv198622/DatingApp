using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using System.Collections.Immutable;

namespace API.Controllers
{
    [Authorize]
    public class MembersController(AppDbContext context) : ApiBaseController
    {
        [AllowAnonymous]
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
