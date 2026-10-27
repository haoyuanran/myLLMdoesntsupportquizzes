@echo off
rem Builds dist\JsonQuiz.exe using the C# compiler that ships with Windows (.NET Framework 4).
cd /d "%~dp0"
set CSC=%WINDIR%\Microsoft.NET\Framework64\v4.0.30319\csc.exe
if not exist "%CSC%" set CSC=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\csc.exe
if not exist "%CSC%" (
  echo Could not find csc.exe. .NET Framework 4 is required.
  exit /b 1
)
if not exist dist mkdir dist
"%CSC%" /nologo /target:winexe /optimize+ /out:dist\JsonQuiz.exe /resource:index.html,index.html /r:System.Windows.Forms.dll launcher\Program.cs
if errorlevel 1 exit /b 1
echo Built dist\JsonQuiz.exe
