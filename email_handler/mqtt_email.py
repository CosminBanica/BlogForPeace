import paho.mqtt.client as mqtt
import sys
import smtplib
from email.mime.text import MIMEText

subscribers = {}

def on_connect(client, userdata, flags, rc):
    print("Connected with result code "+str(rc))
    client.subscribe("#")

def on_message(client, userdata, msg):
    print(msg.topic+" "+str(msg.payload.decode("utf-8")))
    if msg.topic == "subscribers":
        command = str(msg.payload.decode("utf-8")).split(",")
        email = command[0].split(":")[1]
        topic = command[1].split(":")[1]
        if email in subscribers:
            subscribers[email].append(topic)
        else:
            subscribers[email] = [topic]
    elif msg.topic == "unsubscribers":
        command = str(msg.payload.decode("utf-8"))
        command = command.split(":")
        email = command[1]
        if email in subscribers:
            subscribers[email].clear()
    elif msg.topic != "test":
        command = str(msg.payload.decode("utf-8"))
        topic = msg.topic
        sender = "blogforpeace@peace.com"
        text = msg.payload.decode("utf-8")
        tag = topic.split("/")[1]

        for subscriber in subscribers:
            
            if topic in subscribers[subscriber]:
                receiver = subscriber

                message = MIMEText(text)
                message["Subject"] = f"""New Blog For {tag} Tag"""
                message["From"] = sender
                message["To"] = receiver

                with smtplib.SMTP("smtp.mailtrap.io", 2525) as server:
                    server.login("dd047cad1099b4", "1259b862816c9c")
                    server.sendmail(sender, receiver, message.as_string())
                print(message.as_string())
        
    print(subscribers)


if __name__ == "__main__":
    client = mqtt.Client()
    client.on_connect = on_connect
    client.on_message = on_message
    client.connect("localhost", 1883, 60)
    client.loop_start()
    while True:
        try:
            line = sys.stdin.readline()
            line = line[:-1]
            client.publish("test", line)
        except KeyboardInterrupt:
            print("")
            break