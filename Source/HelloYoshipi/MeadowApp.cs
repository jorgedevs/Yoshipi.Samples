﻿using Meadow;
using Meadow.Foundation.Graphics.MicroLayout;
using Meadow.Foundation.Hmi;
using Meadow.Peripherals.Displays;
using System.IO;
using System.Threading.Tasks;
using YoshiPi;

namespace HelloYoshipi;

public class MeadowApp : YoshiPiApp
{
    public override async Task Initialize()
    {
        Resolver.Log.Info("Initialize...");

        Hardware.Display.InvertDisplayColor(true);

        var displayController = new DisplayController(
            new DisplayScreen(
                (IPixelDisplay)Hardware.Display!,
                RotationType._270Degrees,
                Hardware.Touchscreen)
        );

        var ts = new TouchscreenCalibrationService(displayController.DisplayScreen, new FileInfo("calibration.dat"));
        ts.EraseCalibrationData();

        var calData = ts.GetSavedCalibrationData();
        if (calData != null)
        {
            Hardware.Touchscreen.SetCalibrationData(calData);
        }
        else
        {
            Resolver.Log.Info("Calibrating...");
            await ts.Calibrate(true);
            Resolver.Log.Info("Calibration done.");
        }

        displayController.LoadScreen();
    }

    public override Task Run()
    {
        Resolver.Log.Info("Run...");

        return Task.CompletedTask;
    }
}