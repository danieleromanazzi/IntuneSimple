namespace IntuneSimple
{
    public class Settings
    {
        public static double AlertMinuteExpired { get; set; } = -15;
        public static int AlertExpiredEveryMilliseconds { get; set; } = 5000;
        public static int OpenedUacAttemptMilleseconds { get; set; } = 2500;
        public static string UACViewTitle { get; set; } = "Controllo dell'account utente";


    }
}
