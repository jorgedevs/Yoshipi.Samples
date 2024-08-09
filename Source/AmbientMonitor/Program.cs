using Meadow;
using System.Threading.Tasks;

namespace AmbientMonitor;

public class Program
{
    public static async Task Main(string[] args)
    {
        await MeadowOS.Start(args);
    }
}
