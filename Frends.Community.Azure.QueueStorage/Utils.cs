using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

#pragma warning disable 1591

namespace Frends.Community.Azure.QueueStorage
{
    public static class Utils
    {
        public static CloudQueue GetQueueReference(QueueConnectionProperties connection)
        {
            // Parse the connection string and return a reference to the storage account.
            var storageAccount = CloudStorageAccount.Parse(connection.StorageConnectionString);
            // create service client
            var queueClient = storageAccount.CreateCloudQueueClient();
            // Retrieve a reference to a container.
            var queue = queueClient.GetQueueReference(connection.QueueName);

            return queue;
        }
    }
}
