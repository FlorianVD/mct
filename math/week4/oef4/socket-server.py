import socket

host = socket.gethostname()
addr = (host, 6969)
server = socket.create_server(addr)

counter = 0

while True:
    print('Waiting for connections')
    client, addr = server.accept()
    print('Connection from {}'.format(addr))
    
    received = client.recv(16).decode('ascii')
    client.send(f'{counter}: {received}'.encode('ascii'))
    counter += 1