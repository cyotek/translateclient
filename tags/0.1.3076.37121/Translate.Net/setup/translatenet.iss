#include "detect_dotnet2.iss"
#include "frm_DotNetCheck.iss"

#define AppVersion GetFileVersion("..\bin\Translate.Net.exe")
#define AppName "Translate.Net"


[Setup]
AppVersion={#AppVersion}
AppName={#AppName}
AppVerName={cm:NameAndVersion,{#AppName},{#AppVersion}} 
UninstallDisplayName={#AppName}
VersionInfoVersion={#AppVersion}

AppID=SAUTRANSLATENET
AppMutex=SAUTRANSLATENET

UninstallDisplayIcon={app}\Translate.Net.exe
DefaultDirName={pf}\SAU KP\Translate.Net\
DefaultGroupName=Translate.Net
OutputBaseFilename=Translate.Net.{#AppVersion}


Compression=lzma
SolidCompression=yes

ShowLanguageDialog=auto
;ShowLanguageDialog=yes
MinVersion=4.1.1998,5.00.2195
AppCopyright=Copyright © Oleksii Prudkyi, 2008
AppPublisher=Oleksii Prudkyi
AppPublisherURL=http://translateclient.googlepages.com/
AppSupportURL=http://translateclient.googlepages.com/
AppUpdatesURL=http://translateclient.googlepages.com/


ShowTasksTreeLines=false
DisableDirPage=false
DisableProgramGroupPage=true

DisableReadyMemo=false
AlwaysShowDirOnReadyPage=true

AlwaysShowComponentsList=false
DisableReadyPage=true
ChangesAssociations=false
WizardImageStretch=no

;LicenseFile=texts\license\eula.rtf
;InfoBeforeFile=texts\info\info.rtf
LicenseFile=texts\info\info.rtf

PrivilegesRequired=poweruser


[Languages]
Name: en; MessagesFile: "compiler:Default.isl"
Name: bs; MessagesFile: "compiler:Languages\Basque.isl"
Name: br; MessagesFile: "compiler:Languages\BrazilianPortuguese.isl"
Name: ct; MessagesFile: "compiler:Languages\Catalan.isl"
Name: ch; MessagesFile: "compiler:Languages\Czech.isl"
Name: dn; MessagesFile: "compiler:Languages\Danish.isl"
Name: nl; MessagesFile: "compiler:Languages\Dutch.isl"
Name: fn; MessagesFile: "compiler:Languages\Finnish.isl"
Name: fr; MessagesFile: "compiler:Languages\French.isl"
Name: de; MessagesFile: "compiler:Languages\German.isl"
Name: he; MessagesFile: "compiler:Languages\Hebrew.isl"
Name: hu; MessagesFile: "compiler:Languages\Hungarian.isl"
Name: it; MessagesFile: "compiler:Languages\Italian.isl"
Name: nr; MessagesFile: "compiler:Languages\Norwegian.isl"
Name: pl; MessagesFile: "compiler:Languages\Polish.isl"
Name: pr; MessagesFile: "compiler:Languages\Portuguese.isl"
Name: ru; MessagesFile: "compiler:Languages\Russian.isl"; LicenseFile: texts\info\info.ru.rtf
Name: sk; MessagesFile: "compiler:Languages\Slovak.isl"
Name: sl; MessagesFile: "compiler:Languages\Slovenian.isl"
Name: sp; MessagesFile: "compiler:Languages\Spanish.isl"
Name: ua; MessagesFile: "compiler:Languages\Ukrainian.isl"; LicenseFile: texts\info\info.ua.rtf




[Files]
Source: ..\bin\FreeCL.UI.*; DestDir: {app}
Source: ..\bin\FreeCL.RTL.*; DestDir: {app}
Source: ..\bin\FreeCL.Forms.*; DestDir: {app}
Source: ..\bin\translate.*; DestDir: {app}
Source: ..\bin\translate.net.*; DestDir: {app}
Source: ..\source\Translate\lang\*.lng; DestDir: {app}\lang
Source: texts\info\*.rtf; DestDir: {app}
Source: texts\license\*.rtf; DestDir: {app}
Source: texts\privacy\*.rtf; DestDir: {app}
Source: texts\thanks.txt; DestDir: {app}

[Registry]
Root: HKCU; SubKey: Software\Microsoft\Windows\CurrentVersion\Run; ValueType: string; ValueName: Translate.Net; ValueData: {app}\Translate.Net.exe -skipsplash

[Run]
Filename: "{app}\Translate.Net.exe"; Parameters: "-nohide"; WorkingDir: "{app}"; Description: "Run Translate.Net"; Flags: nowait postinstall;


[Icons]
Name: {commondesktop}\Translate.Net; Filename: {app}\Translate.Net.exe; WorkingDir: {app};Parameters: "-nohide"
Name: {group}\Translate.Net; Filename: {app}\Translate.Net.exe; WorkingDir: {app}; Flags: createonlyiffileexists; Parameters: "-nohide"
;Name: {userappdata}\Microsoft\Internet Explorer\Quick Launch\Translate.Net; Filename: {app}\Translate.Net.exe; WorkingDir: {app}; Flags: createonlyiffileexists
Name: {group}\{cm:UninstallProgram,Translate.Net}; Filename: {uninstallexe} 


