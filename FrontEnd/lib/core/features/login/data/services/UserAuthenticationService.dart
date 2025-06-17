import 'dart:io';
import 'package:dio/dio.dart';
import 'package:dio/io.dart';
import 'package:meu_primeiro_app/core/features/login/domain/services/IUserAuthenticationSerivce.dart';
import 'package:meu_primeiro_app/core/features/login/domain/entities/UserAuthentication.dart';


class CustomHttpAdapter extends DefaultHttpClientAdapter {
  CustomHttpAdapter() {
    onHttpClientCreate = (HttpClient client) {
      client.badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
      return client;
    };
  }
}

class UserAuthenticationService implements IAuthenticationService {
  final Dio _dio = Dio();

  UserAuthenticationService() {
    _dio.httpClientAdapter = CustomHttpAdapter();
  }

  @override
  Future<bool> login(UserAuthentication userAuthentication) async {
    Response response = await _dio.post(
      'https://10.0.2.2:8666/User/Login',
      data: {
        'username': userAuthentication.username,
        'password': userAuthentication.password
      },
    );

    return response.statusCode != null && response.statusCode == 200 ? true : false;
  }
}
