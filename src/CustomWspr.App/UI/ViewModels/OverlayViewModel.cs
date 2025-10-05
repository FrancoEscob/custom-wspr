using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomWspr.App.Models;

namespace CustomWspr.App.UI.ViewModels;

public partial class OverlayViewModel : ObservableObject
{
    [ObservableProperty]
    private OverlayState _currentState = OverlayState.Idle;

    [ObservableProperty]
    private string _stateText = "Press R to start recording";

    [ObservableProperty]
    private string _elapsedTime = "00:00";

    [ObservableProperty]
    private bool _isRecordingControlsVisible = false;

    [ObservableProperty]
    private bool _isIdleMessageVisible = true;

    partial void OnCurrentStateChanged(OverlayState value)
    {
        UpdateStateUI();
    }

    [RelayCommand]
    private void ToggleRecording()
    {
        if (CurrentState == OverlayState.Idle)
        {
            CurrentState = OverlayState.Recording;
        }
        else if (CurrentState == OverlayState.Recording)
        {
            CurrentState = OverlayState.Idle;
        }
    }

    [RelayCommand]
    private void PauseRecording()
    {
        if (CurrentState == OverlayState.Recording)
        {
            CurrentState = OverlayState.Paused;
        }
        else if (CurrentState == OverlayState.Paused)
        {
            CurrentState = OverlayState.Recording;
        }
    }

    [RelayCommand]
    private void StopRecording()
    {
        CurrentState = OverlayState.Idle;
    }

    private void UpdateStateUI()
    {
        switch (CurrentState)
        {
            case OverlayState.Idle:
                StateText = "Press R to start recording";
                IsIdleMessageVisible = true;
                IsRecordingControlsVisible = false;
                ElapsedTime = "00:00";
                break;

            case OverlayState.Recording:
                StateText = "Recording...";
                IsIdleMessageVisible = false;
                IsRecordingControlsVisible = true;
                break;

            case OverlayState.Paused:
                StateText = "Paused";
                IsIdleMessageVisible = false;
                IsRecordingControlsVisible = true;
                break;

            case OverlayState.Processing:
                StateText = "Processing...";
                IsIdleMessageVisible = false;
                IsRecordingControlsVisible = false;
                break;

            case OverlayState.Ready:
                StateText = "Ready";
                IsIdleMessageVisible = false;
                IsRecordingControlsVisible = false;
                break;

            case OverlayState.Error:
                StateText = "Error occurred";
                IsIdleMessageVisible = false;
                IsRecordingControlsVisible = false;
                break;
        }
    }
}
