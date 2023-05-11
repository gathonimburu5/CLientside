using EmployeeClient.Models.Domain;

namespace EmployeeClient.Services.Implementation
{
    public interface IReceivedService
    {
        GoodReceivedHeader CreateGoodReceived(GoodReceivedHeader header);
        GoodReceivedHeader GetGoodreceivedById(int id);
        List<GoodReceivedHeader> GetAllGoodreceived();
    }
}
