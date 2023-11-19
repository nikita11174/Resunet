using System.Transactions;

namespace Resutest.Helpers
{
    static public class Helper
    {
        static public TransactionScope CreateTransactionScope(int seconds = 1)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
