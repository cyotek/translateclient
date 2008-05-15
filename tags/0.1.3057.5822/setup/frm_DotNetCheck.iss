[CustomMessages]
frm_DotNetCheck_Caption=Microsoft .NET Framework Check
frm_DotNetCheck_Description=The Microsoft .NET Framework Version 2.0 does not installed on your pc.
frm_DotNetCheck_Line_0=The Microsoft .NET Framework Version 2.0 is required for Translate.Net. 
frm_DotNetCheck_Line_1=Please install it and then rerun setup again.
frm_DotNetCheck_Line_2=You can obtain Microsoft .NET Framework Version 2.0 Redistributable package free of charge from Microsoft web site by button below or seek it at file storages at your local network.
frm_DotNetCheck_Line_3=Redistributable package file will be named dotnetfx.exe or dotnetfx20.exe, size 22.4 Mb
frm_DotNetCheck_Button=Go to Microsoft .NET framework 2.0 Redistributable package download page

ua.frm_DotNetCheck_Caption=Перевірка Microsoft .NET Framework
ua.frm_DotNetCheck_Description=Microsoft .NET Framework Version 2.0 не встановлена на Ваш комп'ютер.
ua.frm_DotNetCheck_Line_0=Microsoft .NET Framework Version 2.0 потрібна для роботи Translate.Net. 
ua.frm_DotNetCheck_Line_1=Встановіть її та перезапустить програму установки.
ua.frm_DotNetCheck_Line_2=Ви можете отримати Microsoft .NET Framework Version 2.0 Redistributable package безкоштовно з сайту Microsoft (кнопка нижче переведе Вас до сторінки завантаження) або знайти її у Вашій локальній мережі 
ua.frm_DotNetCheck_Line_3=Файл буде мати назву dotnetfx.exe або dotnetfx20.exe, розмір 22.4 Mb
ua.frm_DotNetCheck_Button=Перейти на сторінку завантаження .NET framework 2.0 Redistributable package  

ru.frm_DotNetCheck_Caption=Проверка Microsoft .NET Framework
ru.frm_DotNetCheck_Description=Microsoft .NET Framework Version 2.0 не установлена на Ваш компьютер
ru.frm_DotNetCheck_Line_0=Microsoft .NET Framework Version 2.0 необходима для роботы Translate.Net. 
ru.frm_DotNetCheck_Line_1=Установите её и перезапустите программу установки.
ru.frm_DotNetCheck_Line_2=Вы можете получить Microsoft .NET Framework Version 2.0 Redistributable package бесплатно с сайта Microsoft (кнопка ниже переведет Вас к странице загрузки) или найти её в Вашей локальной сети 
ru.frm_DotNetCheck_Line_3=Файл будет называться dotnetfx.exe или dotnetfx20.exe, размер 22.4 Mb
ru.frm_DotNetCheck_Button=Перейти на страницу загрузки .NET framework 2.0 Redistributable package  

[Code]


{ frm_DotNetCheck_Activate }

procedure frm_DotNetCheck_Activate(Page: TWizardPage);
begin
  WizardForm.NextButton.Enabled := False;
  WizardForm.BackButton.Enabled := False;
end;

{ frm_DotNetCheck_ShouldSkipPage }

function frm_DotNetCheck_ShouldSkipPage(Page: TWizardPage): Boolean;
begin
  Result := IsDotNetInstalled();
end;

{ frm_DotNetCheck_BackButtonClick }

function frm_DotNetCheck_BackButtonClick(Page: TWizardPage): Boolean;
begin
  Result := False;
end;

{ frm_DotNetCheck_NextkButtonClick }

function frm_DotNetCheck_NextButtonClick(Page: TWizardPage): Boolean;
begin
  Result := False;
end;

{ frm_DotNetCheck_CancelButtonClick }

procedure frm_DotNetCheck_CancelButtonClick(Page: TWizardPage; var Cancel, Confirm: Boolean);
begin
  // enter code here...
end;

procedure bGoToMicrosoft_Click(Sender: TObject);
var
 ErrorCode : Integer; 
begin
  ShellExec('open', 'http://www.microsoft.com/downloads/details.aspx?FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5', '', '', SW_SHOW, ewNoWait, ErrorCode);
end;

{ frm_DotNetCheck_CreatePage }

function frm_DotNetCheck_CreatePage(PreviousPageId: Integer): Integer;
var
  Page: TWizardPage;
  mInfo: TMemo;
  bGoToMicrosoft: TButton;  
begin
  Page := CreateCustomPage(
    PreviousPageId,
    ExpandConstant('{cm:frm_DotNetCheck_Caption}'),
    ExpandConstant('{cm:frm_DotNetCheck_Description}')
  );

{ mInfo }
  mInfo := TMemo.Create(Page);
  with mInfo do
  begin
    Parent := Page.Surface;
    Left := ScaleX(0);
    Top := ScaleY(0);
    Width := ScaleX(413);
    Height := ScaleY(137);
    Lines.Add(ExpandConstant('{cm:frm_DotNetCheck_Line_0}'));
    Lines.Add(ExpandConstant('{cm:frm_DotNetCheck_Line_1}'));
    Lines.Add('');
    Lines.Add(ExpandConstant('{cm:frm_DotNetCheck_Line_2}'));
    Lines.Add('');
    Lines.Add(ExpandConstant('{cm:frm_DotNetCheck_Line_3}'));
    ReadOnly := True;
    TabOrder := 0;    
  end;
  
  bGoToMicrosoft := TButton.Create(Page);  
  with bGoToMicrosoft do
  begin
    Parent := Page.Surface;  
    Left :=  ScaleX(0);
    Top := ScaleY(152);
    Width :=  ScaleX(413);
    Height := ScaleY(23);
    Caption := ExpandConstant('{cm:frm_DotNetCheck_Button}');
    TabOrder := 1;
    OnClick := @bGoToMicrosoft_Click;
  end;
  with Page do
  begin
    OnActivate := @frm_DotNetCheck_Activate;
    OnShouldSkipPage := @frm_DotNetCheck_ShouldSkipPage;
    OnBackButtonClick := @frm_DotNetCheck_BackButtonClick;
    OnNextButtonClick := @frm_DotNetCheck_NextButtonClick;
    OnCancelButtonClick := @frm_DotNetCheck_CancelButtonClick;
  end;

  Result := Page.ID;
end;

{ frm_DotNetCheck_InitializeWizard }

procedure InitializeWizard();
begin
  frm_DotNetCheck_CreatePage(wpWelcome);
end;


[dir]