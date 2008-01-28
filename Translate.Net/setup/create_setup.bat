@echo off
call setup_iss.cmd


del buildlog.txt
echo _
echo Creating installer
echo ------------------------------------------------------------------------
echo Creating installer >> buildlog.txt
echo ------------------------------------------------------------------------  >> buildlog.txt
call %iscc% translatenet.iss >> buildlog.txt
if errorlevel 1 goto :IS_ERROR
echo ------------------------------------------------------------------------  >> buildlog.txt
echo Done
echo Done >> buildlog.txt


goto :NORMAL_EXIT

:IS_ERROR
echo Errors in script reached
pause
goto :EOF

:NORMAL_EXIT
echo Setup created successfully
del buildlog.txt
pause





