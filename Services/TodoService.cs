using Microsoft.Azure.Cosmos;
using Todo_API.Models;

namespace Todo_API.Services
{
    public class TodoService : ITodoService
    {
        private readonly Container _container;
        
        public TodoService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<TaskModel> Add(TaskModel task)
        {
            var item = await _container.CreateItemAsync<TaskModel>(task, new PartitionKey(task.Id));
            return item;
        }

        public async Task Delete(string id, string partition)
        {
            await _container.DeleteItemAsync<TaskModel>(id, new PartitionKey(partition));
        }

        public async Task<List<TaskModel>> Get(string cosmosQuery)
        {
            var query = _container.GetItemQueryIterator<TaskModel>(new QueryDefinition(cosmosQuery));


            List<TaskModel> results = new List<TaskModel>();

            while(query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        public async Task<TaskModel> Update(TaskModel task)
        {
            var item = await _container.UpsertItemAsync<TaskModel>(task, new PartitionKey(task.Id));

            return item;
        }
    }
}
