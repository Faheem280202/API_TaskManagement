using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Structure.X_BAL.DomainModels
{
    public class Base
    {
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy {  get; set; }
        public DateTime? ModifiedOn {  get; set; }
    }
}
