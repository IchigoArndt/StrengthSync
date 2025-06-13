import 'package:flutter/material.dart';

class DividedBoxWidget extends StatelessWidget {
  final double height;
  final int daysFailed;
  final int daysSucessful;

  const DividedBoxWidget({
    super.key,
    this.height = 100, // Altura padrão,
    this.daysFailed = 0,
    this.daysSucessful = 0
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      height: height,
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(12), // Bordas arredondadas
        boxShadow: [
          BoxShadow(
            color: Colors.black.withOpacity(0.1),
            blurRadius: 6,
            offset: Offset(0, 3),
          ),
        ],
      ),
      child: Row(
        children: [
          Expanded(
            child: Container(
              alignment: Alignment.topCenter,
              padding: EdgeInsets.symmetric(vertical: 10,horizontal: 5),
              child: Column(
                children: [
                  Row(
                    children: [
                      Text(
                        "😊", // Emoji
                        style: TextStyle(fontSize: 25),
                      ),
                      SizedBox(width: 5), // Espaço entre emoji e texto
                      Expanded(
                        child: Text(
                          "Treinos com sucesso", // Mensagem ao lado do emoji
                          style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                        ),
                      ),
                    ],
                  ),
                  SizedBox(height: 50), // Espaço entre as linhas
                  Center( // Garante centralização total
                    child: Text(
                      _getSucessfulDaysCount(),
                      style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
                    ),
                  ),
                ],
              ),
            ),
          ),
          Container(
            width: 2, // Espessura da barra vertical
            color: Colors.black, // Cor da barra divisória
          ),
          Expanded(
            child: Container(
              alignment: Alignment.topCenter,
              padding: EdgeInsets.symmetric(vertical: 10,horizontal: 5),
              child: Column(
                children: [
                  Row(
                    children: [
                      Text(
                        "😢", // Emoji
                        style: TextStyle(fontSize: 25),
                      ),
                      SizedBox(width: 5), // Espaço entre emoji e texto
                      Expanded(
                        child: Text(
                          "Treinos com falha", // Mensagem ao lado do emoji
                          style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                        ),
                      ),
                    ],
                  ),
                  SizedBox(height: 50), // Espaço entre as linhas
                  Center( // Garante centralização total
                    child: Text(
                      _getFaliedDaysCount(),
                      style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
                    ),
                  ),
                ],
              ),
            ),
          )
        ],
      ),
    );
  }

  //TODO: Remover esse metodo daqui e passar para um service, quando o bakcend estiver funcionando
  String _getFaliedDaysCount() => this.daysFailed.toString();
  String _getSucessfulDaysCount() => this.daysSucessful.toString();

}