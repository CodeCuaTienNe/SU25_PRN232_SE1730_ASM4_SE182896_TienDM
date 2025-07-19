@echo off
echo ===========================================
echo    DNA Testing System SOAP Client
echo ===========================================
echo.
echo Starting SOAP Client Console Application...
echo.

cd /d "%~dp0"

echo Restoring packages...
dotnet restore

echo.
echo Building application...
dotnet build

echo.
echo Running application...
echo.
dotnet run

pause
