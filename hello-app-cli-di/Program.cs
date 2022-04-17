using System;

namespace hello_app_cli_di
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            Container = new Container();
						Greeting();
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
}
