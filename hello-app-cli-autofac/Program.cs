using System;
using Autofac;

namespace hello_app_cli_autofac
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<GreetingWordsCreator>().As<IGreetingWordsCreator>();
            builder.RegisterType<Greeter>().As<IGreeter>();
            Container = builder.Build();
            Greeting();
        }

        public static void Greeting()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var greeter = scope.Resolve<IGreeter>();
                greeter.Greeting();
            }
        }
    }

    public interface IGreeter
    {
        void Greeting();
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
        string Create();
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
