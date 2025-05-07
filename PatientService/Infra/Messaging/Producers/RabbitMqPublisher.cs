using MassTransit;

namespace PatientService.Infra.Messaging.Producers
{
	public class RabbitMqPublisher : IMessagePublisher
	{
		private readonly IPublishEndpoint _publishEndpoint;

		public RabbitMqPublisher(IPublishEndpoint publishEndpoint)
		{
			_publishEndpoint = publishEndpoint;
		}

		public async Task PublishAsync<T>(T message) where T : class
		{
			await _publishEndpoint.Publish(message);
		}
	}
}