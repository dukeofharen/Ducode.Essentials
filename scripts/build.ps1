Param(
    [string]$nugetApiKey
)

function Assert-Cmd-Ok {
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Build not succeeded... See errors"
        Exit -1
    }
}

$rootPath = "$PSScriptRoot\.."
$srcPath = "$rootPath\src"
$solutionFile = "$srcPath\Ducode.Essentials.sln"

# Remove all bin and obj folders
Write-Host "Cleaning the solution"
Get-ChildItem $srcPath -include bin, obj -Recurse | foreach ($_) { remove-item $_.fullname -Force -Recurse }

# Perform a debug build
& dotnet build $solutionFile /p:DebugType=Full

# Run unit tests
$unitTestProjects = Get-ChildItem -Path $srcPath -Filter *.Tests.csproj -Recurse
Write-Host "Running unit tests"
foreach ($unitTest in $unitTestProjects) {
    Write-Host $unitTest

    & dotnet restore $unitTest.FullName
    Assert-Cmd-Ok

    & dotnet test $unitTest.FullName
    Assert-Cmd-Ok
}

# Build the NuGet packges
$changelogs = Get-ChildItem -Path $srcPath -Filter changeLog.txt -Recurse
foreach ($changelog in $changelogs) {
    $changelogPath = $changelog.FullName
    Write-Host $changelogPath
    $contents = Get-Content $changelogPath
    $version = $contents[0].Replace("[", "").Replace("]", "")
    Write-Host "Version found: $version"
    
    # Parse .csproj file
    $directory = $changelog.Directory
    $csprojs = Get-ChildItem -Path $directory -Filter *.csproj
    foreach ($csproj in $csprojs) {
        $csprojPath = $csproj.FullName
        Write-Host "Patching and building file $csprojPath"
        [xml]$xml = Get-Content $csprojPath
        $xml.SelectSingleNode("/Project/PropertyGroup[1]").Version = $version
        $xml.Save($csprojPath)

        $csprojFolder = $csproj.Directory
        Write-Host "Creating nuget package for $csprojFolder"
        & cmd /C "cd $csprojFolder && dotnet pack -c Release"
        Assert-Cmd-Ok
    }
}

# Publish NuGet package
Write-Host "Publishing NuGet packages"
$nupkgs = Get-ChildItem -Path $srcPath -Filter *.nupkg -Recurse
foreach ($nupkg in $nupkgs) {
    $nupkgPath = $nupkg.FullName
    Write-Host "Publishing NuGet package $nupkgPath"
    & dotnet nuget push $nupkgPath -k $nugetApiKey -s https://api.nuget.org/v3/index.json
}