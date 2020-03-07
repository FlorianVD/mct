import socket

host = socket.gethostname()
addr = (host, 6969)

server = socket.create_connection(addr)
server.send('bla'.encode('ascii'))

print(server.recv(16).decode('ascii'))

server.close()