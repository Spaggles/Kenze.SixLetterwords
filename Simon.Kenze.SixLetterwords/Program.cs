using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simon.Kenze.SixLetterWords.Business.LetterWords;
using Simon.Kenze.SixLetterWords.BusinessFacade;
using Simon.Kenze.SixLetterWords.Data;
using Simon.Kenze.SixLetterWords.Objects.Filters;
using Simon.Kenze.SixLetterWords.Responses;

var host = CreateHostBuilder(args).Build();
Main(host.Services);

static void Main(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    ICalcLetterCombinationsService service = provider.GetRequiredService<ICalcLetterCombinationsService>();

    ListResponse<string> result = service.GetPossibleCombinationsOfX(new CalcLetterCombinationsFilter
    {
        DesiredResultingCombinationStringLength = 6,
    });

    if (!result.IsSuccess)
    {
        Console.WriteLine(string.Join(",",result.ErrorMessages));
        return;
    }

    foreach (string combination in result.Value)
    {
        Console.WriteLine(combination);
    }

    Console.ReadLine();
}

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) => services
            .AddTransient<ILetterWordsRepository, LetterWordsRepository>()
            .AddTransient<ICalcLetterCombinationsService, CalcLetterCombinationsService>());
}