import socket

addr = (socket.gethostname(), 6969)
client = socket.create_connection(addr)