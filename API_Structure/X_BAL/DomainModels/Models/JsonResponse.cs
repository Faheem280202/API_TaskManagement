namespace API_Structure.X_BAL.DomainModels.Models
{
    public class JsonResponse
    {
        public string Status {  get; set; }
        public string Message { get; set; }
        public string LastUpdatedOn { get; set; }
        public string? URL { get; set; } = string.Empty;

        public int? ID {  get; set; }
        public long? UserID { get; set;}
        public object Data { get; set; }
        public object Table1 { get; set; }
        public object Table2 { get; set; }
        public object Table3 { get; set; }
        public object Table4 { get; set; }
        public object Table5 { get; set; }
        public object Table6 { get; set; }
        public object Table7 { get; set; }
        public object Table8 { get; set; }
        public object Table9 { get; set; }
        public object Table10 { get; set; }
        public object Table11 { get; set; }
        public object Table12 { get; set; }
    }
}
