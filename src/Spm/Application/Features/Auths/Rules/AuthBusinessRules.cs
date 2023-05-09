using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Utilities;

namespace Application.Features.Auths.Rules;

public class AuthBusinessRules
{
    private readonly IUserRepository _userRepository;

    public AuthBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task CheckEmailPresence(string mail)
    {
        User? user = await _userRepository.GetAsync(u => u.Mail == mail);
        if (user != null) throw new BusinessException(Messages.UserExists);
    }

        public async Task CheckEmailAbsence(string mail)
    {
        User? user = await _userRepository.GetAsync(u => u.Mail == mail);
        if (user == null) throw new BusinessException(Messages.UserDoesNotExist);
    }

    public async Task CheckEmployeeNoExistance(string employeeNo)
    {
        User? user = await _userRepository.GetAsync(u => u.EmployeeNo == employeeNo);
        if (user != null) throw new BusinessException(Messages.UserExists);
    }

    // public async Task CheckPasswords(Guid id, string password)
    // {
    //     User? user = await _userRepository.GetAsync(u => u.Id == id);

    //     if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
    //         throw new BusinessException(Messages.PasswordError);
    // }
}