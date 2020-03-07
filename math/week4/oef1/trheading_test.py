from threading import Thread
import threading
import random
import logging
import time

logging.basicConfig(level=logging.DEBUG)

threadDictionary = {}
count = 0

listLock = threading.Lock()

def generateRandom():
    global count
    name = threading.current_thread().getName()
    while count < 20:
        listLock.acquire()
        value = random.randint(1, 20)
        logging.info(f'{name} koos {value}')
        if name not in threadDictionary.keys():
            threadDictionary[name] = [value]
        else:
            threadDictionary[name].append(value)
        count += 1
        listLock.release()
        time.sleep(random.randint(1, 2))


thread1 = Thread(target=generateRandom, name=f"t-01")
thread2 = Thread(target=generateRandom, name=f"t-02")

thread1.start()
thread2.start()

thread1.join()
thread2.join()

print(threadDictionary)
for thread in threadDictionary.keys():
    print(f'{thread} -> {len(threadDictionary[thread])}')