using System.Text.Json;

class Person
{
    public Int32 Id { get; set; }
    public Guid TransportId { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public Int32 SequenceId { get; set; }
    public String[] CreditCardNumbers { get; set; }
    public Int32 Age { get; set; }
    public String[] Phones { get; set; }
    public Int64 BirthDate { get; set; }
    public Double Salary { get; set; }
    public Boolean IsMarred { get; set; }
    public Gender Gender { get; set; }
    public Child[] Children { get; set; }
}

class Child
{
    public Int32 Id { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public Int64 BirthDate { get; set; }
    public Gender Gender { get; set; }
}

enum Gender
{
    Male,
    Female
}

internal class Program
{
    static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    static string fileName = "Persons.json";
    static string filePath = Path.Combine(desktopPath, fileName);

    static ICollection<Person> GenerateRandomlyPersons(int count = 10000)
    {
        ICollection<Person> persons = new List<Person>();

        for (int i = 1; i <= count; i++)
        {
            var birthDate = DateTimeOffset.UtcNow;

            persons.Add(new Person
            {
                Id = i,
                TransportId = Guid.NewGuid(),
                FirstName = "FirstName",
                LastName = "LastName",
                SequenceId = i,
                CreditCardNumbers = new string[] { "Credit Card" },
                Age = new Random().Next(18, 61),
                Phones = new string[] { "Phone" },
                BirthDate = birthDate.ToUnixTimeSeconds(),
                Salary = new Random().Next(1000, 10001),
                IsMarred = new Random().Next(0, 2) == 1,
                Gender = (Gender)new Random().Next(0, 2),
                Children = new Child[] { }
            });
        }

        return persons;
    }

    static async Task WritePersonsAsJsonAsync(ICollection<Person> persons)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fs, persons, options);
            Console.WriteLine("Data has been saved to file");
        }

        persons.Clear();
        Console.WriteLine("Data has been cleared from memory");
    }

    static async Task<ICollection<Person>> ReadPersonsAsJsonAsync()
    {
        ICollection<Person>? persons;

        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            persons = await JsonSerializer.DeserializeAsync<ICollection<Person>>(fs);
            Console.WriteLine("Data has been read from file");
        }

        return persons;
    }

    static void PrintPersons(ICollection<Person> readPersons)
    {
        int personsCount = readPersons.Count;
        int personsCreditCardCount = readPersons.Sum(person => person.CreditCardNumbers.Length);

        int personsAverageChildAge = (int)readPersons.Average(person =>
        {
            double daysInYearsConsideringLeapYears = 365.25;

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

        Console.WriteLine(personsCount);
        Console.WriteLine(personsCreditCardCount);
        Console.WriteLine(personsAverageChildAge);
    }

    public static async Task Main(string[] args)
    {
        var persons = GenerateRandomlyPersons();

        await WritePersonsAsJsonAsync(persons);

        var readPersons = await ReadPersonsAsJsonAsync();

        PrintPersons(readPersons);
    }
}