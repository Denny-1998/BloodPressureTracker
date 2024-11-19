import 'package:dart_amqp/dart_amqp.dart';
import 'package:stomp_dart_client/stomp_dart_client.dart';
import 'package:web_socket_channel/web_socket_channel.dart';

class RabbitmqService {
  late WebSocketChannel _channel;
  late StompClient _client;
  late String _message;

  Future<void> connect(String message) async {
    _message = message;
    _client = StompClient(
        config: StompConfig(
            url: 'ws://localhost:15674/ws', onConnect: onConnectCallback));

    _client.activate();
  }

  void onConnectCallback(StompFrame connectFrame) {
    print("stomp client is connected.");

    _client.send(
        destination: '/exchange/measurementExchange/POST',
        body: _message,
        headers: {
          'auto_delete': 'false',
          'durable': 'false',
          'exclusive': 'false',
        });
    print("message was sent");
    _client.deactivate();
  }
}
