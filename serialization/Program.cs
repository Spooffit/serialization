using Autofac;
using serialization.Common;
using serialization.Common.Actions;
using serialization.Common.Extension;
using serialization.Common.Factories;
using serialization.Common.Providers;
using serialization.Interfaces.Actions;
using serialization.Interfaces.Factories;
using serialization.Interfaces.Providers;
using serialization.IoC;
using serialization.Models;

namespace serialization;

internal class Program
{
    private static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private static readonly string FilePath = Path.Combine(DesktopPath, Constants.FILENAME);
    
    public static async Task Main(string[] args)
    {
        RegisterDependencies();

        await using (var scope = ContainerPreparer.Container.BeginLifetimeScope())
        {
            var personFactory = scope.Resolve<IPersonFactory>();
            var persons = personFactory.ProduceArrange(10000);
        
            var optionFactory = scope.Resolve<IJsonOptionProvider>();
            var writeOptions = optionFactory.GetWriteSerializerOptions();
            var readOptions = optionFactory.GetReadSerializerOptions();
            
            var performer = scope.Resolve<IJsonActionPerformer<Person>>();
            performer.Configure(
                readOptions: readOptions, 
                writeOptions: writeOptions);

            await performer.WriteAsync(FilePath, persons);
            var readPersons = await performer.ReadAsync(FilePath);

            readPersons.Print();

            Console.WriteLine();
            Console.WriteLine("OK");
        }
    }
    
    private static void RegisterDependencies()
    {
        Console.WriteLine();
        Console.WriteLine("Beginning of dependencies injection...");
        ContainerPreparer.Builder.RegisterType<IdProvider>().As<IIdProvider>().SingleInstance();
        ContainerPreparer.Builder.RegisterType<TransportIdProvider>().As<ITransportIdProvider>().SingleInstance();
        ContainerPreparer.Builder.RegisterType<FirstNameProvider>().As<IFirstNameProvider>().SingleInstance();
        ContainerPreparer.Builder.RegisterType<LastNameProvider>().As<ILastNameProvider>().SingleInstance();
        ContainerPreparer.Builder.RegisterType<BirthDateProvider>().As<IBirthDateProvider>().SingleInstance();
        ContainerPreparer.Builder.RegisterType<CreditCardNumberProvider>().As<ICreditCardNumberProvider>().SingleInstance();
        ContainerPreparer.Builder.RegisterType<PhoneProvider>().As<IPhoneProvider>().SingleInstance();
        ContainerPreparer.Builder.RegisterType<SequenceIdProvider>().As<ISequenceIdProvider>().SingleInstance();
        
        ContainerPreparer.Builder.RegisterType<ChildFactory>().As<IChildFactory>().SingleInstance();
        ContainerPreparer.Builder.RegisterType<PersonFactory>().As<IPersonFactory>().SingleInstance();
        
        ContainerPreparer.Builder.RegisterType<JsonReadPersonAction>().As<IJsonReadAction<Person>>().SingleInstance();
        ContainerPreparer.Builder.RegisterType<JsonWritePersonAction>().As<IJsonWriteAction<Person>>().SingleInstance();
        
        ContainerPreparer.Builder.RegisterType<JsonActionPersonPerformer>().As<IJsonActionPerformer<Person>>().SingleInstance();
        ContainerPreparer.Builder.RegisterType<JsonOptionPersonProvider>().As<IJsonOptionProvider>().SingleInstance();
        Console.WriteLine("Dependencies has been injected.");
        Console.WriteLine();
    }
}