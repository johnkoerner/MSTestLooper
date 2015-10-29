@echo off
::------------------------------------------------
:: Install a MSTest unit test type extension
:: which defines a new test class attribute
:: and how to execute its test methods and
:: interpret results.
::
:: NOTE: Only VS needs this and the registration done; the xcopyable mstest uses
:: TestTools.xml virtualized registry file updated which we already have done in sd
::------------------------------------------------


setlocal


:: All the files we need to copy or register are realtive to this script folder
set extdir=%~dp0


:: Get 32 or 64-bit OS
set win64=0
if not "%ProgramFiles(x86)%" == "" set win64=1
if %win64% == 1 (
    set vs14Key=HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\14.0
) else (
    set vs14Key=HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\14.0
)


:: Get the VS installaton path from the Registry
for /f "tokens=2*" %%i in ('reg.exe query %vs14Key% /v InstallDir') do set vsinstalldir=%%j


:: Display some info
echo.
echo =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-
echo Please ensure that you are running with adminstrator privileges
echo to copy into the Visual Studio installation folder add keys to the Registry.
echo Any access denied messages probably means you are not.
echo =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-
echo.
echo 64-bit OS: %win64%
echo Visual Studio 14.0 regkey:   %vs14Key%
echo Visual Studio 14.0 IDE dir:  %vsinstalldir%


::
:: Copy the SSM test type extension assembly to the VS private assemblies folder
::

set extdll=TestLooper.dll
set extpdb=TestLooper.pdb
set vsprivate=%vsinstalldir%PrivateAssemblies
set vspublic=%vsinstalldir%PublicAssemblies
echo Copying to VS PrivateAssemblies: %vsprivate%\%extdll%
copy /Y %extdir%%extdll% "%vsprivate%\%extdll%"
copy /Y %extdir%%extpdb% "%vsprivate%\%extpdb%"
echo Copying to VS PublicAssemblies: %vspublic%\%extdll%
copy /Y %extdir%%extdll% "%vspublic%\%extdll%"
copy /Y %extdir%%extpdb% "%vspublic%\%extpdb%"


::
:: Register the extension with mstest as a known test type
:: (SSM has two currently, both are in the same assembly)
::

echo Registering the unit test types extensions for use in VS' MSTest

:: Keys Only for 64-bit
if %win64% == 1 (
    set vs14ExtKey64=HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\14.0\EnterpriseTools\QualityTools\TestTypes\{13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b}\TestTypeExtensions
    set vs14_configExtKey64=HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\VisualStudio\14.0_Config\EnterpriseTools\QualityTools\TestTypes\{13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b}\TestTypeExtensions
)

:: Keys for both 32 and 64-bit
set vs14ExtKey=HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\14.0\EnterpriseTools\QualityTools\TestTypes\{13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b}\TestTypeExtensions
set vs14_configExtKey=HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\14.0_Config\EnterpriseTools\QualityTools\TestTypes\{13cdc9d9-ddb5-4fa4-a97d-d965ccfc6d4b}\TestTypeExtensions


:: Register the TestLooper
set regAttrName=TestLooperAttribute
set regProvider="TestLooper.TestLooperAttribute, TestLooper"
if  %win64% == 1 (
    reg add %vs14ExtKey64%\%regAttrName%        /f /v AttributeProvider /d %regProvider%
    reg add %vs14_ConfigExtKey64%\%regAttrName% /f /v AttributeProvider /d %regProvider%
)
reg add %vs14ExtKey%\%regAttrName%        /f /v AttributeProvider /d %regProvider%
reg add %vs14_ConfigExtKey%\%regAttrName% /f /v AttributeProvider /d %regProvider%

:: Register the TestLooper
set regAttrName=TestLooperAttribute
set regProvider="TestLooper.TestLooperAttribute, TestLooper"
if win64 == 1 (
    reg add %vs14ExtKey64%\%regAttrName%        /f /v AttributeProvider /d %regProvider%
    reg add %vs14_ConfigExtKey64%\%regAttrName% /f /v AttributeProvider /d %regProvider%
)
reg add %vs14ExtKey%\%regAttrName%        /f /v AttributeProvider /d %regProvider%
reg add %vs14_ConfigExtKey%\%regAttrName% /f /v AttributeProvider /d %regProvider%


:eof
endlocal
exit /b %errorlevel%
