using Meadow;
using Meadow.Hardware;
using MeadowCloudCommands.Commands;
using MeadowCloudCommands.Hardware;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MeadowCloudCommands.Controllers;

internal class MainController
{
    private IMeadowCloudCommandHardware hardware;
    private IWiFiNetworkAdapter network;
    private DisplayController displayController;

    public MainController(IMeadowCloudCommandHardware hardware, IWiFiNetworkAdapter network)
    {
        this.hardware = hardware;
        this.network = network;
    }

    public void Initialize()
    {
        hardware.Initialize();


        displayController = new DisplayController(hardware.Display);
        displayController.ShowSplashScreen();
        Thread.Sleep(3000);
        displayController.ShowDataScreen();

        Resolver.UpdateService.StateChanged += (sender, state) =>
        {
            if (state.ToString().ToLower() == "idle")
            {

            }
        };

        Resolver.CommandService.Subscribe<ToggleRelayCommand>(command =>
        {
            if (command.Relay < 0 || command.Relay > 4)
            {
                displayController.UpdateStatus($"Command invalid!");
                Thread.Sleep(2000);
                displayController.UpdateStatus(DateTime.Now.ToString("hh:mm tt dd/MM/yy"));
                return;
            }

            displayController.UpdateStatus($"Command received!");
            displayController.UpdateSyncStatus(true);

            Resolver.Log.Trace($"Received ToggleRelayCommand command to relay {command.Relay} : {command.IsOn}");

            switch (command.Relay)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    hardware.FourChannelRelay.Relays[command.Relay].State = command.IsOn
                        ? Meadow.Peripherals.Relays.RelayState.Open
                        : Meadow.Peripherals.Relays.RelayState.Closed;
                    break;
                case 4:
                    if (command.IsOn)
                        hardware.FourChannelRelay.SetAllOn();
                    else
                        hardware.FourChannelRelay.SetAllOff();
                    break;
            }

            displayController.UpdateRelayStatus(command.Relay, command.IsOn);
            displayController.UpdateLastUpdated(DateTime.Now.ToString("hh:mm tt dd/MM/yy"));

            Thread.Sleep(2000);

            displayController.UpdateStatus(DateTime.Now.ToString("hh:mm tt dd/MM/yy"));
            displayController.UpdateSyncStatus(false);
        });
    }

    public async Task Run()
    {
        while (true)
        {
            displayController.UpdateWiFiStatus(network.IsConnected);

            if (network.IsConnected)
            {
                displayController.UpdateStatus(DateTime.Now.ToString("hh:mm tt dd/MM/yy"));

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
            else
            {
                displayController.UpdateStatus("Offline...");

                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }
    }
}