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
            builder.RegisterType<EnglishGreeter>().As<IGreeter>();
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

    public class EnglishGreeter : IGreeter
    {
        public void Greeting()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
