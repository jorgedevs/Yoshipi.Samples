using AzureIoTHubLogging.Hardware;
using System;

namespace AzureIoTHubLogging.Controllers;

public class InputController
{
    public event EventHandler? LeftButtonPressed;

    public event EventHandler? RightButtonPressed;

    public InputController(IAzureIoTHubLoggingHardware hardware)
    {
        if (hardware.LeftButton is { } leftButton)
        {
            leftButton.PressStarted += (s, e) => LeftButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        if (hardware.RightButton is { } rightButton)
        {
            rightButton.PressStarted += (s, e) => RightButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
