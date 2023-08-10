namespace PingPongTracker.Data;

public static class Greetings
{
    public record Greeting(int Id, string Language, string Text, string TimeZone);

    public static IEnumerable<Greeting> GetGreetings()
    {
        yield return new Greeting(1, "English", "Let's Play Ping Pong!", "America/New_York");
        yield return new Greeting(2, "Spanish", "¡Juguemos al Ping Pong!", "America/Mexico_City");
        yield return new Greeting(3, "French", "Jouons au Ping Pong!", "Europe/Paris");
        yield return new Greeting(4, "German", "Lass uns Tischtennis spielen!", "Europe/Berlin");
        yield return new Greeting(5, "Italian", "Gioca a Ping Pong!", "Europe/Rome");
        yield return new Greeting(6, "Portuguese", "Vamos jogar Ping Pong!", "America/Sao_Paulo");
        yield return new Greeting(7, "Russian", "Давайте играть в настольный теннис!", "Europe/Moscow");
        yield return new Greeting(8, "Japanese", "卓球をしましょう！", "Asia/Tokyo");
        yield return new Greeting(9, "Chinese", "让我们打乒乓球！", "Asia/Shanghai");
        yield return new Greeting(10, "Korean", "탁구를하자!", "Asia/Seoul");
        yield return new Greeting(11, "Arabic", "لعب تنس الطاولة!", "Asia/Riyadh");
        yield return new Greeting(12, "Hindi", "चलो पिंग पोंग खेलते हैं!", "Asia/Kolkata");
        yield return new Greeting(13, "Turkish", "Ping Pong oynayalım!", "Europe/Istanbul");
    }
}
