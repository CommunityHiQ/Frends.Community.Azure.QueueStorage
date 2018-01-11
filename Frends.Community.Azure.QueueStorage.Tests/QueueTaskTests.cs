using NUnit.Framework;
using System.Threading;
using Microsoft.WindowsAzure.Storage.Queue;
using Frends.Community.Azure.Queue.Tests;

namespace Frends.Community.Azure.QueueStorage.Tests
{
    [TestFixture]
    public class QueueTaskTests
    {
        private QueueConnectionProperties _queueConnectionProperties;
        private Options _options;
        private const string CONNECTIONSTRING = "UseDevelopmentStorage=true;";
        private readonly string _createQueueName = "hiq-test-queue";
        private CloudQueueClient _testClient;


        [SetUp]
        public void InitializeTests()
        {
            _queueConnectionProperties = new QueueConnectionProperties { StorageConnectionString = CONNECTIONSTRING , QueueName = _createQueueName};
            _options = new Options { ThrowErrorOnFailure = true };
            _testClient = QueueTestHelpers.GetQueueClient(CONNECTIONSTRING);

        }

        [TearDown]
        public void TestTearDown()
        {
            //remove test queues
            QueueTestHelpers.DeleteQueue(_testClient, _createQueueName);
        }

        

        [Test]
        public void CreateQueueTest()
        {
            //_queueConnectionProperties.QueueName = _createQueueName;
            var result = QueueTasks.CreateQueueAsync(_queueConnectionProperties, _options, new CancellationToken());

            Assert.IsTrue(result.Result.Success);
            Assert.IsTrue(QueueTestHelpers.QueueExists(_testClient, _createQueueName));
        }

        [Test]
        public void CreateQueue_ReturnsFalseIfQueueExists()
        {
            var resultSuccess = QueueTasks.CreateQueueAsync(_queueConnectionProperties, _options, new CancellationToken());
            var resultFails = QueueTasks.CreateQueueAsync(_queueConnectionProperties, _options, new CancellationToken());

            Assert.IsTrue(resultSuccess.Result.Success);
            Assert.IsFalse(resultFails.Result.Success);
            Assert.IsTrue(QueueTestHelpers.QueueExists(_testClient, _createQueueName));
        }

        [Test]
        public void DeleteQueue()
        {
            // create queue and check that it exists before delete test
            QueueTestHelpers.CreateQueue(_testClient, _createQueueName);
            Assert.IsTrue(QueueTestHelpers.QueueExists(_testClient, _createQueueName));

            var result = QueueTasks.DeleteQueueAsync(_queueConnectionProperties, _options, new CancellationToken());

            Assert.IsTrue(result.Result.Success);
            Assert.IsFalse(QueueTestHelpers.QueueExists(_testClient, _createQueueName));
        }

        [Test]
        public void DeleteQueue_ReturnsFalseIfQueueDoesNotExist()
        {
            _queueConnectionProperties.QueueName = "foobar";
            var result = QueueTasks.DeleteQueueAsync(_queueConnectionProperties, _options, new CancellationToken());

            Assert.IsFalse(result.Result.Success);
        }

        [Test]
        public void GetQueueLength()
        {
            QueueTestHelpers.CreateQueue(_testClient, _createQueueName);
            QueueTestHelpers.AddMessagesToQueue(_testClient, _createQueueName, 5);
            //_queueConnectionProperties.QueueName = _messageQueueName;

            var result = QueueTasks.GetQueueLengthAsync(_queueConnectionProperties, _options, new CancellationToken());

            Assert.IsTrue(result.Result.Success);
            Assert.AreEqual(5, result.Result.Count);

        }
    }
}
