[Code]
function InitializeSetup(): Boolean;
var
 KeyName, ValueName: String;
 Value : Cardinal;
 ErrorCode : Integer; 
begin
  Result := False;
  KeyName := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v2.0.50727';
  ValueName := 'Install';
  
  if RegQueryDWordValue(HKLM, KeyName, ValueName, Value) then
  begin
    if (Value = 1) then
      Result := True;
  end
  
  if(not Result) then
  begin
    if(MsgBox('The Microsoft .NET Framework Version 2.0 required.' #13 'Do you want to open web page were you can download version for your operating system ?', mbConfirmation, MB_YESNO) = idYes)  then
    begin
	    ShellExec('open', 'http://msdn.microsoft.com/netframework/downloads/updates/default.aspx', '', '', SW_SHOW, ewNoWait, ErrorCode);
    end
  end
end;



[Dirs]
