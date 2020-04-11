WebApi app that only listens to websocket requests. Receives messages and sends updates to all connected websockets.

Improvements:
    Split out StateManager, SocketManager, MessageProcessor
    Use builtin DI