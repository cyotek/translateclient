@echo off
call "%ProgramFiles%\Microsoft.NET\SDK\v2.0\Bin\sdkvars.bat"
sgen.exe /a:Translate.Net.exe /t:Translate.TranslateOptions /f /compiler:/keyfile:..\source\Translate\Translate.snk   