; -- Setup.iss --
; Installer for Rocrail starter
;

#define AppId      "MgvLoconetToolbox_v1"
#define AppName    "LocoNet Toolbox"
#define AppShortName "LocoNet Toolbox"
#define AppExeName   "LocoNetToolbox"
#define AppVersion GetFileVersion("..\Build\Application\LocoNetToolbox.exe")

[Setup]
AppId={#AppId}
AppName={#AppName}
AppCopyright=Modelspoorgroep Venlo
AppVerName={#AppName} {#AppVersion}
AppVersion={#AppVersion}
AppPublisher=MGV
AppPublisherURL=http://www.modelspoorgroepvenlo.nl
DefaultDirName={pf}\MGV\LocoNet
DefaultGroupName={#AppName}
DisableProgramGroupPage=yes
SetupIconFile=..\..\Source\WinApp\App.ico
WizardImageFile=..\..\Source\Graphics\Install\banner-164x314.bmp
WizardSmallImageFile=..\..\Source\Graphics\Install\banner-55x58.bmp
UninstallDisplayIcon={app}\{#AppExeName}.exe
Compression=lzma
SolidCompression=yes
OutputDir=..\Setup
OutputBaseFileName=LocoNetToolboxSetup-{#AppVersion}
VersionInfoDescription={#AppName} installer
VersionInfoVersion={#AppVersion}
AllowUNCPath=no
ArchitecturesInstallIn64BitMode=x64
ArchitecturesAllowed=x86 x64
SourceDir=..\Build\Application

[Files]
Source: "LocoNetToolbox.exe"; DestDir: "{app}"; Flags: replacesameversion;
Source: "LocoNetToolbox.exe.config"; DestDir: "{app}"; 

[Icons]
Name: "{group}\{#AppShortName}"; Filename: "{app}\LocoNetToolbox.exe"; WorkingDir: "{app}"

[Run]
Filename: "{dotnet40}\ngen.exe"; Parameters: "install ""{app}\LocoNetToolbox.exe"""; StatusMsg: {cm:Optimize}; Flags: runhidden;
Filename: "{app}\LocoNetToolbox.exe"; Description: "{cm:StartApp}"; Flags: postinstall nowait skipifsilent;

[UninstallDelete]
Type: filesandordirs; Name: "{app}";

[UninstallRun]
Filename: "{dotnet40}\ngen.exe"; Parameters: "uninstall ""{app}\LocoNetToolbox.exe"""; StatusMsg: {cm:UnOptimize}; Flags: runhidden;

[CustomMessages]
StartApp=Start {#AppName}
InstallDotNet=Install the Microsoft.NET 4.0 Framework first.
Optimize=Optimizing performance
UnOptimize=Cleanup performance optimizations

[Code]
const
  dotnet40URL = 'http://download.microsoft.com/download/9/5/A/95A9616B-7A37-4AF6-BC36-D6EA96C8DAAE/dotNetFx40_Full_x86_x64.exe';

function InitializeSetup(): Boolean;
var
  msgRes : integer;
  errCode : integer;

begin
  Result := true;
  // Check for required dotnetfx 4.0 installation
  if (not RegKeyExists(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\full')) then begin
    msgRes := MsgBox(CustomMessage('InstallDotNet'), mbError, MB_OKCANCEL);
    if(msgRes = 1) then begin
      ShellExec('Open', dotnet40URL, '', '', SW_SHOW, ewNoWait, errCode);
    end;
    Abort();
  end;
end;
