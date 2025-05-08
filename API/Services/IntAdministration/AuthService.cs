using AutoMapper;
using softserve.projectlabs.Shared.DTOs.User;
using softserve.projectlabs.Shared.Utilities;
using API.Implementations.Domain;
using API.Models.IntAdmin;

namespace API.Services.IntAdmin;

public class AuthService : IAuthService
{
    private readonly UserDomain _userDomain;
    private readonly TokenGenerator _tokenGenerator;
    private readonly IMapper _mapper;

    public AuthService(UserDomain userDomain, TokenGenerator tokenGenerator, IMapper mapper)
    {
        _userDomain = userDomain;
        _tokenGenerator = tokenGenerator;
        _mapper = mapper;
    }

    public async Task<Result<User>> RegisterAsync(UserCreateDto dto)
    {
        var domainModel = _mapper.Map<User>(dto);
        return await _userDomain.CreateUserAsync(domainModel, plainPassword: dto.UserPassword);
    }

    public async Task<Result<string>> LoginAsync(UserLoginDto dto)
    {
        var userResult = await _userDomain.AuthenticateAsync(dto.UserEmail, dto.UserPassword);
        if (!userResult.IsSuccess)
            return Result<string>.Failure(userResult.ErrorMessage);

        var token = _tokenGenerator.GenerateJwt(userResult.Data);
        return Result<string>.Success(token);
    }

    public async Task<Result<string>> RefreshTokenAsync(string refreshToken)
    {
        return Result<string>.Failure("Not implemented yet.");
    }
}
