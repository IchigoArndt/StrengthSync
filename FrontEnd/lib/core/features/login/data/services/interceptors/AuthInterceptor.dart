import 'package:dio/dio.dart';

class AuthInterceptor extends Interceptor {
  final String? authToken;
  AuthInterceptor(this.authToken);
  @override
  void onRequest(RequestOptions options, RequestInterceptorHandler handler) {
    if (authToken != null) {
      options.headers["Authorization"] = "Bearer $authToken";
    }
    super.onRequest(options, handler);
  }
  @override
  void onError(DioError err, ErrorInterceptorHandler handler) {
    // Aqui você pode lidar com erros de autenticação (como token expirado)
    super.onError(err, handler);
  }
}