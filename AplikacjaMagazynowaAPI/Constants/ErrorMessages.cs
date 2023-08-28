using System.Globalization;

namespace AplikacjaMagazynowaAPI.Constants
{
    public class ErrorMessages
    {
        public const string BadQuantity = "Nieprawidłowa ilość towaru.";
        public const string CannotEditComplete = "Wybrana pozycja została już zrealizowana i nie podlega edycji.";
        public const string DataIncomplete = "Podane dane są niekompletne.";
        public const string NoChangeInData = "Podane dane są tożsame z istniejącym rekordem.";
        public const string OrderItemDoesNotExist = "Produkt o podanym kodzie nie jest powiązany z zamównieniem o takim nunmerze.";
        public const string ProductExistsInDb = "Produkt o podanym kodzie istnieje już w bazie.";
        public const string ProductUnavailable = "Jeden lub więcej z produktów nie jest dostępny.";
        public const string UnexpectedServerError = "Wystąpił nieoczekiwany błąd. Spróbuj ponownie później.";
    }
}
