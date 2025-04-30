namespace PatientService.Infra.Messaging
{
	public interface IMessagePublisher
	{
		Task PublishAsync<T>(T message) where T : class;
	}
}
