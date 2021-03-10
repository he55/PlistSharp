#!/usr/bin/env bash

PACKDIR="packages"

mkdir -p $PACKDIR
dotnet pack -c Release -o $PACKDIR PlistSharp/PlistSharp.csproj
