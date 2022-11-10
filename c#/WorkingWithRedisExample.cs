[ApiController]
[Route("[controller]/[action]")]
public class RedisTestController : ControllerBase
{
    private readonly IRedisClient _redisClient;

    public RedisTestController(IRedisClient redisClient)
    {
        _redisClient = redisClient;
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody]UserDto userDto)
    {
        try
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Username = userDto.Username,
                Password = userDto.Password,
                Email = userDto.Email
            };
        
            var savedUser = await _redisClient.GetDefaultDatabase().AddAsync($"user:{user.Id}", user, TimeSpan.FromSeconds(20));

            return Ok(savedUser);
        }
        catch (Exception e)
        {
            await Console.Out.WriteLineAsync(e.Message);
            return BadRequest(e.Message);
        }
        
    }

    [HttpGet]
    public async Task<ActionResult> GetUser(string userId)
    {
        var user = await _redisClient.GetDefaultDatabase().GetAsync<User>($"user:{userId}");
        if (user is null) return BadRequest("User is null");
        return Ok(user);
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateUser(string userId, UserDto userUpdateData)
    {
        var user = await _redisClient.GetDefaultDatabase().GetAsync<User>($"user:{userId}");
        if (user is null) return BadRequest("User is null");

        user.Username = userUpdateData.Username;
        user.Password = userUpdateData.Password;
        user.Email = userUpdateData.Email;
        
        var saveUpdatedUser = await _redisClient.GetDefaultDatabase().AddAsync($"user:{user.Id}", user, TimeSpan.FromSeconds(20));

        return Ok(saveUpdatedUser);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllUsers()
    {
        var usersKeys = (await _redisClient.GetDefaultDatabase().SearchKeysAsync("*user*")).ToArray();
        var users = await _redisClient.GetDefaultDatabase().GetAllAsync<User>(usersKeys);
        return Ok(users);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAllUsers()
    {
        var usersKeys = (await _redisClient.GetDefaultDatabase().SearchKeysAsync("*user*")).ToArray();
        await _redisClient.GetDefaultDatabase().RemoveAllAsync(usersKeys);
        return Ok();
    }
}

public class User
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}

public class UserDto
{
    public string Username { get; set; }
    
    public string Password { get; set; } 

    public string Email { get; set; }
}