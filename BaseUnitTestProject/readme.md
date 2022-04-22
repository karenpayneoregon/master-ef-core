# About

Test methods for working with Entity Framework Core

There are a few test methods that do not actually perform test but instead are code samples which could had been placed into a console project instead but wanted to keep the code together.

The following tag in the project file `.csproj` eliminates language folders in the debug folder. This is not just for unit test but for any project type.

```xml
<SatelliteResourceLanguages>en</SatelliteResourceLanguages>
```

# Requires

- .NET Core 5 or higher
- Visual Studio 2019 or higher
- NorthWind2022 to be installed using the following [script](https://github.com/karenpayneoregon/class-data-scripts/blob/master/north2022.sql).