import 'package:flutter/material.dart';
import 'package:table_calendar/table_calendar.dart';

class CalendarWidget extends StatefulWidget {
  const CalendarWidget({super.key}); // Adicionando um key para evitar erro de const

  @override
  _CalendarWidgetState createState() => _CalendarWidgetState();
}

class _CalendarWidgetState extends State<CalendarWidget> {
  DateTime _selectedDay = DateTime.now();
  DateTime _focusedDay = DateTime.now();
  CalendarFormat _calendarFormat = CalendarFormat.week; // Adicionado para alternar formatos

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        TableCalendar(
          focusedDay: _focusedDay,
          firstDay: DateTime(2020),
          lastDay: DateTime(2030),
          calendarFormat: _calendarFormat, // Agora o formato pode ser alterado
          availableCalendarFormats: {
          CalendarFormat.month: "Mês 📆",
          CalendarFormat.week: "Semana 📅",
          },
          headerStyle: HeaderStyle(formatButtonVisible: true), // Botão para mudar formato
          onFormatChanged: (format) {
            setState(() {
              _calendarFormat = format;
            });
          },
          onDaySelected: (selectedDay, focusedDay) {
            setState(() {
              _selectedDay = selectedDay;
              _focusedDay = focusedDay;
            });
          },
        )
      ],
    );
  }
}