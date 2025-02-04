using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneIncSample.Data;
using OneIncSample.Models;

namespace OneIncSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet(Name = "users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            _logger.LogInformation("Getting all users");
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            _logger.LogInformation("Getting user with ID: {UserId}", id);
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                _logger.LogWarning("User with ID: {UserId} not found", id);
                return NotFound();  // Returns 404 Not Found in JSON format
            }

            return Ok(user); //Returns data in JSON Format
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                _logger.LogWarning("Duplicate email found: {Email}", user.Email);
                return BadRequest("Email must be unique.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User created: {UserId}", user.Id);
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }

       

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {            
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                _logger.LogWarning("User with ID: {UserId} not found", id);
                return NotFound();
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.PhoneNumber = user.PhoneNumber;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("User updated: {UserId}", id);
                return Ok(existingUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogError("Concurrency issue while updating user with ID: {UserId}", id);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID: {UserId} not found", id);
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User deleted: {UserId}", id);

            return Ok(user);
        }
    }
}
