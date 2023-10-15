using System;
using System.Globalization;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace stringlocalization;

public class Program
{
    public static async Task Main(string[] args)
    {
        if (args is { Length: 1 })
        {
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(args[0]);
        }

        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddLocalization(options =>
        {
            options.ResourcesPath = "Resources";
        });

        builder.Services.AddTransient<MessageService>();

        using IHost host = builder.Build();

        IServiceProvider services = host.Services;

        MessageService messageService = services.GetRequiredService<MessageService>();

        Console.WriteLine("LocaleMsg is : {0}", messageService.GetGreetingMessage());
        
        await host.RunAsync();
    }
}