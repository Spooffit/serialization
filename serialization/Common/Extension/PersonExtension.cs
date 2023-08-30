using serialization.Models;

namespace serialization.Common.Extension;

public static class PersonExtension
{
    public static void Print(this ICollection<Person> persons)
    {
        var dataForPrint = PersonsDataEvaluation(persons);

        Console.WriteLine($"Persons count: {dataForPrint.PersonsCount}");
        Console.WriteLine($"Persons credit card count: {dataForPrint.PersonsCreditCardCount}");
        Console.WriteLine($"Persons Average Child Age: {dataForPrint.PersonsAverageChildAge}");
    }

    private static PersonsPrintData PersonsDataEvaluation(ICollection<Person> collection)
    {
        int personsCount = collection.Count;
        int personsCreditCardCount = collection.Sum(person => person.CreditCardNumbers.Length);

        int personsAverageChildAge = (int)collection.Average(person =>
        {
            double daysInYearsConsideringLeapYears = 365.25; // leap years are included for better accuracy

            if (person.Children.Any())
            {
                return person.Children.Average(child =>
                    (DateTimeOffset.Now - DateTimeOffset.FromUnixTimeSeconds(child.BirthDate)).TotalDays /
                    daysInYearsConsideringLeapYears
                );
            }
            else
            {
                return 0;
            }
        });

        return new PersonsPrintData
        {
            PersonsCount = personsCount,
            PersonsCreditCardCount = personsCreditCardCount,
            PersonsAverageChildAge = personsAverageChildAge
        };
    }

    private struct PersonsPrintData
    {
        public PersonsPrintData(
            int personsCount,
            int personsCreditCardCount,
            int personsAverageChildAge)
        {
            PersonsCount = personsCount;
            PersonsCreditCardCount = personsCreditCardCount;
            PersonsAverageChildAge = personsAverageChildAge;
        }

        public int PersonsCount;
        public int PersonsCreditCardCount;
        public int PersonsAverageChildAge;
    }
}