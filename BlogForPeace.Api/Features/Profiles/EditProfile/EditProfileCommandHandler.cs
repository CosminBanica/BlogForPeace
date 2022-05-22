using BlogForPeace.Core.Domain.UserComments;
using System.Net;
using BlogForPeace.Api.Web;
using MediatR;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;

namespace BlogForPeace.Api.Features.Profiles.EditProfile
{
    public class EditProfileCommandHandler : IEditProfileCommandHandler 
    {
        private readonly IUserCommentsRepository userCommentsRepository;
        private readonly IMediator mediator;
        private IManagedMqttClient mqttClient;

        public EditProfileCommandHandler(IUserCommentsRepository _userCommentsRepository, IMediator _mediator)
        {
            userCommentsRepository = _userCommentsRepository;
            this.mediator = _mediator;
            this.mqttClient = new MqttFactory().CreateManagedMqttClient();
        }

        public async Task HandleAsync(EditProfileCommand command, string identityId, CancellationToken cancellationToken)
        {
            var user = await userCommentsRepository.GetByIdentityAsync(identityId, cancellationToken) as UsersCommentsDomain;

            if (user == null)
            {
                throw new ApiException(HttpStatusCode.Unauthorized, $"User with identity {identityId} does not have a registered profile");
            }

            user.RemoveTags();
            await MqttUnsubscribeAllAsync(command.Email);
            foreach (var tag in command.Tags)
            {
                var userAddedTagEvent = user.AddTag(tag.Name, tag.Description);
                await MqttSubscribeAsync("blog/" + tag.Name, command.Email);
            }

            user.UpdateProfile(command.Email, command.Name, command.Address);

            await userCommentsRepository.SaveAsync(cancellationToken);
        }

        private async Task MqttConnectAsync()
        {
            string clientId = Guid.NewGuid().ToString();
            string mqttURI = "mosquitto";
            int mqttPort = 1883;
            bool mqttSecure = false;

            var messageBuilder = new MqttClientOptionsBuilder()
            .WithClientId(clientId)
            .WithTcpServer(mqttURI, mqttPort)
            .WithCleanSession();

            var options = mqttSecure
              ? messageBuilder
                .WithTls()
                .Build()
              : messageBuilder
                .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
              .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
              .WithClientOptions(options)
              .Build();

            mqttClient = new MqttFactory().CreateManagedMqttClient();

            await mqttClient.StartAsync(managedOptions);
        }

        private async Task MqttSubscribeAsync(string topic, string email, int qos = 1)
        {
            if (!mqttClient.IsStarted)
            {
                await MqttConnectAsync();
            }
            await MqttPublishAsync(topic, email);
        }

        private async Task MqttUnsubscribeAllAsync(string email)
        {
            if (!mqttClient.IsStarted)
            {
                await MqttConnectAsync();
            }
            await mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
                .WithTopic("unsubscribers")
                .WithPayload("email:" + email)
                .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)1)
                .WithRetainFlag(true)
                .Build());
        }

        private async Task MqttPublishAsync(string topic, string email, bool retainFlag = true, int qos = 1)
        {
            if (!mqttClient.IsStarted)
            {
                await MqttConnectAsync();
            }
            await mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
                .WithTopic("subscribers")
                .WithPayload("email:" + email + ", topic:" + topic)
                .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)qos)
                .WithRetainFlag(retainFlag)
                .Build());
        }
    }
}
