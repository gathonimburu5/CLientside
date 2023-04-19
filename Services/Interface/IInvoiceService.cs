using EmployeeClient.Models.Domain;

namespace EmployeeClient.Services.Interface
{
    public interface IInvoiceService
    {
        Invoice CreateInvoice(Invoice invoice);
        Invoice UpdateInvoice(Invoice invoice);
        Invoice GetInvoiceById(int id);
        bool DeleteInvoice(int id);
        List<Invoice> GetAllInvoices();
        string InvoiceCodes();
    }
}
