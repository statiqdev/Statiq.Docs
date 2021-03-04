@echo off
dotnet run --project "build/Statiq.Docs.Build/Statiq.Docs.Build.csproj" -- %*
set exitcode=%errorlevel%
cd %~dp0
exit /b %exitcode%