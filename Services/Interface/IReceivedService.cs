using EmployeeClient.Models.Domain;

namespace EmployeeClient.Services.Interface
{
    public interface IReceivedService
    {
        GoodReceivedHeader CreateGoodReceived(GoodReceivedHeader header);
        GoodReceivedHeader GetGoodreceivedById(int id);
        List<GoodReceivedHeader> GetAllGoodreceived();
    }
}
