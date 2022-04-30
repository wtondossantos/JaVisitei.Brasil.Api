using JaVisitei.Brasil.Business.Service;
using JaVisitei.Brasil.Business.Service.Interfaces;
using JaVisitei.Brasil.Data.Repository.Interfaces;
using JaVisitei.Brasil.Data.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JaVisitei.Brasil.Api.Configuration
{
    public static class ServicesDependency
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IStateService, StateService>();

            services.AddScoped<IMacroregionRepository, MacroregionRepository>();
            services.AddScoped<IMacroregionService, MacroregionService>();

            services.AddScoped<IMicroregionRepository, MicroregionRepository>();
            services.AddScoped<IMicroregionService, MicroregionService>();

            services.AddScoped<IArchipelagoRepository, ArchipelagoRepository>();
            services.AddScoped<IArchipelagoService, ArchipelagoService>();

            services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
            services.AddScoped<IMunicipalityService, MunicipalityService>();

            services.AddScoped<IIslandRepository, IslandRepository>();
            services.AddScoped<IIslandService, IslandService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRegionTypeRepository, RegionTypeRepository>();
            services.AddScoped<IRegionTypeService, RegionTypeService>();

            services.AddScoped<IVisitRepository, VisitRepository>();
            services.AddScoped<IVisitService, VisitService>();

            services.AddScoped<IProfileService, ProfileService>();
        }
    }
}
