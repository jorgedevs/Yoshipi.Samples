using Meadow;
using System.Threading.Tasks;

namespace GroveRelayController;

public class Program
{
    public static async Task Main(string[] args)
    {
        await MeadowOS.Start(args);
    }
}