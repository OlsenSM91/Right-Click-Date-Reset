[Setup]
AppName=Right Click Date Reset
AppVersion=1.0
DefaultDirName={pf}\GH Design\Right Click Date Reset
DefaultGroupName=Right Click Date Reset
UninstallDisplayIcon={app}\NewResetDateExtension.exe
OutputDir=userdocs:Inno Setup Examples Output
OutputBaseFilename=setup
Compression=lzma
SolidCompression=yes

[Files]
Source: "C:\Users\Olsens\Desktop\Coding Projects\Right Click Image Date Reset\NewResetDateExtension\bin\Release\net7.0\win-x64\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Registry]
Root: HKCR; Subkey: "SystemFileAssociations\image\shell\Reset Date\command"; ValueType: string; ValueName: ""; ValueData: """{app}\NewResetDateExtension.exe"" ""%1"""
Root: HKCR; Subkey: "Directory\shell\Recursive Reset Date\command"; ValueType: string; ValueName: ""; ValueData: """{app}\NewResetDateExtension.exe"" ""%1"""

[Icons]
Name: "{group}\Right Click Date Reset"; Filename: "{app}\NewResetDateExtension.exe"