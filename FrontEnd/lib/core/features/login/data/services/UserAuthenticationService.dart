import 'package:meu_primeiro_app/core/features/login/domain/services/IUserAuthenticationSerivce.dart';
import 'package:meu_primeiro_app/core/features/login/domain/entities/UserAuthentication.dart';

class userAuthenticationService implements IAuthenticationService{
  @override
  Future<bool> login(UserAuthentication userAuthentication) async{

    if ((userAuthentication.username.isNotEmpty && userAuthentication.username == "luiz") &&
        (userAuthentication.password.isNotEmpty && userAuthentication.password == "1234")){
      return true; //Aqui será implementado no futuro distante uma comunicação via api
    }

    return false;
  }
}