@echo off
setlocal

set PACKDIR="packages"

mkdir %PACKDIR%
dotnet pack -c Release -o %PACKDIR% PlistSharp/PlistSharp.csproj
