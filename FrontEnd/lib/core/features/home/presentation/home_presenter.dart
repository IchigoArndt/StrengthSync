import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:meu_primeiro_app/core/features/home/presentation/widget/calendar/calendar_widget.dart';
import 'package:meu_primeiro_app/core/features/home/presentation/widget/userCard/userCard_widget.dart';

class HomePage extends StatefulWidget {
  const HomePage({super.key});

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  String nomeUsuario = 'Nome do Usuário';

  @override
  Widget build(BuildContext context) {
    return PopScope(
      canPop: false,
      onPopInvokedWithResult: (bool didPop, Object? result) async {
        if (didPop) return;
        bool shouldExit = await showDialog(
          context: context,
          builder: (context) => _showingExitDialog(),
        );
        if (shouldExit ?? false) {
          SystemNavigator.pop();
        }
      },
      child: Scaffold(
        body: CustomScrollView(
          slivers: [
            SliverPadding(
              padding: EdgeInsets.symmetric(vertical: 40),
              sliver: SliverToBoxAdapter( // Substitui SliverList por SliverToBoxAdapter
                child: Container(
                  alignment: Alignment.center,
                  child: SizedBox(
                    width: MediaQuery.of(context).size.width * 0.9,
                    child: UserCardWidget(nomeUsuario: "Teste", trainingDay: "Superiores"),
                  ),
                ),
              ),
            ),
            SliverToBoxAdapter(
              child: Container(
                alignment: Alignment.center,
                child: SizedBox(
                  width: MediaQuery.of(context).size.width * 0.9,
                  child: CalendarWidget(),
                ),
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
      content: Text("Antes de sair, tem certeza que quer encerrar o treino?"),
      actions: [
        TextButton(
          onPressed: () => Navigator.of(context).pop(false),
          child: Text("Cancelar"),
        ),
        TextButton(
          onPressed: () => Navigator.of(context).pop(true),
          child: Text("Sair"),
        ),
      ],
    );
  }
}
