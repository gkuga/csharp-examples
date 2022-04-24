using System;

namespace hello_app_cli_di
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            NoDIGreeting();
            Container = new Container();
            Greeting();
        }

        public static void NoDIGreeting()
        {
            var greeter = new NoDIGreeter();
            greeter.Greeting();
        }

        public static void Greeting()
        {
            Container.Greeter.Greeting();
            Container.GreeterFactory.create().Greeting();
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

    public interface IServiceLocator
    {
    }

    public class ServiceLocator : IServiceLocator;
    {
        private static NoDIGreetingWordsCreator Creator { get; }
        public ServiceLocator(NoDIGreetingWordsCreator creator)
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
        IServiceLocator _locator;
        public SLGreeter()
        {
            this._creator = new NoDIGreetingWordsCreator("New No DI Hello World!");
        }
        public void Greeting()
        {
            Console.WriteLine(this._creator.Create());
        }
    }

    public interface IGreetingWordsCreator
    {
        public string Create();
    }
    public class GreetingWordsCreator : IGreetingWordsCreator
    {
        public GreetingWordsCreator()
        {
        }

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
}
