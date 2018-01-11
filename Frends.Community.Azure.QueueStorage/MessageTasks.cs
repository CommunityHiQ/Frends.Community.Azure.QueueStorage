using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable 1591

namespace Frends.Community.Azure.QueueStorage
{
    public class MessageTasks
    {
        /// <summary>
        /// Inserts a message to Queue.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="message"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Object { bool Success, string Info}</returns>
        public static async Task<QueueOperationResult> InsertMessageAsync(QueueConnectionProperties connection, MessageProperties message, Options options, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var queue = Utils.GetQueueReference(connection);

                // check for cancellation
                cancellationToken.ThrowIfCancellationRequested();

                // create message
                var queueMessage = new CloudQueueMessage(message.Content);

                // create queue if it does not exist
                if (message.CreateQueue)
                    await queue.CreateIfNotExistsAsync();

                await queue.AddMessageAsync(queueMessage);

                return new QueueOperationResult { Success = true, Info = $"Message added to queue '{connection.QueueName}'." };

            }catch(Exception ex)
            {
                if (options.ThrowErrorOnFailure)
                    throw ex;
                return new QueueOperationResult { Success = false, Info = ex.Message };
            }
        }

        /// <summary>
        /// Peeks at the message in the front of a queue and returns its content.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Object { bool Success, string Info, string Content }</returns>
        public static async Task<QueuePeekMessageResult> PeekNextMessageAsync(QueueConnectionProperties connection, Options options, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var queue = Utils.GetQueueReference(connection);

                //check for cancellation
                cancellationToken.ThrowIfCancellationRequested();

                var peekedMessage = await queue.PeekMessageAsync();

                return new QueuePeekMessageResult { Success = true, Content = peekedMessage.AsString };

            }
            catch(Exception ex)
            {
                if (options.ThrowErrorOnFailure)
                    throw ex;
                return new QueuePeekMessageResult { Success = false, Info = ex.Message };
            }
        }

        /// <summary>
        /// Deletes next message in Queue.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Object { bool Success, string Info}</returns>
        public static async Task<QueueOperationResult> DeleteMessageAsync(QueueConnectionProperties connection, Options options, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var queue = Utils.GetQueueReference(connection);

                //check for cancellation
                cancellationToken.ThrowIfCancellationRequested();

                // get message
                var queuedMessage = await queue.GetMessageAsync();

                // check that message was found
                if (queuedMessage != null)
                {
                    await queue.DeleteMessageAsync(queuedMessage);
                    return new QueueOperationResult { Success = true, Info = $"Deleted next message in queue '{connection.QueueName}'" };
                } else
                    return new QueueOperationResult { Success = false, Info = $"Could not delete message: Message not found in queue '{connection.QueueName}'" };


            }
            catch(Exception ex)
            {
                if (options.ThrowErrorOnFailure)
                    throw ex;
                return new QueueOperationResult { Success = false, Info = ex.Message };
            }
        }
    }
}
