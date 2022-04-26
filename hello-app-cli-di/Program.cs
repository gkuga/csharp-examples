using System;

namespace hello_app_cli_di
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            NoDIGreeting();
            SLGreeting();
            Container = new Container();
            DIGreeting();
            FactoryGreeting();
        }

        public static void SLGreeting()
        {
            var creator = new GreetingWordsCreator();
            var locator = new ServiceLocator(creator);
            var greeter = new SLGreeter(locator);
            greeter.Greeting();
        }

        public static void NoDIGreeting()
        {
            var greeter = new NoDIGreeter();
            greeter.Greeting();
        }

        public static void DIGreeting()
        {
            Container.Greeter.Greeting();
            Container.GreeterFactory.create().Greeting();
        }

        public static void FactoryGreeting()
        {
            var greeter = new FactoryGreeter();
            greeter.Greeting();
        }
    }

    public interface IContainer
    {
        public IGreeter Greeter { get; }
        public IGreeterFactory GreeterFactory { get; }
    }

    public class Container : IContainer
    {
        public IGreeter Greeter { get; }
        public IGreeterFactory GreeterFactory { get; }

        public Container()
        {
            IGreetingWordsCreator creator = new GreetingWordsCreator();
            Greeter = new Greeter(creator);
            GreeterFactory = new GreeterFactory(creator);
        }
    }

    public class ServiceLocator
    {
        public GreetingWordsCreator Creator { get; }
        public ServiceLocator(GreetingWordsCreator creator)
        {
            this.Creator = creator;
        }
    }

    public interface IGreeterFactory
    {
        public IGreeter create();
    }

    public class GreeterFactory : IGreeterFactory
    {
        private IGreetingWordsCreator _creator;

        public GreeterFactory(IGreetingWordsCreator creator)
        {
            _creator = creator;
        }

        public IGreeter create()
        {
            return new Greeter(_creator);
        }
    }

    public interface IGreeter
    {
        public void Greeting();
    }

    public class Greeter : IGreeter
    {
        private IGreetingWordsCreator _creator;
        public Greeter(IGreetingWordsCreator creator)
        {
            this._creator = creator;
        }
        public void Greeting()
        {
            Console.WriteLine(this._creator.Create());
        }
    }

    public class NoDIGreeter
    {
        private NoDIGreetingWordsCreator _creator;
        public NoDIGreeter()
        {
            this._creator = new NoDIGreetingWordsCreator("New No DI Hello World!");
        }
        public void Greeting()
        {
            Console.WriteLine(this._creator.Create());
        }
    }

    public class SLGreeter
    {
        private ServiceLocator _locator;
        public SLGreeter(ServiceLocator locator)
        {
            this._locator = locator;
        }
        public void Greeting()
        {
            Console.WriteLine(this._locator.Creator.Create());
        }
    }

    public interface IGreetingWordsCreator
    {
        public string Create();
    }

    public class GreetingWordsCreator : IGreetingWordsCreator
    {
        public string Create()
        {
            return "Hello World!";
        }
    }

    public class NoDIGreetingWordsCreator
    {
        private string _words;
        public NoDIGreetingWordsCreator(string words)
        {
            this._words = words;
        }
        public string Create()
        {
            return this._words;
        }
    }

    public interface IFactory<T>
    {
        T Create(object context);
    }
    public interface IGreetingWordsCreatorFactory
    {
        IGreetingWordsCreator Create();
    }
    public class GreetingWordsCreatorFactory : IGreetingWordsCreatorFactory
    {
        public IGreetingWordsCreator Create()
        {
            return new GreetingWordsCreator();
        }
    }
    public class FactoryGreeter
    {
        public static IGreetingWordsCreatorFactory Factory { get; set; } = new GreetingWordsCreatorFactory();

        private IGreetingWordsCreator _creator;

        public FactoryGreeter()
        {
            this._creator = Factory.Create();
        }
        public void Greeting()
        {
            Console.WriteLine(this._creator.Create());
        }
    }
}
