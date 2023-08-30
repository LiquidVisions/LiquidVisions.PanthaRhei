[![codecov](https://codecov.io/github/LiquidVisions/LiquidVisions.PanthaRhei/branch/master/graph/badge.svg?token=ZCARYN8KZI)](https://codecov.io/github/LiquidVisions/LiquidVisions.PanthaRhei)

[![.NET](https://github.com/LiquidVisions/LiquidVisions.PanthaRhei/actions/workflows/main.yml/badge.svg)](https://github.com/LiquidVisions/LiquidVisions.PanthaRhei/actions/workflows/nuget.yaml)

# PanthaRhei

PanthaRhei is a code generation tool designed to improve software stability and evolvability by providing a pluggable and customizable framework for code expansion. The project is inspired by the scientific research 'Normalized Systems'

## Features

- Code generation for creating software artifacts
- Pluggable architecture for custom expanders
- Easy customization and extension of the code generation process
- Attempts to enhance software stability and evolvability

## How It Works

PanthaRhei operates based on a code generator that takes input models and applies expanders to generate code artifacts. The expanders, which can be customized and extended, provide specific rules and logic for generating code that aligns with the principles of software stability and evolvability.

## Getting Started

The project is currently in an experimental phase and is, therefore, not yet published publically yet.
<!--- 
To install, make sure you have a valid token to access the private github repository. You can obtain this token by emailing gerco.koks@outlook.com, and requesting the token. Add a new source to the global nuget.config using the following commandline:
dotnet nuget add source https://nuget.pkg.github.com/liquidvisions/index.json --name github --password <<password here>> --username <<username here>>
Now add PanthaRhei as a dotnet tool, making it available using the flux command.
-->

<!--
COMMAND TO INSTALL NEW DOTNET TEMPLATES:
	- dotnet new install C:\Dev\LiquidVisions.PanthaRhei\Templates\Expander --force
-->
