using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Utilities;
using Core.Security.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;
using Core.Security.Entities;
using Core.LDAP;
using Core.Ldap;
using Core.Security.Jwt;

namespace Application.Features.Auths.Commands
{
    public class LoginUserCommand : IRequest<LoginUserDto>
    {
        public string employeeNo { get; set; }
        public string password { get; set; }

        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IAuthService _authService;


            public LoginUserCommandHandler(
                IUserRepository userRepository,
                AuthBusinessRules authBusinessRules,
                IAuthService authService
            )
            {
                _userRepository = userRepository;
                _authBusinessRules = authBusinessRules;
                _authService = authService;
            }

            public async Task<LoginUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                if (request.employeeNo != "" && request.password != "")
                {
                    //await _authBusinessRules.CheckEmailPresence(request.mail);

                    //await _authBusinessRules.CheckPasswords(user.Id, request.password);


                    // Bu kısım Ldap sisteminden geriye kullanıcı bilgilerinin gelmesi için yazılmıştır.
                    var userData = LdapUserHelper.GetLDAPUserInfo(request.employeeNo, request.password, true); // Fonsiyonda true olan bölüm detayları getirme ayarı.Varsayılan false yapılmış.True yaptım ki detaylar gelsin.

                    if (userData.UserAuth)
                    {
                        //Login olan Ldap kullanıcısı...
                        User? user = new User
                        {
                            Name = userData.Name,
                            Surname = userData.Surname,
                            Mail = userData.Email,
                            EmployeeNo = userData.UserCode,
                            Status = true
                        };

                        // Login olan ldap kullanıcısı SPM veritabanında ki User tablosunda yoksa,o kullanıcıyı SPM/User içine eklemek için önce klulanıcı bulunmalı
                        User? userToAddSpm = await _userRepository.GetAsync(u => u.EmployeeNo == user.EmployeeNo);


                        AccessToken accessToken = new AccessToken();
                        // if içindeki userData = Eğer kullanıcı ldaptan boş gelmezse,
                        // yani kullanıcı login olursa ve kullanıcı Spm User tablosunda yoksa, o kullanıcıyı User tablosuna eklenir.
                        if (userToAddSpm == null && userData != null)
                        {
                            await _userRepository.AddAsync(user);
                            accessToken = await _authService.CreateAccessToken(user);
                        }
                        else
                        {
                            accessToken = await _authService.CreateAccessToken(userToAddSpm);
                        }


                        return new LoginUserDto
                        {
                            AccessToken = accessToken,
                            EmployeeNo = userData.UserCode,
                            Mail = userData.Email,
                            Name = userData.Name,
                            Surname = userData.Surname,
                            isSuccess = true

                        };
                    }
                    else
                    {
                        return new LoginUserDto
                        {
                            isSuccess = false,
                            Message = "Yetkiniz bulunmamaktadır!"
                        };
                    }

                }
                else
                {
                    return new LoginUserDto
                    {
                        isSuccess = false,
                        Message = "Sicil ve parola giriniz!"
                    };
                }
            }
        }
    }
}