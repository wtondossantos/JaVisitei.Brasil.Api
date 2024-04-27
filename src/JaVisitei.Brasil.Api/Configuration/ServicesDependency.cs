using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Business.Service.Services;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Response.Profile;
using JaVisitei.Brasil.Caching.Service.Base;
using JaVisitei.Brasil.Caching.Service.Interfaces;
using JaVisitei.Brasil.Caching.Service.Services;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Data.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JaVisitei.Brasil.Api.Configuration
{
    public static class ServicesDependency
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ProfileValidator<LoginResponse>, ProfileValidator<LoginResponse>>();
            services.AddScoped<ProfileValidator<ActivationResponse>, ProfileValidator<ActivationResponse>>();
            services.AddScoped<ProfileValidator<ForgotPasswordResponse>, ProfileValidator<ForgotPasswordResponse>>();
            services.AddScoped<ProfileValidator<ResetPasswordResponse>, ProfileValidator<ResetPasswordResponse>>();
            services.AddScoped<ProfileValidator<GenerateConfirmationCodeResponse>, ProfileValidator<GenerateConfirmationCodeResponse>>();

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICountryCachingService, CountryCachingService>();

            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IStateCachingService, StateCachingService>();

            services.AddScoped<IMacroregionRepository, MacroregionRepository>();
            services.AddScoped<IMacroregionService, MacroregionService>();

            services.AddScoped<IMicroregionRepository, MicroregionRepository>();
            services.AddScoped<IMicroregionService, MicroregionService>();

            services.AddScoped<IArchipelagoRepository, ArchipelagoRepository>();
            services.AddScoped<IArchipelagoService, ArchipelagoService>();

            services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
            services.AddScoped<IMunicipalityService, MunicipalityService>();
            services.AddScoped<IMunicipalityCachingService, MunicipalityCachingService>();

            services.AddScoped<IIslandRepository, IslandRepository>();
            services.AddScoped<IIslandService, IslandService>();

            services.AddScoped<IRegionTypeRepository, RegionTypeRepository>();
            services.AddScoped<IRegionTypeService, RegionTypeService>();

            services.AddScoped<IVisitRepository, VisitRepository>();
            services.AddScoped<IVisitService, VisitService>();
            services.AddScoped<VisitValidator, VisitValidator>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserValidator, UserValidator>();

            services.AddScoped<IUserManagerRepository, UserManagerRepository>();
            services.AddScoped<IUserManagerService, UserManagerService>();

            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<EmailValidator, EmailValidator>();

            services.AddScoped<IRecaptchaService, RecaptchaService>();
            services.AddScoped<RecaptchaValidator, RecaptchaValidator>();

            services.AddScoped<ICachingService, CachingService>();
        }
    }
}
