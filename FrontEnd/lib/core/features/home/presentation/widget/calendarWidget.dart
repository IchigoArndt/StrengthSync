import 'package:flutter/material.dart';
import 'package:table_calendar/table_calendar.dart';

class CalendarWidget extends StatefulWidget {
  const CalendarWidget({super.key});

  @override
  _CalendarWidgetState createState() => _CalendarWidgetState();
}

class _CalendarWidgetState extends State<CalendarWidget> {
  DateTime _focusedDay = DateTime.now();
  DateTime? _selectedDay;
  CalendarFormat _calendarFormat = CalendarFormat.week;
  bool titleVisible = false;

  Map<DateTime, String> _trainingStatus = {}; // Guarda status de cada dia

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        SizedBox(height: 50),
        TableCalendar(
          locale: 'pt_br',
          focusedDay: _focusedDay,
          firstDay: DateTime(2020),
          lastDay: DateTime(2030),
          headerVisible: titleVisible,
          calendarFormat: _calendarFormat,
          headerStyle: HeaderStyle(
            titleCentered: true,
            formatButtonVisible: false,
          ),
          calendarStyle: CalendarStyle(
            markersMaxCount: 0,
            todayDecoration: BoxDecoration(
              color: Colors.transparent, // Remove a cor de fundo do dia atual
            ),
          ),
          /*selectedDayPredicate: (day) {
            return isSameDay(_selectedDay, day);
          },*/
          onDaySelected: (selectedDay, focusedDay) {
            setState(() {
              _selectedDay = selectedDay;
              _focusedDay = focusedDay;
            });
            _showTrainingDialog(selectedDay);
          },
          eventLoader: (day) {
            if (_trainingStatus.containsKey(day)) {
              return [
                _trainingStatus[day]!,
              ]; // Retorna os eventos (status do treino)
            }
            return [];
          },
          calendarBuilders: CalendarBuilders(
            defaultBuilder: (context, day, focusedDay) {
              return Stack(
                alignment: Alignment.center,
                children: [
                  Text(day.day.toString(), style: TextStyle(fontSize: 16)),
                  if (_trainingStatus.containsKey(day))
                    Text(
                      _trainingStatus[day] == "Treino pulado 😢" ? "😢" : "✅",
                      style: TextStyle(
                        fontSize: 24.0,
                        backgroundColor: Colors.transparent,
                          fontFamily: 'Arial'
                      ), // Exibe apenas o emoji do treino
                    ),
                ],
              );
              return Container(); // Não exibe nada caso o dia não tenha status
            },
          ),
        ),
        SizedBox(height: 10),
        TextButton.icon(
          onPressed: () {
            setState(() {
              _calendarFormat =
                  _calendarFormat == CalendarFormat.month
                      ? CalendarFormat.week
                      : CalendarFormat.month;

              titleVisible =
                  _calendarFormat == CalendarFormat.week ? false : true;
            });
          },
          icon: Icon(
            _calendarFormat == CalendarFormat.month
                ? Icons.arrow_drop_up
                : Icons.arrow_drop_down,
          ),
          label: Text(
            _calendarFormat == CalendarFormat.month
                ? "Retrair Calendário"
                : "Expandir Calendário",
            style: TextStyle(fontSize: 16),
          ),
        ),
      ],
    );
  }

  void _showTrainingDialog(DateTime selectedDay) {
    showDialog(
      context: context,
      builder:
          (context) => AlertDialog(
            title: Text("Selecione o status do treino"),
            actions: [
              TextButton(
                onPressed: () {
                  setState(() {
                    _trainingStatus[selectedDay] = "Treinado";
                  });
                  Navigator.pop(context);
                },
                child: Text("Treinado ✅"),
              ),
              TextButton(
                onPressed: () {
                  setState(() {
                    _trainingStatus[selectedDay] = "Treino pulado 😢";
                  });
                  Navigator.pop(context);
                },
                child: Text("Treino pulado ❌"),
              ),
            ],
          ),
    );
  }
}
