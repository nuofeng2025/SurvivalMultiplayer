@SET EXCEL_FOLDER=.\..\配置表
@SET JSON_FOLDER=.\json
@SET CS_FOLDER=.\cs
@SET EXE=.\excel2json.exe

@ECHO Converting excel files in folder %EXCEL_FOLDER% ...
for /f "delims=" %%i in ('dir /b /a-d /s %EXCEL_FOLDER%\*.xlsx') do (
    @echo   processing %%~nxi 
    @CALL %EXE% --excel %EXCEL_FOLDER%\%%~nxi --json %JSON_FOLDER%\%%~ni.json  --csharp  %CS_FOLDER%\%%~ni.cs --header 3
)



set curDir=%~dp0
set c_cs_targetDir=%curDir%..\..\client\NineKingsA\Assets\_Scripts\Framework\Controllers\Config\Configmodel\
set c_cs_sourceDir=%curDir%cs\
echo "[log] c_cs_targetDir : %c_cs_targetDir%"
echo "[log] c_cs_sourceDir : %c_cs_sourceDir%"
xcopy "%c_cs_sourceDir%\*.*" "%c_cs_targetDir%\" /E /I /C /Y

set c_json_targetDir=%curDir%..\..\client\NineKingsA\Assets\_AddressRes\Config\
set c_json_sourceDir=%curDir%json\
echo "[log] c_json_targetDir : %c_json_targetDir%"
echo "[log] c_json_sourceDir : %c_json_sourceDir%"
xcopy "%c_json_sourceDir%\*.*" "%c_json_targetDir%\" /E /I /C /Y

set s_cs_targetDir=%curDir%..\..\client\Server\Model\Module\Demo\Config\
set s_cs_sourceDir=%curDir%cs\
echo "[log] s_cs_targetDir : %s_cs_targetDir%"
echo "[log] s_cs_sourceDir : %s_cs_sourceDir%"
xcopy "%s_cs_sourceDir%\*.*" "%s_cs_targetDir%\" /E /I /C /Y

set s_json_targetDir=%curDir%..\..\client\JsonData\
set s_json_sourceDir=%curDir%json\
echo "[log] s_json_targetDir : %s_json_targetDir%"
echo "[log] s_json_sourceDir : %s_json_sourceDir%"
xcopy "%s_json_sourceDir%\*.*" "%s_json_targetDir%\" /E /I /C /Y
 
echo successfully.
pause

echo 请按任意键开始或结bai束
pause>nul