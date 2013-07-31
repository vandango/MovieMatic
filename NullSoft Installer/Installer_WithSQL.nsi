;NSIS Modern User Interface
;Toenda Software Development - MovieMatic Installer Script
;Written by Jonathan Naumann

;--------------------------------
;Include Modern UI

  !include "MUI.nsh"
  !include WordFunc.nsh
  !include "LogicLib.nsh"
  !include "MUI2.nsh"

  !insertmacro VersionCompare

;--------------------------------
;General

  ;Name and file
  !define VERSION "1.3.2"
  !define MIN_FRA_MAJOR "4"
  !define MIN_FRA_MINOR "0"
  !define MIN_FRA_BUILD "*"
  Name "MovieMatic"
  OutFile "MovieMatic_Installer_MSSQL_${VERSION}.exe"

  ;Default installation folder
  InstallDir "$PROGRAMFILES\Toenda\MovieMatic"
  
  !define INSTALL_NAME "MovieMatic"
  !define STARTMENU_NAME "Toenda\MovieMatic"
  !define STARTMENU_BASE "Toenda"
  !define APP_DIR "$LOCALAPPDATA\Toenda\MovieMatic"

  RequestExecutionLevel admin



;--------------------------------
;Variables

  Var STARTMENU_FOLDER
  ;Var ACCESSKEY


;--------------------------------
;Interface Settings

  !define MUI_HEADERIMAGE
  !define MUI_HEADERIMAGE_BITMAP "Resource\logo_small.bmp" ; optional
  !define MUI_ABORTWARNING


;--------------------------------
;Functions

Function .onInit
  Call AbortIfBadFramework
  
  Call CheckMinSQLVersion
  Push $0
  
  !define SQL_VERSION "$0"

  ;MessageBox MB_OK|MB_ICONEXCLAMATION "$0"
FunctionEnd


Function CheckMinSQLVersion
  ClearErrors

  ReadRegStr $0 HKLM "SOFTWARE\Microsoft\Microsoft SQL Server\90\Tools\ClientSetup\CurrentVersion" "CurrentVersion"
  IfErrors SQLServerNotFound SQLServerFound

  SQLServerFound:
  	;Check the first digit of the version; must be 9
  	StrCpy $1 $0 1
  	StrCmp $1 "9" SQLServer2005Found SQLServerVersionError
  	Goto ExitCheckMinSQLVersion

  SQLServer2005Found:
  	StrCpy $0 "MSSQL_2005"
  	Goto ExitCheckMinSQLVersion

  SQLServerVersionError:
  	;MessageBox MB_OK|MB_ICONEXCLAMATION  "This product requires a minimum SQLServer version of 9 (SQLServer 2005); detected version $0. Setup will abort."
  	StrCpy $0 "OLD_MSSQL"
  	Goto ExitCheckMinSQLVersion

  SQLServerNotFound:
  	;MessageBox MB_OK|MB_ICONEXCLAMATION  "SQLServer was not detected; this is required for installation. Setup will abort."
  	StrCpy $0 "NO_MSSQL"
  	Goto ExitCheckMinSQLVersion

  ExitCheckMinSQLVersion:
FunctionEnd


;
;;NB Use an asterisk to match anything.
;Call AbortIfBadFramework
;; No pops. It just aborts inside the function, or returns if all is well.
;; Change this if you like.
Function AbortIfBadFramework

  ;Save the variables in case something else is using them
  Push $0
  Push $1
  Push $2
  Push $3
  Push $4
  Push $R1
  Push $R2
  Push $R3
  Push $R4
  Push $R5
  Push $R6
  Push $R7
  Push $R8

  StrCpy $R5 "0"
  StrCpy $R6 "0"
  StrCpy $R7 "0"
  StrCpy $R8 "0.0.0"
  StrCpy $0 0

  loop:

  ;Get each sub key under "SOFTWARE\Microsoft\NET Framework Setup\NDP"
  EnumRegKey $1 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP" $0
  StrCmp $1 "" done ;jump to end if no more registry keys
  IntOp $0 $0 + 1
  StrCpy $2 $1 1 ;Cut off the first character
  StrCpy $3 $1 "" 1 ;Remainder of string

  ;Loop if first character is not a 'v'
  StrCmpS $2 "v" start_parse loop

  ;Parse the string
  start_parse:
  StrCpy $R1 ""
  StrCpy $R2 ""
  StrCpy $R3 ""
  StrCpy $R4 $3

  StrCpy $4 1

  parse:
  StrCmp $3 "" parse_done ;If string is empty, we are finished
  StrCpy $2 $3 1 ;Cut off the first character
  StrCpy $3 $3 "" 1 ;Remainder of string
  StrCmp $2 "." is_dot not_dot ;Move to next part if it's a dot

  is_dot:
  IntOp $4 $4 + 1 ; Move to the next section
  goto parse ;Carry on parsing

  not_dot:
  IntCmp $4 1 major_ver
  IntCmp $4 2 minor_ver
  IntCmp $4 3 build_ver
  IntCmp $4 4 parse_done

  major_ver:
  StrCpy $R1 $R1$2
  goto parse ;Carry on parsing

  minor_ver:
  StrCpy $R2 $R2$2
  goto parse ;Carry on parsing

  build_ver:
  StrCpy $R3 $R3$2
  goto parse ;Carry on parsing

  parse_done:

  IntCmp $R1 $R5 this_major_same loop this_major_more
  this_major_more:
  StrCpy $R5 $R1
  StrCpy $R6 $R2
  StrCpy $R7 $R3
  StrCpy $R8 $R4

  goto loop

  this_major_same:
  IntCmp $R2 $R6 this_minor_same loop this_minor_more
  this_minor_more:
  StrCpy $R6 $R2
  StrCpy $R7 R3
  StrCpy $R8 $R4
  goto loop

  this_minor_same:
  IntCmp R3 $R7 loop loop this_build_more
  this_build_more:
  StrCpy $R7 $R3
  StrCpy $R8 $R4
  goto loop

  done:

  ;Have we got the framework we need?
  IntCmp $R5 ${MIN_FRA_MAJOR} max_major_same fail end
  max_major_same:
  IntCmp $R6 ${MIN_FRA_MINOR} max_minor_same fail end
  max_minor_same:
  IntCmp $R7 ${MIN_FRA_BUILD} end fail end

  fail:
  StrCmp $R8 "0.0.0" no_framework
  goto wrong_framework

  no_framework:
  MessageBox MB_OK|MB_ICONSTOP "Installation failed.$\n$\n\
         This software requires Windows Framework version \
         ${MIN_FRA_MAJOR}.${MIN_FRA_MINOR}.${MIN_FRA_BUILD} or higher.$\n$\n\
         No version of Windows Framework is installed.$\n$\n\
         Please update your computer at http://windowsupdate.microsoft.com/."
  abort

  wrong_framework:
  MessageBox MB_OK|MB_ICONSTOP "Installation failed!$\n$\n\
         This software requires Windows Framework version \
         ${MIN_FRA_MAJOR}.${MIN_FRA_MINOR}.${MIN_FRA_BUILD} or higher.$\n$\n\
         The highest version on this computer is $R8.$\n$\n\
         Please update your computer at http://windowsupdate.microsoft.com/."
  abort

  end:

  ;Pop the variables we pushed earlier
  Pop $R8
  Pop $R7
  Pop $R6
  Pop $R5
  Pop $R4
  Pop $R3
  Pop $R2
  Pop $R1
  Pop $4
  Pop $3
  Pop $2
  Pop $1
  Pop $0
FunctionEnd


Function StrTok
  Exch $R1
  Exch 1
  Exch $R0
  Push $R2
  Push $R3
  Push $R4
  Push $R5

  ;R0 fullstring
  ;R1 tokens
  ;R2 len of fullstring
  ;R3 len of tokens
  ;R4 char from string
  ;R5 testchar

  StrLen $R2 $R0
  IntOp $R2 $R2 + 1

  loop1:
    IntOp $R2 $R2 - 1
    IntCmp $R2 0 exit

    StrCpy $R4 $R0 1 -$R2

    StrLen $R3 $R1
    IntOp $R3 $R3 + 1

    loop2:
      IntOp $R3 $R3 - 1
      IntCmp $R3 0 loop1

      StrCpy $R5 $R1 1 -$R3

      StrCmp $R4 $R5 Found
    Goto loop2
  Goto loop1

  exit:
  ;Not found!!!
  StrCpy $R1 ""
  StrCpy $R0 ""
  Goto Cleanup

  Found:
  StrLen $R3 $R0
  IntOp $R3 $R3 - $R2
  StrCpy $R1 $R0 $R3

  IntOp $R2 $R2 - 1
  IntOp $R3 $R3 + 1
  StrCpy $R0 $R0 $R2 $R3

  Cleanup:
  Pop $R5
  Pop $R4
  Pop $R3
  Pop $R2
  Exch $R0
  Exch 1
  Exch $R1

FunctionEnd


;--------------------------------
;Pages

  !insertmacro MUI_PAGE_WELCOME

  !define MUI_PAGE_HEADER_TEXT "Changelog"
  !define MUI_PAGE_HEADER_SUBTEXT "Hier können sie die Änderungen an der aktuellen Version einsehen."
  ;!define MUI_LICENSEPAGE_TEXT_TOP "Changelog"
  !define MUI_LICENSEPAGE_TEXT_BOTTOM "Wenn sie selber Fehler finden oder Wünsche haben, schreiben sie eine eMail an info@toenda.com."
  !define MUI_LICENSEPAGE_BUTTON "Weiter >"
  !insertmacro MUI_PAGE_LICENSE "Resource\Changelog.txt"

  !insertmacro MUI_PAGE_LICENSE "Resource\License.rtf"
  ;!insertmacro MUI_PAGE_COMPONENTS
  !insertmacro MUI_PAGE_DIRECTORY

  ;Page Custom GetAccessKey
    
  ;Start Menu Folder Page Configuration
  !define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU" 
  !define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\Toenda\MovieMatic" 
  !define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Toenda\MovieMatic"
  
  !insertmacro MUI_PAGE_STARTMENU Application $STARTMENU_FOLDER
  
  !insertmacro MUI_PAGE_INSTFILES
  
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES


;--------------------------------
;Languages
 
  !insertmacro MUI_LANGUAGE "German"


;--------------------------------
;Installer Sections

Section ""
  SetOutPath "$INSTDIR"
  
  ;Files and folders
  CreateDirectory ${APP_DIR}
  
  File "Files\AODL.dll"
  File "Files\ICSharpCode.SharpZipLib.dll"
  File "Files\MovieMatic.exe"
  File "Files\MovieMatic.exe.config"
  File "Files\MovieMaticInterface.dll"
  File "Files\Toenda.Foundation.BackupRestoreHandler.dll"
  File "Files\Toenda.Foundation.Controls.dll"
  File "Files\Toenda.Foundation.dll"
  
  ${If} ${SQL_VERSION} == "MSSQL_2005"
    MessageBox MB_OK|MB_ICONSTOP "Microsoft SQLServer 2005 Express Edition ist bereits auf diesem System vorhanden, Installation übersprungen."
  ${Else}
    File "Additionals\SQLEXPR32_DEU.EXE"

    ;nsExec::Exec 'SQLEXPR32_DEU.EXE /qb ADDLOCAL=ALL SQLAUTOSTART=1 SECURITYMODE=SQL SAPWD=moviematic'
    ExecCmd::exec '"$INSTDIR\SQLEXPR32_DEU.EXE" >ExecCmd.log' '/qb ADDLOCAL=ALL SQLAUTOSTART=1 SECURITYMODE=SQL SAPWD=moviematic'
    Pop $0 ; return value - process exit code or error or STILL_ACTIVE (0x103).
    ExecCmd::wait $0

    ${If} $0 == 0
        MessageBox MB_OK "Microsoft SQLServer 2005 Express Edition konnte nicht installiert werden."
    ${Else}
        MessageBox MB_OK "Microsoft SQLServer 2005 Express Edition wurde erfolgreich installiert"
    ${EndIf}

    ;Delete "$TEMP\SQLEXPR32_DEU.EXE"
  ${EndIf}

  File "/oname=${APP_DIR}\MovieMatic.xml" "Resource\MovieMatic.xml"

  File "Additionals\mm.bak"

  ExecCmd::exec 'osql.exe -U sa -P moviematic -S SQLEXPRESS -Q "RESTORE DATABASE MovieMatic From Disk = $\'$INSTDIR\mm.bak$\' WITH MOVE $\'MovieMatic$\' TO $\'$INSTDIR\mm.mdf$\', MOVE $\'MovieMatic_Log$\' TO $\'$INSTDIR\mm.ldf$\'$\" >$INSTDIR\ExecCmd.log' ''
    Pop $0 ; return value - process exit code or error or STILL_ACTIVE (0x103).
    ExecCmd::wait $0

    ;${If} $0 == 0
    ;    MessageBox MB_OK "Die MovieMatic Datenbank konnte nicht an den SQL Server gehängt werden."
    ;${Else}
    ;    MessageBox MB_OK "Die MovieMatic Datenbank wurde erfolgreich installiert"
    ;${EndIf}
    DetailPrint "($0) Die MovieMatic Datenbank wurde erfolgreich nach $INSTDIR kopiert..."


  ;Delete "=$TEMP\mm.bak"

  ;Store installation folder
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${INSTALL_NAME}" "DisplayName" "${INSTALL_NAME}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${INSTALL_NAME}" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${INSTALL_NAME}" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${INSTALL_NAME}" "NoRepair" 1

  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"

  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application

    ;Create shortcuts
    CreateDirectory "$SMPROGRAMS\${STARTMENU_NAME}"
    CreateShortCut "$SMPROGRAMS\${STARTMENU_NAME}\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
    CreateShortCut "$SMPROGRAMS\${STARTMENU_NAME}\MovieMatic.lnk" "$INSTDIR\MovieMatic.exe"

  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd


;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecBase ${LANG_GERMAN} "Die Basisdateien für MovieMatic. Wenn Sie nur ein Update durchführen, benötigen Sie nur diese Option."
  LangString DESC_SecSQL ${LANG_GERMAN} "Der Datenbank Server für MovieMatic.$\nFür ein Update wird diese Option nicht benötigt."
  LangString DESC_SecDB ${LANG_GERMAN} "Die Datenbank für MovieMatic.$\nFür ein Update wird diese Option nicht benötigt."

  ;Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
    ;!insertmacro MUI_DESCRIPTION_TEXT ${SecBase} $(DESC_SecBase)
    ;!insertmacro MUI_DESCRIPTION_TEXT ${SecSQL} $(DESC_SecSQL)
    ;!insertmacro MUI_DESCRIPTION_TEXT ${SecDB} $(DESC_SecDB)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END


;--------------------------------
;Uninstaller Section

Section "Uninstall"
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${INSTALL_NAME}"

  Delete "$INSTDIR\*.*"
  Delete "${APP_DIR}\*.*"

  RMDir /r "$INSTDIR"
  RMDir /r "${APP_DIR}\Temp"
  RMDir /r "${APP_DIR}"
  
  Delete "$SMPROGRAMS\${STARTMENU_NAME}\Uninstall.lnk"
  RMDir "$SMPROGRAMS\${STARTMENU_NAME}"
  RMDir "$SMPROGRAMS\${STARTMENU_BASE}"
SectionEnd

