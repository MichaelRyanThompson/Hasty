using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hasty.Data;
using Hasty.Services;
using Hasty.Services.Interfaces;
using Hasty.Web.Api.StartUp.DependencyInjection;
using Hasty.Web.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hasty.Web.StartUp
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is IConfigurationRoot)
            {
                services.AddSingleton<IConfigurationRoot>(configuration as IConfigurationRoot);   // IConfigurationRoot
            }

            services.AddSingleton<IConfiguration>(configuration);   // IConfiguration explicitly

            string connString = configuration.GetConnectionString("Default");
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2
            // The are a number of differe Add* methods you can use. Please verify which one you
            // should be using services.AddScoped<IMyDependency, MyDependency>();

            // services.AddTransient<IOperationTransient, Operation>();

            // services.AddScoped<IOperationScoped, Operation>();

            // services.AddSingleton<IOperationSingleton, Operation>();

            services.AddSingleton<IAuthenticationService<int>, WebAuthenticationService>();
            services.AddSingleton<ICommentService, CommentService>();
            services.AddSingleton<Hasty.Data.Providers.IDataProvider, SqlDataProvider>(delegate (IServiceProvider provider)
            {
                return new SqlDataProvider(connString);
            }
            );
            services.AddSingleton<IAppointmentService, AppointmentService>();
            services.AddSingleton<IBaseUserMapper, UserService>();
            services.AddSingleton<IBlogService, BlogService>();
            services.AddSingleton<ICharitableFundService, CharitableFundService>();
            services.AddSingleton<ICivilianProfileService, CivilianProfileService>();
            services.AddSingleton<IDonationService, DonationService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IExternalLinksService, ExternalLinksService>();
            services.AddSingleton<IFAQService, FAQService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IGoogleAnalyticsService, GoogleAnalyticsService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IIdentityProvider<int>, WebAuthenticationService>();
            services.AddSingleton<IListingService, ListingService>();
            services.AddSingleton<IListingAvailabilityExceptionsService, ListingAvailabilityExceptionsService>();
            services.AddSingleton<IListingReservationsService, ListingReservationsService>();
            services.AddSingleton<IListingsAdditionalService, ListingsAdditionalService>();
            services.AddSingleton<ILocationMapper, LocationService>();
            services.AddSingleton<ILocationService, LocationService>();
            services.AddSingleton<ILookUpService, LookUpService>();
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<IMilitaryProfileService, MilitaryProfileService>();
            services.AddSingleton<INewsletterContentService, NewsletterContentService>();
            services.AddSingleton<INewsletterSubService, NewsletterSubService>();
            services.AddSingleton<INewsletterService, NewsletterService>();
            services.AddSingleton<INewsletterTemplateService, NewsletterTemplateService>();
            services.AddSingleton<IPatriotPointsService, PatriotPointsService>();
            services.AddSingleton<IPaymentService, PaymentService>();
            services.AddSingleton<IPodcastService, PodcastService>();
            services.AddSingleton<IRatingService, RatingService>();
            services.AddSingleton<IServiceAvailableService, ServiceAvailableService>();
            services.AddSingleton<IShareStoryService, ShareStoryService>();
            services.AddSingleton<ISiteReferenceService, SiteReferenceService>();
            services.AddSingleton<IStripeSubscriptionService, StripeSubscriptionService>();
            services.AddSingleton<ISurveyQuestionAnswerOptionService, SurveyQuestionAnswerOptionService>();
            services.AddSingleton<ISurveyQuestionService, SurveyQuestionService>();
            services.AddSingleton<ISurveysService, SurveysService>();
            services.AddSingleton<IUrlTypesService, UrlTypesService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IVideoChatLogService, VideoChatLogService>();
            services.AddSingleton<IVideoChatService, VideoChatService>();

            GetAllEntities().ForEach(tt =>
            {
                IConfigureDependencyInjection idi = Activator.CreateInstance(tt) as IConfigureDependencyInjection;

                //This will not error by way of being null. BUT if the code within the method does
                // then we would rather have the error loadly on startup then worry about debuging the issues as it runs
                idi.ConfigureServices(services, configuration);
            });
        }

        public static List<Type> GetAllEntities()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                 .Where(x => typeof(IConfigureDependencyInjection).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                 .ToList();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}