using BlogForPeace.Api.Features.Profiles.RegisterProfile;
using BlogForPeace.Api.Features.Profiles.ViewProfile;
using BlogForPeace.Api.Features.Profiles.EditProfile;

namespace BlogForPeace.Api.Features.Profiles
{
    internal static class ProfilesModule
    {
        internal static void AddProfilesHandlers(this IServiceCollection services)
        {
            services.AddTransient<IRegisterProfileCommandHandler, RegisterProfileCommandHandler>();
            services.AddTransient<IViewProfileQueryHandler, ViewProfileQueryHandler>();
            services.AddTransient<IEditProfileCommandHandler, EditProfileCommandHandler>();
        }
    }
}
