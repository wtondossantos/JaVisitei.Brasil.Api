using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.User;
using JaVisitei.Brasil.Business.ViewModels.Response.User;
using System.Collections.Generic;
using System;

namespace JaVisitei.Brasil.Test.Mocks
{
    public static class UserMock
    {
        public static InsertUserRequest CreateUserBasicRequestMock()
        {
            return new InsertUserRequest
            {
                Name = "Wellington",
                Username = "Silva",
                Surname = "wellington",
                Email = "teste@teste.com",
                ReEmail = "teste@teste.com",
                Password = "!Abc5678",
                RePassword = "!Abc5678"
            };
        }

        public static UserValidator CreatedUserBasicResponseMock()
        {
            return new UserValidator
            {
                Data = new UserResponse
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Wellington",
                    Username = "wellington",
                    Surname = "Silva",
                    Email = "teste@teste.com",
                    Password = "12345678901234567890123456789",
                    UserRoleId = 3,
                    SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49"
                },
                Message = "sucesso"
            };
        }

        public static InsertFullUserRequest CreateUserAdminRequestMock()
        {
            return new InsertFullUserRequest
            {
                Name = "Wellington",
                Surname = "Silva",
                Username = "wellington",
                Email = "teste@teste.com",
                ReEmail = "teste@teste.com",
                Password = "!Abc5678",
                RePassword = "!Abc5678"
            };
        }

        public static UserValidator CreatedUserAdminResponseMock()
        {
            return new UserValidator
            {
                Data = new UserResponse
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Wellington",
                    Username = "wellington",
                    Surname = "Silva",
                    Email = "teste@teste.com",
                    Password = "12345678901234567890123456789",
                    UserRoleId = 1,
                    SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49"
                },
                Message = "sucesso"
            };
        }

        public static UpdateUserRequest UpdateUserRequestMock()
        {
            return new UpdateUserRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "David",
                Surname = "Coffee",
                Username = "wellingtonedit",
                Email = "teste1@teste.com",
                ReEmail = "teste1@teste.com",
                OldPassword = "OldPass123",
                Password = "!Abc56789",
                RePassword = "!Abc56789",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49"
            };
        }

        public static UpdateFullUserRequest UpdateFullUserRequestMock()
        {
            return new UpdateFullUserRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "David",
                Surname = "Coffee",
                Username = "wellingtonedit",
                Email = "teste1@teste.com",
                ReEmail = "teste1@teste.com",
                OldPassword = "OldPass123",
                Password = "!Abc56789",
                RePassword = "!Abc56789",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                UserRoleId = 2
            };
        }
       
        public static UserValidator CreatedUserContributorResponseMock()
        {
            return new UserValidator
            {
                Data = new UserResponse
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "David",
                    Surname = "Coffee",
                    Username = "wellingtonedit",
                    Email = "teste1@teste.com",
                    Password = "!Abc56789",
                    SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                    RegistryDate = DateTime.Now,
                    Actived = true,
                    UserRoleId = 2
                },
                Message = "sucesso"
            };
        }

        public static List<UserResponse> GetListUserTest()
        {
            return new List<UserResponse>
            {
                UserActivedBasicMock(),
                UserContributorInactiveMock(),
                UserActivedContributorMock()
            };
        }

        public static UserResponse UserActivedBasicMock()
        {
            return new UserResponse
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Wellington",
                Surname = "Silva",
                Username = "wellington",
                Email = "teste@teste.com",
                RegistryDate = DateTime.Now,
                Actived = true,
                UserRoleId = 3
            };
        }

        public static UserResponse UserContributorInactiveMock()
        {
            return new UserResponse
            {
                Id = Guid.NewGuid().ToString(),
                Name = "John",
                Surname = "Bob",
                Username = "johnbob",
                Email = "teste@teste.com",
                RegistryDate = DateTime.Now,
                Actived = true,
                UserRoleId = 2
            };
        }

        public static UserResponse UserActivedContributorMock()
        {
            return new UserResponse
            {
                Id = Guid.NewGuid().ToString(),
                Name = "David",
                Surname = "Coffee",
                Username = "wellingtonedit",
                Email = "teste1@teste.com",
                Password = "!Abc56789",
                SecurityStamp = "7223d690-2d88-4eae-a425-b57f9e767e49",
                RegistryDate = DateTime.Now,
                Actived = true,
                UserRoleId = 2
            };
        }

        public static UserValidator UserValidatorErrorMock()
        {
            return new UserValidator
            {
                Errors = new List<string> {
                    "Invalid return"
                }
            };
        }
    }
}
