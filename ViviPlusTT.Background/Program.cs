


using ViviPlusTT.Infrastructure.Repository;


namespace ViviPlusTT.Background
{
    class Program
    {
        static void Main(string[] args)
        {
            IResponseRepository responseRepository = new ResponseRepository();            

            var core = new Core.Background.WatcherService(responseRepository);

            core.GenerateResponse();
        }
    }
}