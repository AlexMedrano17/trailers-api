using Microsoft.Extensions.Options;
using trailers_api.Data;

namespace trailers_api.Services
{
    public class UserService
    {
        //  private readonly AppSettings _appSettings;

        // trailersContext _context;
        // public UserService(trailersContext context, IOptions<AppSettings> appSettings){
        //     _context = context;
        //     _appSettings = appSettings.Value;
        // }

        // public AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model)
        // {
        //     var encryptedPass = Encryptor.GetSHA256(model.Pass);
        //     var user = _context.User.SingleOrDefault(x => x.Email == model.Email && x.Pass == encryptedPass);

        //     // return null if user not found
        //     if (user == null) return null;

        //     // authentication successful so generate jwt token
        //     var token = generateJwtToken(user);

        //     return new AuthenticateResponseDTO(token);
        // }

        // public IEnumerable<User> GetAll()
        // {
        //     return _context.User.ToList();
        // }

        // public User GetById(string id)
        // {
        //     return _context.User.FirstOrDefault(x => x.Id == id);
        // }

        // // helper methods
        // private string generateJwtToken(User user){
        //     // generate token that is valid for 7 days
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //     var tokenDescriptor = new SecurityTokenDescriptor
        //     {
        //         Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id) }),
        //         Expires = DateTime.UtcNow.AddDays(7),
        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //     };
        //     var token = tokenHandler.CreateToken(tokenDescriptor);
        //     return tokenHandler.WriteToken(token);
        // }
    }
}