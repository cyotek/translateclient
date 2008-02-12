[Code]

function IsDotNetInstalled(): Boolean;
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
end;



[Dirs]
