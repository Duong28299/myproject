using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MISA.HCT.ETax.Api.Interfaces;
using MISA.HCT.ETax.Api.Middleware;
using MISA.HCT.ETax.Api.Services;
using MISA.HCT.ETax.ApplicationCore.Entities;
using MISA.HCT.ETax.Infrastructure.Data;
using MISA.HCT.ETax.Infrastructure.Data.Repositories;
using MISA.HCT.ETax.Infrastructure.DatabaseContext;
using MISA.HCT.ETax.Infrastructure.UnitOfWork;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using MISA.HCT.ETax.ApplicationCore.Entities.Mapping;
using MISA.HCT.ETax.Infrastructure.Data.Repositories.Interfaces;
using MISA.HCT.ETax.Api.Utils;
using System.Net.WebSockets;
using Microsoft.Extensions.Caching.Distributed;

namespace MISA.HCT.ETax.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Configuration Interface
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
           DatabaseContext.ConnectionString = Configuration.GetConnectionString("MintaxConnection");
            services.AddCors(options => options.AddPolicy("AllowAll", builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            })
            );

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                })
             .ConfigureApiBehaviorOptions(options =>
             {
                 options.SuppressModelStateInvalidFilter = true;
             });
            ;

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MISA.HCT Etax Service", Version = "v1" });
            });

            // Config DI:
            services.AddTransient(typeof(IBaseEntityService<>), typeof(BaseService<>));
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));


            services.AddScoped<IProviderGroupService, ProviderGroupService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IProviderGroupRepository, ProviderGroupRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            // Redis cache
            services.AddDistributedMemoryCache();

            services.AddStackExchangeRedisCache(options =>
            {
#if DEBUG
                options.Configuration = Configuration.GetSection("RedisCacheConfig:RedisHostDEBUG").Value;
                options.InstanceName = Configuration.GetSection("RedisCacheConfig:InstanceName").Value;
#else
                options.Configuration = Configuration.GetSection("RedisCacheConfig:RedisHost").Value;
                options.InstanceName = Configuration.GetSection("RedisCacheConfig:InstanceName").Value;
#endif
            });
        }

		/// <summary>
		/// Configure
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		/// <param name="distributedCache"></param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDistributedCache distributedCache)
        {
            //Cấu hình CORS
            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();    
            }

            RedisCache._distributedCache = distributedCache;

			//Thực hiện validate header request
			//Tất cả service đều cần header có Authorization
			//app.Use(async (context, next) =>
			//{
			//	var actionResult = new ActionServiceResult();
			//	var path = context.Request.Path.ToString();
			//	var organizationID = context.Request.Headers["OrganizationID"].ToString();
			//	var authorization = context.Request.Headers["Authorization"].ToString();
			//	if (path.Contains("api") && (string.IsNullOrEmpty(authorization)|| String.IsNullOrEmpty(organizationID)))
			//	{
			//		context.Response.StatusCode = 401;
			//		context.Response.ContentType = "application/json";
			//		actionResult.Success = false;
			//		actionResult.MISACode = ApplicationCore.Enums.Enumeration.MISACode.ValidateEntity;
			//		actionResult.Message = MISA.HCT.ETax.ApplicationCore.Properties.Resources.ValidateEntity;
			//		actionResult.Data = null;
			//		var jsonString = JsonConvert.SerializeObject(actionResult);
			//		await context.Response.WriteAsync(jsonString, Encoding.UTF8);
			//		await context.Response.CompleteAsync();
			//		return;
			//	}
			//	await next.Invoke();
			//});

			////NHANH 10/5/2020 Cơ chế muntitenant
			//app.Use(async (context, next) =>

			//{

			//    //Lấy chuỗi kết nối từ db
			//    //using var con = new MySqlConnection(Configuration.GetConnectionString("MintaxConnection"));
			//    //con.Open();
			//    //string sql = "SELECT * FROM cars";
			//    //using var cmd = new MySqlCommand(sql, con);

			//    //using MySqlDataReader mySqlDataReader = cmd.ExecuteReader();


			//    //while (mySqlDataReader.Read())
			//    //{
			//    //    var entity = Activator.CreateInstance<T>();
			//    //    for (int i = 0; i < mySqlDataReader.FieldCount; i++)
			//    //    {
			//    //        string fieldName = mySqlDataReader.GetName(i);
			//    //        PropertyInfo property = entity.GetType().GetProperty(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
			//    //        var fieldValue = mySqlDataReader[fieldName];
			//    //        if (property != null && fieldValue != DBNull.Value)
			//    //        {
			//    //            property.SetValue(entity, fieldValue, null);
			//    //        }
			//    //    }
			//    //    yield return entity;
			//    //}


			//    //con.Close();






			//    //// Tạo chuỗi kết nối
			//    //var builder = new MySqlConnectionStringBuilder();

			//    //builder.Server = "192.168.15.18";
			//    //builder.Port = 3306;
			//    //builder.Database = "MISA_EtaxMintax_Developer";
			//    //builder.UserID = "root";
			//    //builder.Password = "misa@2020";
			//    //builder.CharacterSet = "utf8";

			//    ////gán chuỗi kết nối lấy được
			//    //DatabaseContext.ConnectionString = builder.ConnectionString;





			//    //var actionResult = new ActionServiceResult();
			//    //var path = context.Request.Path.ToString();
			//    //var organizationID = context.Request.Headers[" "].ToString();
			//    //var authorization = context.Request.Headers["Authorization"].ToString();

			//    await next.Invoke();
			//});


			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MISA Mintax Etax Service V1");
                c.RoutePrefix = "swagger";
            });
            app.UseHttpsRedirection();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            // midleware cơ chế multitenant 
            //app.UseMiddleware(typeof(MuntiTenantMidleWare));


            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
