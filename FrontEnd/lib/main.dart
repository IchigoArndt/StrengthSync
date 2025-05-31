import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:provider/provider.dart';

import 'package:meu_primeiro_app/core/features/login/presentation/login_presenter.dart';
import 'package:meu_primeiro_app/core/features/login/data/services/UserAuthenticationService.dart';
import 'package:meu_primeiro_app/core/features/home/presentation/home_presenter.dart';

void main() {
  runApp(
    MultiProvider(
      providers: [
        Provider<userAuthenticationService>(
          create: (_) => userAuthenticationService(),
        ),
      ],
      child: const MyApp(),
    ),
  );
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      localizationsDelegates: [
        GlobalMaterialLocalizations.delegate,
        GlobalWidgetsLocalizations.delegate,
        GlobalCupertinoLocalizations.delegate
      ],
      supportedLocales: [const Locale('pt', 'BR')],
      title: 'Flutter Demo',
      initialRoute: '/',
      routes: {
        '/': (context) => LoginPage(),
        '/home': (context) => HomePage()
      },
    );
  }
}
