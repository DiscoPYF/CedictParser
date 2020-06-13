# CedictParser

A C# .Net Standard 2.0 library for parsing the [CC-CEDICT](https://cc-cedict.org/wiki/) format.

## Install

Install the [NuGet package](https://www.nuget.org/packages/CedictParser).

.Net CLI
```
dotnet add package CedictParser
```

Package Manager:
```
Install-Package CedictParser
```

Package Reference:
```
<PackageReference Include="CedictParser" Version="1.0.0" />
```

## Usage

Create an instance of the CedictParser class with a filepath or a `StreamReader`.

```csharp
using (var parser = new CedictParser(filepath))
{
}
```

```csharp
using (var reader = new StreamReader(filepath, Encoding.UTF8))
{
    var parser = new CedictParser(reader);
}
```

`CedictParser` contains synchronous and asynchronous methods to read one or all entries.

Read one entry:

```csharp
// sync
CedictEntry entry = parser.Read();

// async
CedictEntry entry = await parser.ReadAsync();
```

Read all remaining entries:

```csharp
// sync
IList<CedictEntry> entries = parser.ReadToEnd();

// async
IList<CedictEntry> entries = await parser.ReadToEndAsync();
```
