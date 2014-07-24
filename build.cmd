msbuild /p:Configuration=Release
.\.nuget\NuGet.exe pack MapControl\MapControl.WPF.csproj -Prop Configuration=Release
.\.nuget\NuGet.exe pack TileSharpLayer\TileSharpLayer.csproj -Prop Configuration=Release
