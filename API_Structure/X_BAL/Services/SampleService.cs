using API_Structure.X_DAL.Providers.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Data;
using System.Diagnostics;
using API_Structure.X_BAL.DomainModels.Models;
using static API_Structure.Constants.Constants;
using System.Data.SqlClient;
using API_Structure.X_BAL.Services.Interface;

namespace API_Structure.X_BAL.Services
{
    public class SampleService:ISampleService
    {
        public JsonResponse Sample(int ID)
        {
            JsonResponse jsonResponse = new JsonResponse();
            try
            {
                SqlParameter[] objParam = new SqlParameter[1];
                objParam[0] = new SqlParameter("@ID", ID);
                DataSet dataSet = new ADODataFunction().ExecuteDataset("SP_FirstProcedure", objParam);
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    DataTable dataTable = dataSet.Tables[0];
                    if (dataTable.Rows.Count > 0)
                    {
                        jsonResponse.Status = dataTable.Rows[0][ProcedureColumnName.Status].ToString();
                        jsonResponse.Message = dataTable.Rows[0][ProcedureColumnName.Message].ToString();
                    }
                    if(dataSet.Tables.Count > 1) 
                    { 
                        if (dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0)
                        {
                            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                            Dictionary<string, object> row;
                            foreach (DataRow dr in dataSet.Tables[1].Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dataSet.Tables[1].Columns)
                                {
                                    row.Add(col.ColumnName, dr[col]);
                                }
                                rows.Add(row);
                            }
                            jsonResponse.Data = rows;
                        }
                    }
                    else
                    {
                        jsonResponse.Status = ResponseStatus.Failed;
                        jsonResponse.Message = ResponseMessages.ServerError;
                    }
                }
                else
                {
                    jsonResponse.Status = ResponseStatus.Failed;
                    jsonResponse.Message = ResponseMessages.ServerError;
                }
            }
            catch (Exception ex)
            {
                jsonResponse.Status = ResponseStatus.Failed;
                jsonResponse.Message = ResponseMessages.ServerError;
                //StackTrace CallStack = new StackTrace(ex, true);
                //ex.Data["ErrDescription"] = ex.Message ?? string.Format("Error captured in {0} on Line No {1} of Method {2}", CallStack.GetFrame(0).GetFileName(), CallStack.GetFrame(0).GetFileLineNumber(), CallStack.GetFrame(0).GetMethod().ToString());
                //new ADODataFunction().SaveErrorLog("SampleService", "Sample", Convert.ToString(ex.Data["ErrDescription"]), Convert.ToString(ex), CallStack.GetFrame(CallStack.FrameCount - 1).GetFileLineNumber());
            }
            return jsonResponse;
        }
    }
}
