using System.Globalization;

namespace AplikacjaMagazynowaAPI.Constants
{
    public class ErrorMessages
    {
        public const string DataIncomplete = "Podane dane są niekompletne.";
        public const string QuantityLessThanZero = "Ilość towaru nie może być mniejsza od zera";
        public const string QuantityLessOrZero = "Ilość towaru musi być większa od zera.";
        public const string ProductExistsInDb = "Produkt o podanym kodzie istnieje już w bazie.";
        public const string ProductDoesNotExist = "Produkt o podanym kodzie nie istnieje";
        public const string ProductUnavailable = "Jeden lub więcej produktów z Twojego zamówienia nie jest dostępny w żądanej ilości lub nie istnieje w bazie";
    }
}
