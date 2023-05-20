using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeClient.Models.Domain;

namespace EmployeeClient.Services.Interface
{
    public interface IHeaderInvoiceService
    {
        PInvoiceHeader CreateHeader(PInvoiceHeader model);
        PInvoiceHeader UpdateHeader(PInvoiceHeader model);
        bool DeleteHeader(int id);
        PInvoiceHeader GetHeaderById(int id);
        List<PInvoiceHeader> GetAllHeaderList();
        string GenerateInvoiceCode();
        GoodReceivedHeader CreateGood(GoodReceivedHeader header);
    }
}