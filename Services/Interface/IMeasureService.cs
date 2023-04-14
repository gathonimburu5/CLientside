using EmployeeClient.Models.Domain;

namespace EmployeeClient.Services.Interface
{
    public interface IMeasureService
    {
        MeasureUnit CreateMeasure(MeasureUnit model);
        MeasureUnit UpdateMeasure(MeasureUnit model);
        bool DeleteMeasure(int id);
        MeasureUnit GetMeasureById(int id);
        List<MeasureUnit> GetAllMeasure();
    }
}
