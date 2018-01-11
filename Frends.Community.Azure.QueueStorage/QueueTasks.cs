using System;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable 1591

namespace Frends.Community.Azure.QueueStorage
{
    public class QueueTasks
    {
        /// <summary>
        /// Create a new QueueStorage.
        /// Queue name must start with a letter or number, and can contain only letters, numbers, and the dash (-) character. See https://docs.microsoft.com/en-us/rest/api/storageservices/naming-queues-and-metadata for details.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Object { bool Success, string Info }</returns>
        public static async Task<QueueOperationResult> CreateQueueAsync(QueueConnectionProperties connection, Options options, CancellationToken cancellationToken)
        {
            try
            {
                // check for cancellation
                cancellationToken.ThrowIfCancellationRequested();

                var queue = Utils.GetQueueReference(connection);

                // check for cancellation
                cancellationToken.ThrowIfCancellationRequested();

                // create queue if it does not exist
                if (await queue.CreateIfNotExistsAsync())
                    return new QueueOperationResult { Success = true, Info = $"Queue '{connection.QueueName}' created." };
                else
                    return new QueueOperationResult { Success = false, Info = $"Queue named '{connection.QueueName}' already exists." };

            }
            catch (Exception ex)
            {
                if (options.ThrowErrorOnFailure)
                    throw ex;
                return new QueueOperationResult { Success = false, Info = ex.Message };
            }
        }

        /// <summary>
        /// Delete a existing QueueStorage
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Object { bool Success, string Info }</returns>
        public static async Task<QueueOperationResult> DeleteQueueAsync(QueueConnectionProperties connection, Options options, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var queue = Utils.GetQueueReference(connection);
                
                // check for cancellation
                cancellationToken.ThrowIfCancellationRequested();

                // delete queue if exists
                if (await queue.DeleteIfExistsAsync())
                    return new QueueOperationResult { Success = true, Info = $"Queue '{connection.QueueName}' deleted." };
                else
                    return new QueueOperationResult { Success = false, Info = $"Queue '{connection.QueueName}' not found." };

            }
            catch (Exception ex)
            {
                if (options.ThrowErrorOnFailure)
                    throw ex;
                return new QueueOperationResult { Success = false, Info = ex.Message };
            }
        }

        /// <summary>
        /// Gets an estimate number of messages in a queue
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Object { bool Success, string Info, int Count }</returns>
        public static async Task<QueueGetLengthResult> GetQueueLengthAsync(QueueConnectionProperties connection, Options options, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var queue = Utils.GetQueueReference(connection);
                // check for cancellation
                cancellationToken.ThrowIfCancellationRequested();

                await queue.FetchAttributesAsync();

                cancellationToken.ThrowIfCancellationRequested();

                int? cachedMessageCount = queue.ApproximateMessageCount;

                return new QueueGetLengthResult { Success = true, Count = cachedMessageCount == null ? 0 : (int)cachedMessageCount };

            }catch(Exception ex)
            {
                if (options.ThrowErrorOnFailure)
                    throw ex;
                return new QueueGetLengthResult { Success = false, Info = ex.Message };
            }
        }
    }
}
