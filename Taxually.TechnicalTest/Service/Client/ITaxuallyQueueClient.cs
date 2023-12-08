namespace Taxually.TechnicalTest.Service.Client;

public interface ITaxuallyQueueClient
{
    Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
}