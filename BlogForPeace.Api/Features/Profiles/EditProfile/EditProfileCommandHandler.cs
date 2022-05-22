﻿using BlogForPeace.Core.Domain.UserComments;
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
            foreach (var tag in command.Tags)
            {
                var userAddedTagEvent = user.AddTag(tag.Name, tag.Description);
            }

            user.UpdateProfile(command.Email, command.Name, command.Address);

            await userCommentsRepository.SaveAsync(cancellationToken);
        }

        public async Task MqttConnectAsync()
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
    }
}
