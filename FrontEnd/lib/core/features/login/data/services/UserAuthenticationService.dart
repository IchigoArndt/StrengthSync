import 'dart:io';
import 'package:dio/dio.dart';
import 'package:dio/io.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

import 'package:meu_primeiro_app/core/features/login/data/services/interceptors/LoggingInterceptor.dart';
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
  final Dio _dio = Dio(BaseOptions(
    baseUrl: 'https://10.0.2.2:8666',
    connectTimeout: Duration(seconds: 5),
    receiveTimeout: Duration(seconds: 4),
  ));

  final storage = FlutterSecureStorage();

  UserAuthenticationService() {
    _dio.httpClientAdapter = CustomHttpAdapter();
  }

  @override
  Future<bool> login(UserAuthentication userAuthentication) async {
    /*TODO: Melhorar a forma desse log, talvez adicionar no mongo ? ou em outro lugar*/
    _dio.interceptors.add(LoggingInterceptor()); //Adiciona um interceptor de log
    try{
      Response response = await _dio.post(
        '/User/Login',
        data: {
          'username': userAuthentication.username,
          'password': userAuthentication.password
        },
      );

      if (response.statusCode == 200){
        await storage.write(key: 'auth_token', value: response.data);
        return true;
      }
      else
        return false;

    } catch (erro)
    {
      print(erro);
      return false;
    }

  }
}
