import 'package:flutter/material.dart';

class HomePage extends StatefulWidget {
  const HomePage({super.key});

  @override
  State<HomePage> createState() => _homePage();
}

class _homePage extends State<HomePage> {
  String nomeUsuario = 'Nome do Usuário';

  @override
  Widget build(BuildContext context) {
    return PopScope(
      canPop: false, // Impede que a tela seja fechada automaticamente
      onPopInvokedWithResult: (bool didPop, Object? result) async {
        if (didPop) return; // Se já foi fechado, não faz nada

        bool shouldExit = await showDialog(
          context: context,
          builder:
              (context) => AlertDialog(
                title: Text("Hora de descansar ou de mais uma série? 💪"),
                content: Text(
                  "Antes de sair, tem certeza que quer encerrar o treino?",
                ),
                actions: [
                  TextButton(
                    onPressed: () => Navigator.of(context).pop(false),
                    // Cancela saída
                    child: Text("Cancelar"),
                  ),
                  TextButton(
                    onPressed: () => Navigator.of(context).pop(true),
                    // Confirma saída
                    child: Text("Sair"),
                  ),
                ],
              ),
        );

        if (shouldExit ?? false) {
          Navigator.of(context).pop(); // Fecha a tela
        }
      },
      child: Scaffold(
        appBar: AppBar(title: const Text('Exemplo')),
        body: Center(
          child: ListTile(
            leading: const Icon(Icons.account_circle), // Ícone
            title: Text(nomeUsuario), // Nome do Usuário
          ),
        ),
      ),
    );
  }
}
