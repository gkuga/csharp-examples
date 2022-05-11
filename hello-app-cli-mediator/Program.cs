using System;

namespace hello_app_cli_mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            var greeterA = new GreeterA(new GreeterB(), new GreeterC(), new GreeterD(), new GreeterE());
            greeterA.Greeting();
        }
    }
    class Mediator
    {
        private GreeterB B { get; }
        private GreeterC C { get; }
        private GreeterD D { get; }
        private GreeterE E { get; }

        public Mediator(GreeterB b, GreeterC c, GreeterD d, GreeterE e)
        {
            B = b;
            C = c;
            D = d;
            E = e;
        }
    }
    class GreeterA
    {
        private GreeterB _b;
        private GreeterC _c;
        private GreeterD _d;
        private GreeterE _e;

        public GreeterA(GreeterB b, GreeterC c, GreeterD d, GreeterE e)
        {
            _b = b;
            _c = c;
            _d = d;
            _e = e;
        }
        public void Greeting()
        {
            Console.WriteLine($"Hello {this._b.Name}");
            Console.WriteLine($"Hello {this._c.Name}");
            Console.WriteLine($"Hello {this._d.Name}");
            Console.WriteLine($"Hello {this._e.Name}");
        }
    }
    class GreeterB
    {
        public string Name { get; } = "B";
    }
    class GreeterC
    {
        public string Name { get; } = "C";
    }
    class GreeterD
    {
        public string Name { get; } = "D";
    }
    class GreeterE
    {
        public string Name { get; } = "E";
    }
}
