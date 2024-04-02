namespace SynCore.Api.Services;

public class AuthUserProvider
{
    private const string IdType = "id";
    private const string NameType = "name";
    private const string EmailType = "email";
    private const string CollegeType = "college";
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private AuthUser _authUser;
    public AuthUser GetAuthUser()
    {
        if (_httpContextAccessor?.HttpContext == null)
        {
            return null;
        }
        
        if (_authUser is null)
        {
            var claims = _httpContextAccessor.HttpContext.User
                .Claims
                .ToArray();

            if (claims.Length == 0)
            {
                return null;
            }

            _authUser = new AuthUser()
            {
                Id = new Guid(claims.First(c => c.Type == IdType).Value),
                Name = claims.First(c => c.Type == NameType).Value,
                Email = claims.First(c => c.Type == EmailType).Value,
                College = claims.First(c => c.Type == CollegeType).Value,
            };
        }

        return _authUser;
    }
}

public class AuthUser
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string College { get; set; }
}