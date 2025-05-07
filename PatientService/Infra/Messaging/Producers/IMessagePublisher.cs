namespace PatientService.Infra.Messaging.Producers
{
	public interface IMessagePublisher
	{
		Task PublishAsync<T>(T message) where T : class;
	}
}