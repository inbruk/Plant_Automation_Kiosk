using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Query;

using ASPNETCoreWebApiODataTestServer.Models;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.EntityFrameworkCore;
using ASPNETCoreWebApiODataTestServer.DataAccessLayer;

namespace ASPNETCoreWebApiODataTestServer
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
            services.AddOData(); // включение поддержки OData
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); // используем net core 2.1
            services.AddTransient<MyModelBuilder>();

            // настройка доступа к БД через EF, строка подключения в JSON конфигурационном файле
            string connStr = Configuration.GetConnectionString("PlantAutomationDatabase");
            services.AddDbContext<PlantAutomationContext>(options => options.UseSqlServer(connStr));

            // настройка разрешения запросов из других доменов/ориджинов
            services.AddCors( 
                options => {
                    options.AddPolicy
                    ( 
                        "AllowAllOrigins", 
                        builder => 
                        {
                            builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader(); 
                        } 
                    );
                }
            );

            // настройка проверки XSRF-Token внутри контроллеров (в них еще надо атрибут указывать)
            services.AddAntiforgery(options =>
            {
                // Set Cookie properties using CookieBuilder properties

                //options.FormFieldName = "XXSRFToken"; формы здесь не используются 
                options.HeaderName = "X-XSRF-Token";
                options.SuppressXFrameOptionsHeader = false;
            });

            // настройка фильтра генерящего XSRF-Token и выдающего его на клиент через куки XSRF-TOKEN
            //services.AddMvc(opts =>
            //{
            //    opts.Filters.AddService(typeof(AntiforgeryCookieResultFilter));
            //});
            //services.AddTransient<AntiforgeryCookieResultFilter>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MyModelBuilder modelBuilder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAllOrigins");

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapODataServiceRoute("ODataRoutes", "odata", modelBuilder.GetEdmModel(app.ApplicationServices));
            });

        }
    }
}



