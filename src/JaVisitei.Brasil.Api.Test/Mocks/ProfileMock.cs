using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Profile;
using JaVisitei.Brasil.Business.ViewModels.Response.Profile;
using System.Collections.Generic;
using System;

namespace JaVisitei.Brasil.Test.Mocks
{
    public static class ProfileMock
    {
        public static ProfileValidator<ActivationResponse> ProfileActivationResponseMock()
        {
            return new ProfileValidator<ActivationResponse>
            {
                Data = new ActivationResponse
                {
                    Actived = true,
                    UserEmail = "teste@teste.com"
                },
                Message = "sucesso"
            };
        }

        public static ProfileValidator<GenerateConfirmationCodeResponse> ProfileGenerateConfirmationCodeResponseMock()
        {
            return new ProfileValidator<GenerateConfirmationCodeResponse>
            {
                Data = new GenerateConfirmationCodeResponse
                {
                    Generated = true,
                    UserEmail = "teste@teste.com"
                },
                Message = "sucesso"
            };
        }

        public static ProfileValidator<ForgotPasswordResponse> ProfileForgotPasswordResponseMock()
        {
            return new ProfileValidator<ForgotPasswordResponse>
            {
                Data = new ForgotPasswordResponse
                {
                    Requested = true,
                    UserEmail = "teste@teste.com"
                },
                Message = "sucesso"
            };
        }

        public static ProfileValidator<ResetPasswordResponse> ProfileResetPasswordResponseMock()
        {
            return new ProfileValidator<ResetPasswordResponse>
            {
                Data = new ResetPasswordResponse
                {
                    Redefined = true,
                    UserEmail = "teste@teste.com"
                },
                Message = "sucesso"
            };
        }

        public static ProfileValidator<LoginResponse> ProfileLoginResponseMock()
        {
            return new ProfileValidator<LoginResponse>
            {
                Data = new LoginResponse
                {
                    Id = Guid.NewGuid().ToString(),
                    Token = "nfpasdngasASDFnasbsdfSADFA3453256et.ASDgasdgsadobh",
                    Expiration = DateTime.Now.AddMinutes(10)
                },
                Message = "sucesso"
            };
        }

        public static ResetPasswordRequest ResetPasswordRequestMock()
        {
            return new ResetPasswordRequest
            {
                ResetPasswordCode = "ASF325AS002",
                Email = "teste@teste.com",
                Password = "!Abc5678",
                RePassword = "!Abc5678"
            };
        }

        public static LoginRequest LoginRequestMock()
        {
            return new LoginRequest
            {
                Input = "teste@teste.com",
                Password = "!Abc5678"
            };
        }

        public static ProfileValidator<M> ProfileValidatorErrorMock<M>()
        {
            return new ProfileValidator<M>
            {
                Errors = new List<string> {
                    "Invalid return"
                }
            };
        }

        public static ActiveAccountRequest ActiveAccountRequestMock()
        {
            return new ActiveAccountRequest {
                ActivationCode = "2ASFSEA5001"
            };
        }

        public static GenerateConfirmationCodeRequest GenerateConfirmationCodeRequestMock()
        {
            return new GenerateConfirmationCodeRequest {
                Email = "teste@teste.com.zz"
            };
        }

        public static ForgotPasswordRequest ForgotPasswordRequestMock()
        {
            return new ForgotPasswordRequest
            {
                Email = "teste@teste.com.zz"
            };
        }
    }
}
