using System.ComponentModel.DataAnnotations;

namespace EmployeeClient.Models.Domain
{
    public class MeasureUnit
    {
        public int MeasureUnitId { get; set; }
        [Display(Name = "Measure Unit Name")]
        public string MeasureUnitName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
