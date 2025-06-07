import 'package:flutter/material.dart';
import 'package:meu_primeiro_app/core/features/home/presentation/widget/calendar/calendar_widget.dart';
import 'package:meu_primeiro_app/core/features/home/presentation/widget/userCard/userCard_widget.dart';

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
          builder: (context) => _showingExitDialog(),
        );

        if (shouldExit ?? false) {
          Navigator.of(context).pop(); // Fecha a tela
        }
      },
      child: Scaffold(
        body: CustomScrollView(
          slivers: [
            SliverPadding(
              padding: EdgeInsets.symmetric(vertical: 40),
              sliver: SliverList(
                delegate: SliverChildListDelegate([
                  Align(
                    alignment: Alignment.center,
                    child: SizedBox(
                      width: MediaQuery.of(context).size.width * 0.9, // Define a largura máxima
                      child: UserCardWidget(nomeUsuario: "Teste"),
                    ),
                  ),
                  Align(
                    alignment: Alignment.center,
                    child: Padding(
                      padding: EdgeInsets.all(16),
                      child: SizedBox(
                          width: MediaQuery.of(context).size.width * 0.9, // Define a largura máxima
                          child: CalendarWidget(),
                    )
                    ),
                  ),
                ]),
              ),
            ),
          ],
        ),
      ),
    );
  }

  AlertDialog _showingExitDialog() {
    return AlertDialog(
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
    );
  }
}
