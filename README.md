# Frends.Community.Azure.QueueStorage

FRENDS community task for Azure Queue Storage operations

- [Installing](#installing)
- [Tasks](#tasks)
     - [CreateQueueAsync](#createqueueasync)
     - [DeleteQueueAsync](#deletequeueasync)
     - [GetQueueLengthAsync](#getqueuelengthasync)
     - [InsertMessageAsync](#insertmessageasync)
     - [DeleteMessageAsync](#deletemessageasync)
     - [PeekNextMessageAsync](#peeknextmessageasync)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)

# Installing

You can install the task via FRENDS UI Task View or you can find the nuget package from the following nuget feed
'Insert nuget feed here'

# Tasks

## CreateQueueAsync

Creates a new Queue in QueueStorage.

### Properties

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Storage Connection String | string | Queue Storage Connection string. See [Configure Azure Storage connection strings](https://docs.microsoft.com/en-us/azure/storage/common/storage-configure-connection-string?toc=%2fazure%2fstorage%2fblobs%2ftoc.json) | UseDevelopmentStorage=true; |
| Queue Name | string | Name of the created Queue. See [Naming Queues and Metada](https://docs.microsoft.com/en-us/rest/api/storageservices/naming-queues-and-metadata) for naming conventions. | 'my-new-queue' |
| Throw Error On Failure | bool | If true exception is thrown in case of failure, otherwise returns Object { Success = false} | true |

### Result
| Property | Type | Description | Example |
| ---------------------| ---------------------| ----------------------- | -------- |
| Success | boolean | Task execution result. | true |
| Info | string | Task result message. If Throw Exception on failure is false, contains error message. | 'Queue 'my-queue' created.' |



## DeleteQueueAsync

Deletes a queue in QueueStorage.

### Properties

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Storage Connection String | string | Queue Storage Connection string. See [Configure Azure Storage connection strings](https://docs.microsoft.com/en-us/azure/storage/common/storage-configure-connection-string?toc=%2fazure%2fstorage%2fblobs%2ftoc.json) | UseDevelopmentStorage=true; |
| Queue Name | string | Name of the Queue you wish to delete. | 'delete-this-queue' |
| Throw Error On Failure | bool | If true exception is thrown in case of failure, otherwise returns Object { Success = false} | true |

### Result
| Property | Type | Description | Example |
| ---------------------| ---------------------| ----------------------- | -------- |
| Success | boolean | Task execution result. | true |
| Info | string | Task result message. If Throw Exception on failure is false, contains error message. | 'Queue 'my-queue' deleted.' |

## GetQueueLengthAsync

Gets an estimate number of messages in a queue.

### Properties

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Storage Connection String | string | Queue Storage Connection string. See [Configure Azure Storage connection strings](https://docs.microsoft.com/en-us/azure/storage/common/storage-configure-connection-string?toc=%2fazure%2fstorage%2fblobs%2ftoc.json) | UseDevelopmentStorage=true; |
| Queue Name | string | Name of the Queue which message count is calculated. | 'count-this-queue' |
| Throw Error On Failure | bool | If true exception is thrown in case of failure, otherwise returns Object { Success = false} | true |

### Result
| Property | Type | Description | Example |
| ---------------------| ---------------------| ----------------------- | -------- |
| Success | boolean | Task execution result. | true |
| Info | string | Task result message. If Throw Exception on failure is false, contains error message. |  |
| Count | int | The estimated number of messages in queue. | 4 |

## InsertMessageAsync

Inserts a new message to queue.

### Properties

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Storage Connection String | string | Queue Storage Connection string. See [Configure Azure Storage connection strings](https://docs.microsoft.com/en-us/azure/storage/common/storage-configure-connection-string?toc=%2fazure%2fstorage%2fblobs%2ftoc.json) | UseDevelopmentStorage=true; |
| Queue Name | string | Name of the Queue where message is added. | 'add-message-queue' |
| Content | string | Message content. | 'Hello QueueStorage!' |
| Create Queue | bool | Indicates if Queue should be created in case it does not exist. | true |
| Throw Error On Failure | bool | If true exception is thrown in case of failure, otherwise returns Object { Success = false} | true |

### Result
| Property | Type | Description | Example |
| ---------------------| ---------------------| ----------------------- | -------- |
| Success | boolean | Task execution result. | true |
| Info | string | Task result message. If Throw Exception on failure is false, contains error message. | 'Message added to queue 'my-queue'.' |

## DeleteMessageAsync

Removes next message from queue.

### Properties

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Storage Connection String | string | Queue Storage Connection string. See [Configure Azure Storage connection strings](https://docs.microsoft.com/en-us/azure/storage/common/storage-configure-connection-string?toc=%2fazure%2fstorage%2fblobs%2ftoc.json) | UseDevelopmentStorage=true; |
| Queue Name | string | Name of the Queue from where message is deleted. | 'remove-message-queue' |
| Throw Error On Failure | bool | If true exception is thrown in case of failure, otherwise returns Object { Success = false} | true |

### Result
| Property | Type | Description | Example |
| ---------------------| ---------------------| ----------------------- | -------- |
| Success | boolean | Task execution result. | true |
| Info | string | Task result message. If Throw Exception on failure is false, contains error message. | 'Deleted next message in queue 'my-queue'.' |

## PeekNextMessageAsync

Peeks at the message in front of the queue and returns its content.

### Properties

| Property | Type | Description | Example |
| -------- | -------- | -------- | -------- |
| Storage Connection String | string | Queue Storage Connection string. See [Configure Azure Storage connection strings](https://docs.microsoft.com/en-us/azure/storage/common/storage-configure-connection-string?toc=%2fazure%2fstorage%2fblobs%2ftoc.json) | UseDevelopmentStorage=true; |
| Queue Name | string | Name of the Queue from where message is peeked. | 'peek-message-queue' |
| Throw Error On Failure | bool | If true exception is thrown in case of failure, otherwise returns Object { Success = false} | true |

### Result
| Property | Type | Description | Example |
| ---------------------| ---------------------| ----------------------- | -------- |
| Success | boolean | Task execution result. | true |
| Info | string | If Task fails, contains error information |  |
| Content | string | Content of the message in front of the queue | 'some content.' |

# Building
You need a valid licence of Rebex.Net

Clone a copy of the repo

`git clone https://github.com/CommunityHiQ/Frends.Community.Azure.QueueStorage.git`

Restore dependencies

`nuget restore frends.community.sftp`

Rebuild the project

Run Tests with nunit3. Tests can be found under

`Frends.Community.Sftp.Tests\bin\Release\Frends.Community.Azure.QueueStorage.Tests.dll`

Create a nuget package

`nuget pack nuspec/Frends.Community.Azure.QueueStorage.nuspec`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Change Log

| Version | Changes |
| ----- | ----- |
| 1.0.0 | Initial version of Azure QueueStorage tasks |