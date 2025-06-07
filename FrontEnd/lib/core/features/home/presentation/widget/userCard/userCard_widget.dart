import 'package:flutter/material.dart';

class UserCardWidget extends StatefulWidget {
  final String nomeUsuario;
  final String trainingDay;

  const UserCardWidget({required this.nomeUsuario, required this.trainingDay, super.key});

  @override
  _UserCardWidgetState createState() => _UserCardWidgetState();
}

class _UserCardWidgetState extends State<UserCardWidget> {
  late String statusMensagem;

  @override
  void initState() {
    super.initState();
    statusMensagem = "Treino do dia: ${widget.trainingDay}";
  }

  void _alterarMensagem() {
    setState(() {
      statusMensagem = statusMensagem.contains("Treino do dia")
          ? _MotivalMessage(widget.trainingDay)
          : "Treino do dia: ${widget.trainingDay}";
    });
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: _alterarMensagem, // Muda a mensagem ao tocar
      child: Container(
        padding: const EdgeInsets.symmetric(vertical: 0, horizontal: 25),
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(12),
          boxShadow: [
            BoxShadow(
              color: Colors.black.withOpacity(0.1),
              blurRadius: 6,
              offset: Offset(0, 3),
            ),
          ],
        ),
        child: ListTile(
          leading: const Icon(Icons.account_circle, size: 40, color: Colors.blue),
          title: Text(
            widget.nomeUsuario,
            style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
          ),
          subtitle: Text(
            statusMensagem, // Mensagem dinâmica
            style: TextStyle(fontSize: 16, color: Colors.grey),
          ),
        ),
      ),
    );
  }

  String _MotivalMessage(String dayTraining) {

    if(dayTraining == "Superiores")
      return "Cada repetição é um tijolo na construção da sua força";

    return "ABCD";
  }
}