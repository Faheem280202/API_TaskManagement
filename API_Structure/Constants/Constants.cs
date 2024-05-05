namespace API_Structure.Constants
{
    public class Constants
    {
        public class Procedures
        {
            public const string ErrorLogs = "Error";
            public const string GetID = "SP_GetID";
        }

        public class AESKeys
        {
            public static string AES256EncryptString = "WHH4nv43Let4huxP6vBqoabnE7JkpibkMf6wCGRPJBC=";
            public static string AES256IVString = "bfcvJCbmwS0qaQRmamEyJg==";
        }

        public class ProcedureColumnName
        {
            public const string Status = "STATUS";
            public const string Message = "MESSAGE";
            public const string DownloadUrl = "DownloadURL";
            public const string ID = "ID";
        }

        public class ResponseStatus
        {
            public const string Success = "S";
            public const string Failed = "F";
            public const string Reset = "R";
            public const string Pending = "P";
        }
        public class ResponseMessages
        {
            public const string Success = "Success";
            public const string Failed = "Failed";
            public const string ServerError = "Something Went Wrong";
            public const string NoDataAvailable = "No Data Available";
            public const string InvalidPDF = "Invalid PDF File, Please upload a valid PDF File";
            public const string InvalidRequest = "Invalid Request";
            public const string PANRequird = "PAN Number is Requird";
            public const string PasswordRequired = "Password is Required";
        }
    }
}
