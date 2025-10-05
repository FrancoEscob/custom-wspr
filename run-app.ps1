$ErrorActionPreference = "Continue"

$exePath = ".\src\CustomWspr.App\bin\x64\Debug\net8.0-windows10.0.19041.0\CustomWspr.App.exe"

Write-Host "Launching Custom WSPR..." -ForegroundColor Green
Write-Host "Executable: $exePath" -ForegroundColor Cyan

if (Test-Path $exePath) {
    try {
        $process = Start-Process -FilePath $exePath -PassThru -ErrorAction Stop
        Write-Host "Process started with ID: $($process.Id)" -ForegroundColor Green
        Write-Host "Waiting for process to initialize..." -ForegroundColor Yellow
        Start-Sleep -Seconds 3
        
        if ($process.HasExited) {
            Write-Host "WARNING: Process exited with code: $($process.ExitCode)" -ForegroundColor Red
        } else {
            Write-Host "Process is running!" -ForegroundColor Green
        }
    }
    catch {
        Write-Host "ERROR: Failed to start process" -ForegroundColor Red
        Write-Host $_.Exception.Message -ForegroundColor Red
    }
} else {
    Write-Host "ERROR: Executable not found at: $exePath" -ForegroundColor Red
}

Write-Host "`nPress any key to exit..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
