#!/usr/bin/env bash

OUTPUT_DIR="packages"
mkdir $OUTPUT_DIR

dotnet pack -c Release -o $OUTPUT_DIR --nologo PlistSharp/PlistSharp.csproj
