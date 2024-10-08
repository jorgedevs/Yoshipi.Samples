﻿using Meadow;
using System.Threading.Tasks;
using WifiWeather.Hardware;
using YoshiPi;

namespace WifiWeather;

public class MeadowApp : YoshiPiApp
{
    private MainController? mainController;

    public override Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        Hardware.Display.InvertDisplayColor(true);

        var hardware = new WifiWeatherHardware(Hardware);
        mainController = new MainController(hardware);

        return Task.CompletedTask;
    }

    public override Task Run()
    {
        Resolver.Log.Info("Run...");

        mainController?.Run();

        return Task.CompletedTask;
    }
}