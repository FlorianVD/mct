import random
import string
import time

# Importeer Kafka Confluent library
from confluent_kafka import Consumer, KafkaError


# Genereer een random string
def random_generator(size=64, chars=string.ascii_uppercase + string.digits):
    return ''.join(random.choice(chars) for x in range(size))

# Maak een consumer aan
c = Consumer({
    'bootstrap.servers': '192.168.40.11',
    'group.id': random_generator(),
    'auto.offset.reset': 'earliest',
    'enable.auto.commit': 'false'
})

try:
    # Luister naar een bepaald topic
    c.subscribe(['panda'])

    while True:
        # Consume message, time-out van 1 seconde
        msg = c.poll(1.0)

        # Is er een bericht?
        if msg is None:
            continue

        # Is er een fout?
        if msg.error():
            print("Consumer error: {}".format(msg.error()))
            continue

        # Bericht decoderen en weergeven
        print('Received message: {}'.format(msg.value().decode('utf-8')))

        # Even pauzeren, verwijder gerust
        time.sleep(3)

        # Offset comitten
        c.commit()

# Excepties afvangen
except KafkaError as e:
    # Stracktrace printen
    print(e.__str__())

    # Consumer afsluiten
    c.close()
