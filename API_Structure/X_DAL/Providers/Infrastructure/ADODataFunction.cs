using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using API_Structure.X_BAL.DomainModels.Models;
using System.Collections.Generic;
using API_Structure.X_DAL.Providers.Infrastructure;
using System.Diagnostics;

namespace API_Structure.X_DAL.Providers.Infrastructure
{
    public class ADODataFunction : Disposable
    {
        protected override void DisposeCore()
        {



        }
        public ADODataFunction()
        {

        }

        private readonly IConfiguration _configuration;
        public ADODataFunction(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public enum DbContext
        {
            DBContext
        }

        public DataSet ExecuteDataset(string CommandText, SqlParameter[] SqlParameters, CommandType Type = CommandType.StoredProcedure)
        {
            try
            {
                string ConnectionStringSSON = string.Empty;
                var configurationBuilder = new ConfigurationBuilder();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);
                var root = configurationBuilder.Build();
                ConnectionStringSSON = root.GetConnectionString("DBContext");
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(ConnectionStringSSON))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(CommandText, con))
                    {
                        da.SelectCommand.CommandType = Type;
                        da.SelectCommand.CommandTimeout = 0;
                        if (SqlParameters != null)
                        {
                            da.SelectCommand.Parameters.AddRange(SqlParameters);
                        }
                        da.Fill(ds);
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                StackTrace CallStack = new StackTrace(ex, true);
                ex.Data["ErrDescription"] = ex.Message ?? string.Format("Error captured in {0} on Line No {1} of Method {2}", CallStack.GetFrame(0).GetFileName(), CallStack.GetFrame(0).GetFileLineNumber(), CallStack.GetFrame(0).GetMethod().ToString());
                SaveErrorLog("ADODataFunction", "ExecuteDataset", Convert.ToString(ex.Data["ErrDescription"]), Convert.ToString(ex), CallStack.GetFrame(CallStack.FrameCount - 1).GetFileLineNumber());
                throw ex;
            }
        }

        public void SaveErrorLog(string controller, string method, string? message, string? errorTrace, int? errorLine = null)
        {
            try
            {
                JsonResponse response = new JsonResponse();
                var objParams = new SqlParameter[] {
                    new SqlParameter("@Controller", controller),
                    new SqlParameter("@Method", method),
                    new SqlParameter("@Message", message),
                    new SqlParameter("@ErrorTrace", errorTrace),
                };
                DataSet data = ExecuteDataset("here add your procedure name", objParams, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}