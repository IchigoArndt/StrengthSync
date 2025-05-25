import 'package:meu_primeiro_app/core/features/login/domain/entities/UserAuthentication.dart';

abstract class IAuthenticationService {
  Future<bool> login(UserAuthentication userAuthentication);
}