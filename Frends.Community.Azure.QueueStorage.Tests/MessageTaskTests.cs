using Frends.Community.Azure.QueueStorage;
using Microsoft.WindowsAzure.Storage.Queue;
using NUnit.Framework;
using System.Threading;

namespace Frends.Community.Azure.Queue.Tests
{
    [TestFixture]
    public class MessageTaskTests
    {
        private QueueConnectionProperties _queueConnectionProperties;
        private MessageProperties _messageProperties;
        private Options _options;
        private const string CONNECTIONSTRING = "UseDevelopmentStorage=true;";
        private readonly string _messageQueueName = "hiq-message-test-queue";
        private readonly string _newQueueName = "new-message-test-queue";
        private CloudQueueClient _testClient;


        [SetUp]
        public void InitializeTests()
        {
            _queueConnectionProperties = new QueueConnectionProperties { StorageConnectionString = CONNECTIONSTRING, QueueName = _messageQueueName };
            _messageProperties = new MessageProperties { Content = "Hello Unit Tests", CreateQueue = false };
            _options = new Options { ThrowErrorOnFailure = true };
            _testClient = QueueTestHelpers.GetQueueClient(CONNECTIONSTRING);

            QueueTestHelpers.CreateQueue(_testClient, _messageQueueName);
        }

        [TearDown]
        public void TestTearDown()
        {
            //remove test queues
            QueueTestHelpers.DeleteQueue(_testClient, _messageQueueName);
            QueueTestHelpers.DeleteQueue(_testClient, _newQueueName);
        }

        [Test]
        public void AddMessageToQueue()
        {
            var result = MessageTasks.InsertMessageAsync(_queueConnectionProperties, _messageProperties, _options, new CancellationToken());

            Assert.IsTrue(result.Result.Success);
        }

        [Test]
        public void AddMessageToQueue_CreatesQueue()
        {
            _queueConnectionProperties.QueueName = _newQueueName;
            _messageProperties.CreateQueue = true;
            var result = MessageTasks.InsertMessageAsync(_queueConnectionProperties, _messageProperties, _options, new CancellationToken());

            Assert.IsTrue(result.Result.Success);
            Assert.IsTrue(QueueTestHelpers.QueueExists(_testClient, _newQueueName));
        }


        [Test]
        public void PeekNextMessage()
        {
            QueueTestHelpers.AddMessagesToQueue(_testClient, _messageQueueName, 2);

            var result = MessageTasks.PeekNextMessageAsync(_queueConnectionProperties, _options, new CancellationToken());

            Assert.IsTrue(result.Result.Success);
            Assert.AreEqual("Test message number 0", result.Result.Content);
        }

        [Test]
        public void DeleteMessage()
        {
            QueueTestHelpers.AddMessagesToQueue(_testClient, _messageQueueName, 1);

            var result = MessageTasks.DeleteMessageAsync(_queueConnectionProperties, _options, new CancellationToken());

            Assert.IsTrue(result.Result.Success);
        }

        [Test]
        public void DeleteMessage_IfQueueEmpty_ReturnsFalse()
        {
            var result = MessageTasks.DeleteMessageAsync(_queueConnectionProperties, _options, new CancellationToken());

            Assert.IsFalse(result.Result.Success);
        }
    }
}
