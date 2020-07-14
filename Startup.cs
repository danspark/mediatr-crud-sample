using MediatR.Sample.Core.Behaviors;
using MediatR.Sample.Core.Commands;
using MediatR.Sample.Core.Infrastructure;
using MediatR.Sample.Core.Persistence;
using MediatR.Sample.Core.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace MediatR.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddCommandsAndQueries(services);

            AddPersistence(services);

            AddLogging(services);

            services.AddControllers();

            services.AddSingleton<DomainEventPublisher>();
        }

        private static void AddLogging(IServiceCollection services)
        {
            services.AddLogging(builder =>
                builder.AddSerilog(dispose: true)
                    .AddConsole());
        }

        private static void AddPersistence(IServiceCollection services)
        {
            services.AddDbContext<SamplesContext>(builder =>
                builder.UseInMemoryDatabase("sample")
                    .ConfigureWarnings(opt => opt.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
        }

        private static void AddCommandsAndQueries(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);

            services.AddScoped(typeof(IRequestHandler<,>), typeof(CreateEntityCommand<>.Handler));
            services.AddScoped(typeof(IRequestHandler<,>), typeof(ReadEntityQuery<>.Handler));
            services.AddScoped(typeof(IRequestHandler<,>), typeof(UpdateEntityCommand<>.Handler));
            services.AddScoped(typeof(IRequestHandler<,>), typeof(DeleteEntityCommand<>.Handler));
            services.AddScoped(typeof(IRequestHandler<,>), typeof(ListEntitiesQuery<>.Handler));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionalBehavior<,>));

            //reminder: this is automatically registered by the library: https://github.com/jbogard/MediatR/issues/453
            //services.AddScoped(typeof(IRequestPreProcessor<>), typeof(PreProcessorLoggingBehavior<>));
            //services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(PostProcessorLoggingBehavior<,>));
            //services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(NullEntityFilter<,>));
            //services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(PublishCreationEventBehavior<,>));
            //services.AddScoped(typeof(INotificationHandler<>), typeof(EntityCreationNotificator<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
