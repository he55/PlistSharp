#!/bin/bash
PACKDIR="packs"

mkdir -p $PACKDIR

dotnet pack -c Release -o $PACKDIR src/PlistSharp/PlistSharp.csproj
