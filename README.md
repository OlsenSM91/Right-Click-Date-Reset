# Right Click Date Reset

This project allows users to reset the date of image files to the current time via a context menu entry.

## Project Structure

- `ResetDateExtension.csproj`: The project file for the .NET application.
- `ResetDateExtension.cs`: The main C# file containing the code for the application.
- `resetdate.iss`: The Inno Setup script for creating an installer for the application.

## Building the Project

1. Ensure you have .NET 7.0 or later installed on your machine.
2. Navigate to the project directory in a command prompt or terminal.
3. Run the following command to build the project:
```bash
dotnet build
```

## Publishing the Project

1. Navigate to the project directory in a command prompt or terminal.
2. Run the following command to publish the project to a `publish` directory:
```bash
dotnet publish -c Release -o publish
```

## Updating the Inno Setup Script for a New Release

1. Ensure you have Inno Setup installed on your machine.
2. Open the `resetdate.iss` file in the Inno Setup Compiler.
3. Update the `OutputDir` and `OutputBaseFilename` directives under the `[Setup]` section if necessary:
```iss
OutputDir=userdocs:Inno Setup Examples Output
OutputBaseFilename=setup
```

4. Update the `Source` directive under the `[Files]` section to point to the new `publish` directory:
```iss
Source: "path\to\your\publish\directory\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
```
Replace `path\to\your\publish\directory` with the actual path to your `publish` directory.

5. Save the changes to the `resetdate.iss` file.
6. Click the "Compile" button in the Inno Setup Compiler to create a new setup executable.
