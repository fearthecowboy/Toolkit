@echo off
pushd %~dp0

call Build-Release.cmd

: pack all the script
tools\nuget pack Common\Common.csproj -symbols -Properties Configuration=Release
tools\nuget pack Windows.Core\Windows.Core.csproj -symbols -Properties Configuration=Release
tools\nuget pack Windows.UI\Windows.UI.csproj -symbols -Properties Configuration=Release
tools\nuget pack CommandLine\CommandLine.csproj -symbols -Properties Configuration=Release
tools\nuget pack Windows.PEBinary\Windows.PEBinary.csproj -symbols -Properties Configuration=Release
tools\nuget pack Windows.Crypto\Windows.Crypto.csproj -symbols -Properties Configuration=Release

move *.nupkg repo

popd

