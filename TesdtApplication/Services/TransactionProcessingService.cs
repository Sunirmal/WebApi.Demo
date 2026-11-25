namespace WebApi.Demo.Services
{
    public interface ITransactionProcessingService
    {
        string ProcessTransaction(string transactionData);
    }
    public class TransactionProcessingService : ITransactionProcessingService   
    {
        private readonly Guid _serviceId;
        public TransactionProcessingService()
        {
            _serviceId = Guid.NewGuid();
            Console.WriteLine($"TransactionProcessingService instance created with ID: {_serviceId}");
        }
        public string ProcessTransaction(string transactionData)
        {
            var transactionId = Guid.NewGuid();
            
            Console.WriteLine($"Processing transaction with ID: {transactionId}");
            // Implement your transaction processing logic here
            return $"Processed transaction: {transactionData}";
        }
    }
}
