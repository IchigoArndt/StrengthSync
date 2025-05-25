import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'package:meu_primeiro_app/core/features/login/presentation/styles/login_input_styles.dart';
import 'package:meu_primeiro_app/core/features/login/presentation/styles/login_snackbar_styles.dart';
import 'package:meu_primeiro_app/core/features/login/data/services/UserAuthenticationService.dart';
import 'package:meu_primeiro_app/core/features/login/domain/entities/UserAuthentication.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({super.key});

  @override
  State<LoginPage> createState() => _loginPage();
}

class _loginPage extends State<LoginPage> {
  String username = "";
  String password = "";

  String pathImage = '../../../';

  late userAuthenticationService authenticationService;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    authenticationService = Provider.of<userAuthenticationService>(context, listen: false);
  }

  void authenticateUser() async{

    UserAuthentication user = UserAuthentication(username: username, password: password);

    bool authentication = await authenticationService.login(user);

    final snackbar = authentication
        ? StyleSnackBar.snackBarSucess
        : StyleSnackBar.snackBarError;

    ScaffoldMessenger.of(context).showSnackBar(snackbar);

    if (authentication)
    {
      //Utilizando o padrão, mas há possibilidade de mudar
      Navigator.pushNamedAndRemoveUntil(
        context,
        '/home', // Nome da tela destino
            (route) => false, // Remove todas as telas anteriores
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
        children: [
          // 🖼️ Imagem de fundo ocupando toda a tela
          Positioned.fill(

            child: Opacity(
              opacity: 0.7,
              child: Image.asset(
                "assets/images/background_login.png",
                fit: BoxFit.cover, // Ajusta a imagem ao tamanho da tela
              ),
            ),
          ),
          // 🔹 Área do login sobreposta à imagem
          Center(
            child: Padding(
              padding: const EdgeInsets.all(16.0),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Text(
                    "Bem Vindo",
                    style: TextStyle(
                      fontSize: 28,
                      fontWeight: FontWeight.bold,
                      color: Colors.white,
                    ),
                  ),
                  SizedBox(height: 20),
                  TextField(
                    onChanged: (value) => setState(() => username = value),
                    decoration: StyleInputs.getStyle("Nome do usuario"),
                  ),
                  SizedBox(height: 10),
                  TextField(
                    onChanged: (value) => setState(() => password = value),
                    decoration: StyleInputs.getStyle("Senha"),
                    obscureText: true,
                  ),
                  SizedBox(height: 20),
                  FractionallySizedBox(
                    widthFactor: 0.9,
                    child: ElevatedButton(
                      onPressed: authenticateUser,
                      style: ElevatedButton.styleFrom(
                        backgroundColor: Colors.blue,
                        foregroundColor: Colors.white,
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(20),
                        ),
                        padding: EdgeInsets.symmetric(
                          horizontal: 40,
                          vertical: 15,
                        ),
                      ),
                      child: Text("Logar", style: TextStyle(fontSize: 16)),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}