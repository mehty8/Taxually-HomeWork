namespace Taxually.TechnicalTest.Service.Client;

public interface ITaxuallyHttpClient
{
    Task PostAsync<TRequest>(string url, TRequest request);
}