cd /d "d:\Community\github\CodeX\CodeX-dev\CodeX.Core"
msbuild CodeX.Core.csproj /t:Rebuild /p:Configuration="Release";TargetFramework=v4.0;OutputPath="../bin/Release/v4.0"
msbuild CodeX.Core.csproj /t:Rebuild /p:Configuration="Release";TargetFramework=v4.5;OutputPath="../bin/Release/v4.5"
msbuild CodeX.Core.csproj /t:Rebuild /p:Configuration="Release";TargetFramework=v4.5.1;OutputPath="../bin/Release/v4.5.1"
cd /d "D:\Community\github\CodeX\CodeX-dev\CodeX.Windows.Forms"
msbuild CodeX.Windows.Forms.csproj /t:Rebuild /p:Configuration="Release";TargetFramework=v4.0;OutputPath="../bin/Release/v4.0"
msbuild CodeX.Windows.Forms.csproj /t:Rebuild /p:Configuration="Release";TargetFramework=v4.5;OutputPath="../bin/Release/v4.5"
msbuild CodeX.Windows.Forms.csproj /t:Rebuild /p:Configuration="Release";TargetFramework=v4.5.1;OutputPath="../bin/Release/v4.5.1"