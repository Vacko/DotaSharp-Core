# DotaSharp Core

DotaSharp provides API that you can make use of to create assemblies for Dota 2.

## Features
- Drawing information on the screen (manabars, spells, items, ...)
- Controlling players hero or his units (using spells and items, attacking/moving, ...)
- Unlocking game cheat CVARs and editing their values (camera distance, fog, ...)

## Usage

You need to create bootstrap a native DLL that executes code which invokes the .NET runtime that causes 
managed assembly (DotaSharp Core) to be loaded inside the target process. [Example](https://github.com/Vacko/Managed-code-injection/tree/master/Bootstrap)

## Copyright and License

Copyright 2016 Vacko

Licensed under the [MIT licence](LICENSE)
