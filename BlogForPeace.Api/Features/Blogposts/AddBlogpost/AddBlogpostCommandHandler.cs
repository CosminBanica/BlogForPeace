﻿using BlogForPeace.Core.Domain.Blogpost;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;

namespace BlogForPeace.Api.Features.Blogposts.AddBlogpost
{
    public class AddBlogpostCommandHandler : IAddBlogpostCommandHandler
    {
        private readonly IBlogpostRepository blogpostRepository;
        private IManagedMqttClient mqttClient;

        public AddBlogpostCommandHandler(IBlogpostRepository _blogpostRepository)
        {
            this.blogpostRepository = _blogpostRepository;
            this.mqttClient = new MqttFactory().CreateManagedMqttClient();
        }

        public async Task HandleAsync(AddBlogpostCommand command, CancellationToken cancellationToken)
        {
            foreach (var tag in command.Tags)
            {
                await MqttPublishAsync("blog/" + tag.Name, command.Text, true);
            }

            await blogpostRepository.AddAsync(
                new InsertBlogpostInBlogCommand(command.Title, command.Text, command.Location, command.Tags),
                cancellationToken);
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

        private async Task MqttPublishAsync(string topic, string payload, bool retainFlag = true, int qos = 1)
        {
            if (!mqttClient.IsStarted)
            {
                await MqttConnectAsync();
            }
            await mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)qos)
                .WithRetainFlag(retainFlag)
                .Build());
        }
    }
}
