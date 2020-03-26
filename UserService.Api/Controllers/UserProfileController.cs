using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserService.BLL.Contracts;
using UserService.Infrastructure.DTO;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileManager userProfileManager;

        public UserProfileController(IUserProfileManager userProfileManager)
        {
            this.userProfileManager = userProfileManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDTO>> GetProfile([FromRoute] long id)
        {
            // var result = await this.userProfileManager.GetByIdAsync(id);

            await this.userProfileManager.UpdateAsync(new UserUpdateDTO {Id = 1, FirstName = "New", SecondName = "Name", Email = "superemail@test.com"});

            //if (result == null)
            //{
            //    return NotFound();
            //}

            return Ok(await this.userProfileManager.GetByIdAsync(id));
        }

        [HttpPost]
        public async ValueTask<IActionResult> AddProfile([FromBody] UserCreateDTO profile)
        {
            var id = await this.userProfileManager.CreateAsync(profile);

            return CreatedAtAction(nameof(GetProfile), new { id }, profile);
        }

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> UpdateProfile([FromRoute] int id, [FromBody] UserUpdateDTO profile)
        {
            var result = await this.userProfileManager.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            profile.Id = id;
            await this.userProfileManager.UpdateAsync(profile);

            return Ok(profile);
        }
    }
}