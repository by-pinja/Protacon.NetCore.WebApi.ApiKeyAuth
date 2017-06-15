$project = "Protacon.NetCore.WebApi.ApiKeyAuth"

if(Test-Path $PSScriptRoot\$project\artifacts) {
    Remove-Item $PSScriptRoot\$project\artifacts -Force -Recurse
}

dotnet restore
dotnet build

$version = if($env:APPVEYOR_REPO_TAG) {
    "$env:APPVEYOR_REPO_TAG_NAME"
} else {
    "0.0.1-beta$env:APPVEYOR_BUILD_NUMBER"
}

dotnet pack $PSScriptRoot\$project\$project.csproj -c Release -o $PSScriptRoot\$project\artifacts /p:Version=$version