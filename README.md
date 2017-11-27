Documentation: [http://correlatorsharp.github.io/dotnet/CorrelatorSharp.html](http://correlatorsharp.github.io/dotnet/CorrelatorSharp.html)


# Releasing a new version

1. Update the version number in `appveyor.yml`
1. Review and update release notes in the GitHub deployment section in `appveyor.yml` 
1. Create a release Git tag `vX.Y.Z`. This will trigger NuGet publish in AppVeyor.