using System.ComponentModel;

#pragma warning disable 1591

namespace Frends.Community.Azure.QueueStorage
{
    /// <summary>
    /// Properties for Azure Storage Connection
    /// </summary>
    public class QueueConnectionProperties
    {
        /// <summary>
        /// Connection String to Azure Storage
        /// </summary>
        [DefaultValue("\"UseDevelopmentStorage=true;\"")]
        public string StorageConnectionString { get; set; }

        /// <summary>
        /// Queue name must start with a letter or number, and can contain only letters, numbers, and the dash (-) character.
        /// </summary>
        public string QueueName { get; set; }
    }

    public class MessageProperties
    {
        /// <summary>
        /// Message content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// True: creates queue if it does not exist
        /// False: throws error if queue does not exist
        /// </summary>
        [DefaultValue(true)]
        public bool CreateQueue { get; set; }
    }

    public class Options
    {
        /// <summary>
        /// Choose if Exception should be thrown if error occurs, otherwise returns Object { Success = false, Message = 'Error information' }
        /// </summary>
        [DefaultValue(true)]
        public bool ThrowErrorOnFailure { get; set; }
    }

    public class QueueOperationResult
    {
        public bool Success { get; set; }
        public string Info { get; set; }
    }
    public class QueueGetLengthResult
    {
        public bool Success { get; set; }
        public string Info { get; set; }
        public int Count { get; set; }
    }
    public class QueuePeekMessageResult
    {
        public bool Success { get; set; }
        public string Info { get; set; }
        public string Content { get; set; }
    }
}
