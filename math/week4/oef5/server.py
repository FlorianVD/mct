import socket

addr = (socket.gethostname(), 6969)
server = socket.create_server(addr)