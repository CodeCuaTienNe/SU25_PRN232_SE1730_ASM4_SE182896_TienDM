#!/usr/bin/env pwsh

Write-Host "===========================================" -ForegroundColor Green
Write-Host "    DNA Testing System SOAP Client" -ForegroundColor Green
Write-Host "===========================================" -ForegroundColor Green
Write-Host ""
Write-Host "Starting SOAP Client Console Application..." -ForegroundColor Yellow
Write-Host ""

# Change to script directory
Set-Location $PSScriptRoot

Write-Host "Restoring packages..." -ForegroundColor Cyan
dotnet restore

Write-Host ""
Write-Host "Building application..." -ForegroundColor Cyan
dotnet build

Write-Host ""
Write-Host "Running application..." -ForegroundColor Cyan
Write-Host ""
dotnet run

Read-Host "Press Enter to exit"
