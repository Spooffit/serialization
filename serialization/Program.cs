using System.Text.Json;
using Autofac;
using serialization.Common;
using serialization.Common.Factories;
using serialization.Common.Providers;
using serialization.Interfaces;
using serialization.Models;

namespace serialization;

internal class Program
{
    static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    static string filePath = Path.Combine(desktopPath, Constants.FILENAME);
    
    public static async Task Main(string[] args)
    {
        RegisterDependencies();
        await PerformActionAsync();

        Console.WriteLine();
        Console.WriteLine("OK");
    }
    
    private static void RegisterDependencies()
    {
        Console.WriteLine("The beginning of dependencies injection...");
        Builder.RegisterType<IdProvider>().As<IIdProvider>().SingleInstance();
        Builder.RegisterType<TransportIdProvider>().As<ITransportIdProvider>().SingleInstance();
        Builder.RegisterType<FirstNameProvider>().As<IFirstNameProvider>().SingleInstance();
        Builder.RegisterType<LastNameProvider>().As<ILastNameProvider>().SingleInstance();
        Builder.RegisterType<BirthDateProvider>().As<IBirthDateProvider>().SingleInstance();
        Builder.RegisterType<CreditCardNumberProvider>().As<ICreditCardNumberProvider>().SingleInstance();
        Builder.RegisterType<PhoneProvider>().As<IPhoneProvider>().SingleInstance();
        Builder.RegisterType<SequenceIdProvider>().As<ISequenceIdProvider>().SingleInstance();
        Builder.RegisterType<ChildFactory>().As<IChildFactory>().SingleInstance();
        Builder.RegisterType<PersonFactory>().As<IPersonFactory>().SingleInstance();
        Console.WriteLine("Dependencies has been injected.");
        Console.WriteLine();
    }

    private static async Task PerformActionAsync()
    {
        var persons = GeneratePersonsRandomly();
        
        var options = GenerateJsonSerializerOptions();
        
        await WritePersonsAsJsonAsync(persons, options);

        var readPersons = await ReadPersonsAsJsonAsync();

        PrintPersons(readPersons);
    }

    private static JsonSerializerOptions GenerateJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    private static ICollection<Person> GeneratePersonsRandomly(int count = 10000)
    {
        using (var scope = Container.BeginLifetimeScope())
        {
            var personFactory = scope.Resolve<IPersonFactory>();
            return personFactory.ProduceArrange(count);
        }
    }

    static async Task WritePersonsAsJsonAsync(ICollection<Person> persons, JsonSerializerOptions? options = null)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            if (options != null)
            {
                await JsonSerializer.SerializeAsync(fs, persons, options);
            }
            else
            {
                await JsonSerializer.SerializeAsync(fs, persons);
            }
            Console.WriteLine("Data has been saved to file.");
        }

        persons.Clear();
        Console.WriteLine("Data has been cleared from memory.");
    }

    static async Task<ICollection<Person>> ReadPersonsAsJsonAsync()
    {
        ICollection<Person>? persons;
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            persons = await JsonSerializer.DeserializeAsync<ICollection<Person>>(fs, options);
            Console.WriteLine("Data has been read from file.");
            Console.WriteLine();
        }

        return persons;
    }

    static void PrintPersons(ICollection<Person> readPersons)
    {
        int personsCount = readPersons.Count;
        int personsCreditCardCount = readPersons.Sum(person => person.CreditCardNumbers.Length);

        int personsAverageChildAge = (int)readPersons.Average(person =>
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

        Console.WriteLine($"Persons count: {personsCount}");
        Console.WriteLine($"Persons credit card count: {personsCreditCardCount}");
        Console.WriteLine($"Persons Average Child Age: {personsAverageChildAge}");
    }
    
    
    private static IContainer _container;
    public static IContainer Container => _container ?? (_container = Builder.Build());

    private static ContainerBuilder _builder;
    public static ContainerBuilder Builder => _builder ?? (_builder = new ContainerBuilder());
}