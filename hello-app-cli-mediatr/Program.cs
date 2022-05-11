using System;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Autofac;
using MediatR;

namespace hello_app_cli_mediatr;

class Program
{
    static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<Mediator>().As<IMediator>();
        builder.Register<ServiceFactory>(context =>
        {
            var c = context.Resolve<IComponentContext>();
            return t => c.Resolve(t);
        });
        builder.RegisterType<PingHandler>().As<IRequestHandler<Ping, string>>();
        var container = builder.Build();
        var mediator = container.Resolve<IMediator>();
        var response = mediator.Send(new Ping());
        Console.WriteLine(response.Result);
    }
}

public class Ping : IRequest<string> { }

public class PingHandler : IRequestHandler<Ping, string>
{
    public Task<string> Handle(Ping request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Pong");
    }
}
