using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using API_Structure.X_DAL.DatabaseUtility.Interface;


namespace API_Structure.X_DAL.DatabaseUtility.Services
{
    [Authorize]
    public class CommonFunctionServices : ICommonFunctionServices
    {
        private IHttpContextAccessor _httpContextAccessor;
        public CommonFunctionServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        public long GetUserID()
        {
            var identity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            return Convert.ToInt64(identity.Claims.Where(c => c.Type == "UserID")
                  .Select(c => c.Value).SingleOrDefault());
        }

        public string GetClaim(string key)
        {
            var identity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            string data = identity.Claims.Where(c => c.Type == key)
                  .Select(c => c.Value).SingleOrDefault();
            if (data == null)
            {
                data = string.Empty;
            }
            return data;
        }



        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }



    }
}