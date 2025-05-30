import 'package:meu_primeiro_app/core/features/home/domain/services/IGetImageService.dart';

class GetImageService implements IGetImageService {
  @override
  String GetImage()  {
    int hour = DateTime.now().hour;

    if (hour >= 6 && hour < 18) {
      return 'assets/images/moon.png'; // Imagem do sol
    } else {
      return 'assets/images/sun.png'; // Imagem da lua
    }

  }
}
