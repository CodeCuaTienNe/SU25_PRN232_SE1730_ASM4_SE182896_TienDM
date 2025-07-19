@echo off
echo ============================================
echo    DNA Testing System - Quick Test
echo ============================================
echo.
echo This script will test both HTTP and HTTPS protocols
echo.

echo Starting SOAP API Server...
cd ..\DNATestingSystem.SoapAPIServices.TienDM
start cmd /k "dotnet run"

echo.
echo Waiting for server to start (10 seconds)...
timeout /t 10 /nobreak >nul

echo.
echo Testing HTTP Protocol...
cd ..\DNATestingSystem.SoapClient.ConsoleApp.TienDM
echo 2| dotnet run

echo.
echo Press any key to test HTTPS protocol...
pause >nul

echo.
echo Testing HTTPS Protocol...
echo 1| dotnet run

echo.
echo Tests completed. Press any key to close...
pause >nul
