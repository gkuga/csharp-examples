using System;

namespace hello_app_cli_non_autofac
{
    class Program
    {
        static void Main(string[] args)
        {
            Greeting();
        }

        public static void Greeting()
        {
            IGreetingWordsCreator creator = new GreetingWordsCreator();
            IGreeter greeter = new Greeter(creator);
            greeter.Greeting();
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
