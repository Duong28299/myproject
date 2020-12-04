using Microsoft.AspNetCore.Http;
using MISA.Mintax.ETax.Api.Properties;
using MISA.Mintax.ETax.ApplicationCore.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static MISA.Mintax.ETax.ApplicationCore.Enums.Enumeration;
using MISA.Mintax.ETax.ApplicationCore;
using Microsoft.Extensions.Logging;
using MISA.Mintax.ETax.Infrastructure.Data;
using MySql.Data.MySqlClient;
using MISA.Mintax.ETax.Infrastructure.DatabaseContext;
using Microsoft.Extensions.Configuration;
using MISA.Mintax.ETax.Api.Common;
using System.Reflection;
using System.IO;
using System.Text;
using MISA.Mintax.ETax.Api.Utils;
using Microsoft.Extensions.Options;

namespace MISA.Mintax.ETax.Api.Middleware
{
    /// <summary>
    /// Xử lý khi có exception xảy ra
    /// </summary>
    /// CreatedBy: NVMANH (05/2020)
    public class MuntiTenantMidleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        private readonly string _muntitenantCatalog;
        private readonly string _muntitenantServer;
        private readonly int _weighPerDatabase;
        private readonly IOptions<AppsettingConfig> _configuration;


        public IConfiguration Configuration { get; }

		/// <summary>
		/// ErrorHandle
		/// </summary>
		/// <param name="next"></param>
		/// <param name="logger"></param>
		/// <param name="configuration"></param>
		/// <param name="config"></param>
		public MuntiTenantMidleWare(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IConfiguration configuration, IOptions<AppsettingConfig> config)
        {
            _configuration = config;
            _logger = logger;
            this.next = next;
            Configuration = configuration;
            _muntitenantCatalog = Configuration.GetConnectionString("CataLog");
            _muntitenantServer = Configuration.GetConnectionString("ServertocreateMultitenant");
            _weighPerDatabase = int.Parse(Configuration.GetSection("MaxWeighPerDatabase").Value);

        }
        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await ConnectionstringByTenentID(context, _logger, 0);
                //await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(
                new ActionServiceResult
                {
                    Success = false,
                    Message = ApplicationCore.Properties.Resources.Exception,
                    MISACode = MISACode.Exception,
                    Data = ex
                });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            logger.LogError("Exception : {result}", result);
            return context.Response.WriteAsync(result);
        }
        /// <summary>
        /// Lẫy dữ liệu đổi constring theo tenentID
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        /// <param name="flag">flag = 0 lỗi lần 1 => cập nhật lại cache flag = 1 cập nhật cache r mà vẫn lỗi </param>
        /// Created By NHANH 10/6/2020
        private async Task ConnectionstringByTenentID(HttpContext context, ILogger<ErrorHandlingMiddleware> logger, int flag)
        {
            // Lấy Tenent ID từ token 
            var token = context.Request.Headers["Authorization"].ToString();
            var TenantID = Guid.Parse(CommonFunction.GetClaimFromToken(token, "tenant_id"));

            // nhanh2 flag=1 cập nhật lại cache
            if (flag == 1)
            {
                await LoadRedisCacheByID(TenantID.ToString());
            }
            var existingTenantID = (TenantConnectionString)await (RedisCache.GetCacheAsync<TenantConnectionString>(TenantID.ToString()));
            //existingTenantID = null;
            if (existingTenantID != null)
            {
                try
                {
                    DatabaseContext.ConnectionString = existingTenantID.connectionString;
                    await this.next(context);
                }
                catch (Exception)
                {
                    // NHANH2 đã cập nhật lại cache mà vẫn lỗi 
                    if (flag == 1)
                        throw;
                    await ConnectionstringByTenentID(context, logger, 1);
                }
            }
            else
            {
                DatabaseContext.ConnectionString = _muntitenantCatalog;
                //var procedureName1 = "Proc_CheckTenentByTenentID";
                //var temp = databaseConnector.GetData<int>(procedureName1, new object[] { TenantID });

                // Tìm tenant trong cache 
                // nếu chưa có thì tìm trong db catalog 

                // Xét tenentID Đã tồn tại hay chưa 
                var procedureName1 = "Proc_GetTenentByTenentID";
                var TenantCatalog = GetData<CataLog>(procedureName1, new object[] { TenantID }).FirstOrDefault();

                // Nếu chưa có tenant này
                if (TenantCatalog == null)
                {
                    // Check license 
                    //NHANH 10/7/2020 Hiện tại token chưa chứa thông tin license chờ rồi dựng
                    var weighAfterCheckLicense = 10;

                    //Tìm database hiện tại và xét weight database đó nếu database đấy tổng weight đã vượt quá thì udpate trạng thái và tạo db mới
                    DatabaseContext.ConnectionString = _muntitenantCatalog;
                    var procedureName3 = "Proc_GetCurrentTenentServer";
                    var currentTenentServer = GetData<TenentServer>(procedureName3, null).ToList().FirstOrDefault();
                    // Todo : Xử lý lỗi không có database nào 

                    // Đã có một trigger cập nhật SumTenantWeigh 

                    //}
                    //nếu đầy thì cập nhật trạng thái đầy và tìm db mới để lưu
                    if (currentTenentServer.SumTenantWeight >= currentTenentServer.MaxWeight)
                    {
                        currentTenentServer.Status = 2;
                        var updateResult = UpdateEntity<TenentServer>(currentTenentServer).Result;

                        // Tiến hành tạo mới db 
                        // TODO : Chỗ này cần tiến hành xử lý bất đồng bộ 
                        //Gom code lại sau 
                        var dbName = "MISA_EtaxMintax_Developer" + (currentTenentServer.TenentServerID + 2) + "_" + DateTime.Now.ToString("yyyy"); // đảm bảo unique

                        _ = CreateNewDatabase(dbName, currentTenentServer);

                        // Tạo mới db thành công 
                        // Lưu thông tin db mới tạo 
                        DatabaseContext.ConnectionString = _muntitenantCatalog;
                        var findEmtyDBServer = "Proc_FindEmtyDBServer";
                        var emptyTenentServer = GetData<TenentServer>(findEmtyDBServer, null).ToList().FirstOrDefault();
                        //Set tenant đến server đó 
                        var newTenantCatalog = new CataLog()
                        {
                            TenantID = TenantID,
                            TenantServerID = emptyTenentServer.TenentServerID,
                            Weigh = weighAfterCheckLicense
                        };
                        var insertNewTenantCatalog = InsertEntity<CataLog>(newTenantCatalog).Result;
                        // Cập nhật trạng thái server đó 
                        emptyTenentServer.Status = 1;
                        var updateCatalogResult = UpdateEntity<TenentServer>(emptyTenentServer).Result;
                    }
                    else
                    {
                        // Chưa đầy thì set tenent này cho db đó 
                        // nhanh2 : Đã test và ok 
                        var tenantInCatalog = new CataLog()
                        {
                            TenantID = TenantID,
                            Weigh = weighAfterCheckLicense,
                            TenantServerID = currentTenentServer.TenentServerID
                        };
                        var insertTenantToCataLogResult = InsertEntity<CataLog>(tenantInCatalog).Result;
                        if (insertTenantToCataLogResult == 1)
                        {
                            // Thành công

                        }
                        else
                        {
                            // trả về lỗi
                        }

                    }
                    // Phân đến database đang tạo 
                }
                // nếu tenant này đã tồn tại

                // Lấy database kết nối mà tenentID đó kếkt nối đến
                var procedureName2 = "Proc_GetTenentServerByID";
                var data = GetData<TenentServer>(procedureName2, new object[] { TenantID });
                //NHANH2CGETDATA.CommitTransaction();
                //Tạo chuỗi kết nối
                var tenentserver = data.ToList().FirstOrDefault();
                // Lấy chuỗi kết nối từ db lên 

                var builder = new MySqlConnectionStringBuilder();
                builder.Server = tenentserver.Server;
                builder.Port = (uint)tenentserver.Port;
                builder.Database = tenentserver.DatabaseSouce;
                builder.UserID = tenentserver.User;
                builder.Password = tenentserver.Password;
                builder.CharacterSet = tenentserver.CharSet;

                DatabaseContext.ConnectionString = builder.ConnectionString;
                var tenantConnectionString = new TenantConnectionString() { connectionString = builder.ConnectionString };
                await RedisCache.SetCacheAsync(TenantID.ToString(), tenantConnectionString);
                await next(context);
                //var code = HttpStatusCode.InternalServerError;
                //var result = JsonConvert.SerializeObject(
                //    new ActionServiceResult
                //    {
                //        Success = false,
                //        Message = ApplicationCore.Properties.Resources.Exception,
                //        MISACode = MISACode.Exception,
                //    });
                //context.Response.ContentType = "application/json";
                //context.Response.StatusCode = (int)code;
                //logger.LogError("Exception : {result}", result);
                ////return context.Response.WriteAsync(result);
            }
        }

        /// <summary>
        /// Thực hiện thêm mới DB khi DB cũ đầy dữ liệu
        /// </summary>
        /// <param name="dbName">Tên DB</param>
        /// <param name="currentTenentServer">Thông tin đơn vị hiện tại</param>
        /// <returns></returns>
        /// created by vdthang 26.11.2020
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task CreateNewDatabase(string dbName, TenentServer currentTenentServer)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var file = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "MariaDBScrip", "NHANH2ETAXTABLEONLY.sql"), Encoding.UTF8);
            var scripSetDatabase = " SET NAMES 'utf8' ; CREATE DATABASE IF NOT EXISTS   " + dbName + "  CHARACTER SET utf8 COLLATE utf8_general_ci; USE " + dbName + "  ;   ";
            using (var conn = new MySqlConnection(_muntitenantServer))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = scripSetDatabase;
                // đang lỗi 
                cmd.ExecuteNonQuery();
                var conn2 = new MySqlConnection(_muntitenantServer + "database =" + dbName + ";");
                var temp = "USE  " + dbName + " ;";
                MySqlScript script = new MySqlScript(conn2, "SET NAMES 'utf8';" + temp + file);
                //script.Delimiter = "$$";
                script.Execute();
            }
            var newTenantServer = new TenentServer()
            {
                TenentServerID = currentTenentServer.TenentServerID + 2,
                Server = _muntitenantServer.Split(";")[0].Split("=")[1],
                Port = int.Parse(_muntitenantServer.Split(";")[1].Split("=")[1]),
                DatabaseSouce = dbName,
                MaxWeight = _weighPerDatabase,
                User = _muntitenantServer.Split(";")[2].Split("=")[1],
                Password = _muntitenantServer.Split(";")[3].Split("=")[1],
                CharSet = _muntitenantServer.Split(";")[4].Split("=")[1],
                SumTenantWeight = 0,
                Status = 0,
            };

            // Update trạng thái là db đã đầy 

            //TODO : CHeck thêm trường hợp insert lỗi

            // Update thành công 

            var insertTenantToCataLogResult = InsertEntity<TenentServer>(newTenantServer).Result;
            if (insertTenantToCataLogResult != 0)
            {
                //thành công

            }
            else
            {
                // trả về lỗi

            }
            //Tìm db chưa được sử dụng ( status = o để lưu vào )

            //nhanh2 10/8/2020 : đã testok

        }
        /// <summary>
        /// Cập nhật lại cache
        /// </summary>
        /// <returns></returns>
        /// NHANH 19/10/2020
        public async Task LoadRedisCache()
        {
            //await RedisCache.ClearCacheAsync(tenantID);
            var GetTenantDBServer = "Proc_GetCatalogAndTenantServer";
            DatabaseContext.ConnectionString = _muntitenantCatalog;

            var TenentServer = GetData<ConnectServer>(GetTenantDBServer, null).ToList();

            foreach (var item in TenentServer)
            {
                var key = item.TenantID;

                //Tạo chuỗi kết nối
                var tenentserver = item;
                // Lấy chuỗi kết nối từ db lên 

                var builder = new MySqlConnectionStringBuilder();
                builder.Server = tenentserver.Server;
                builder.Port = (uint)tenentserver.Port;
                builder.Database = tenentserver.DatabaseSouce;
                builder.UserID = tenentserver.User;
                builder.Password = tenentserver.Password;
                builder.CharacterSet = tenentserver.CharSet;

                var tenantConnectionString = new TenantConnectionString() { connectionString = builder.ConnectionString };
                await RedisCache.SetCacheAsync(key.ToString(), tenantConnectionString);
            }
            }
        /// <summary>
        /// Cập nhật cache theo keyvalue
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        public async Task LoadRedisCacheByID(string tenantID)
        {
            DatabaseContext.ConnectionString = _muntitenantCatalog;
            // Lấy database kết nối mà tenentID đó kếkt nối đến
            var procedureName2 = "Proc_GetTenentServerByID";
            var data = GetData<TenentServer>(procedureName2, new object[] { tenantID });
            if (data != null)
            {
                var key = tenantID;

                //Tạo chuỗi kết nối
                var tenentserver = data.ToList().FirstOrDefault();
                // Lấy chuỗi kết nối từ db lên 

                var builder = new MySqlConnectionStringBuilder();
                builder.Server = tenentserver.Server;
                builder.Port = (uint)tenentserver.Port;
                builder.Database = tenentserver.DatabaseSouce;
                builder.UserID = tenentserver.User;
                builder.Password = tenentserver.Password;
                builder.CharacterSet = tenentserver.CharSet;

                var tenantConnectionString = new TenantConnectionString() { connectionString = builder.ConnectionString };
                await RedisCache.SetCacheAsync(key.ToString(), tenantConnectionString);
            }
        }
        private Task<object> SaveChangeAndReturnSingleValue<T>(DatabaseConnector databaseConnector, string procedureName, object entity)
        {
            databaseConnector.MapParameterValueAndEntityProperty<T>(procedureName, (T)entity);
            return databaseConnector.ExecuteScalar();
        }
        protected virtual async Task<object> InsertAndReturnSingleValue<T>(string procedureName, object entity)
        {
            using (DatabaseConnector databaseConnector = new DatabaseConnector())
            {
                var value = await SaveChangeAndReturnSingleValue<T>(databaseConnector, procedureName, (T)entity);
                databaseConnector.CommitTransaction();
                return value;
            }
        }
        public Task<int> InsertEntity<T>(object entity)
        {
            using (DatabaseConnector databaseConnector = new DatabaseConnector())
            {
                var procedureName = DatabaseUtility.GeneateStoreName<T>(ProcdureTypeName.Insert);
                var value = SaveChangeAndReturnRecordEffect<T>(databaseConnector, procedureName, entity);
                databaseConnector.CommitTransaction();
                return value;
            }
        }
        public Task<int> UpdateEntity<T>(object entity)
        {
            using (DatabaseConnector databaseConnector = new DatabaseConnector())
            {
                var procedureName = DatabaseUtility.GeneateStoreName<T>(ProcdureTypeName.Update);
                var value = SaveChangeAndReturnRecordEffect<T>(databaseConnector, procedureName, entity);
                databaseConnector.CommitTransaction();
                return value;
            }
        }

        protected Task<int> SaveChangeAndReturnRecordEffect<T>(DatabaseConnector databaseConnector, string procedureName, object entity)
        {
            databaseConnector.MapParameterValueAndEntityProperty<T>(procedureName, (T)entity);
            return databaseConnector.ExecuteNonQuery();
        }


        public async Task<object> ExecuteProcedureText(string procedureName, object[] parameters)
        {
            using (DatabaseConnector databaseConnector = new DatabaseConnector())
            {
                var value = await Excute(procedureName, parameters, databaseConnector);
                databaseConnector.SetCommandType(System.Data.CommandType.Text);
                databaseConnector.CommitTransaction();
                return value;
            }
        }

        public async Task<object> Excute(string procedureName, object[] parameters, DatabaseConnector databaseConnector)
        {
            if (parameters != null && parameters.Length > 0)
                databaseConnector.SetParameterValue(procedureName, parameters);
            return await databaseConnector.ExecuteScalar();
        }


        public IEnumerable<T> GetData<T>(string procedureName, object[] parameters)
        {
            using (DatabaseConnector databaseConnector = new DatabaseConnector())
            {
                if (parameters != null && parameters.Length > 0)
                    databaseConnector.SetParameterValue(procedureName, parameters);
                using (MySqlDataReader mySqlDataReader = databaseConnector.ExecuteReader(procedureName))
                {
                    while (mySqlDataReader.Read())
                    {
                        var entity = Activator.CreateInstance<T>();
                        for (int i = 0; i < mySqlDataReader.FieldCount; i++)
                        {
                            string fieldName = mySqlDataReader.GetName(i);
                            PropertyInfo property = entity.GetType().GetProperty(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                            var fieldValue = mySqlDataReader[fieldName];
                            if (property != null && fieldValue != DBNull.Value)
                            {
                                property.SetValue(entity, fieldValue, null);
                            }
                        }
                        yield return entity;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Các model cho cơ chết multitenant
    /// </summary>
    public class CataLog : BaseEntity
    {
        /// <summary>
        /// TenantID
        /// </summary>
        public Guid? TenantID { get; set; }

        /// <summary>
        /// TenantServerID
        /// </summary>
        public int TenantServerID { get; set; }

        /// <summary>
        /// Weigh
        /// </summary>
        public int Weigh { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

    }
    public class TenentServer : BaseEntity
    {
        public int TenentServerID { get; set; }
        public string Server { get; set; }
        public int? Port { get; set; }
        public string DatabaseSouce { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string CharSet { get; set; }
        public int? MaxWeight { get; set; }
        public int? Status { get; set; }
        public int? SumTenantWeight { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }



    public class TenantConnectionString
    {
        public string connectionString { get; set; }
    }
    // Thêm theo IOption 
    public class ConnectServer : TenentServer
    {
        public Guid? TenantID { get; set; }

    }
}
